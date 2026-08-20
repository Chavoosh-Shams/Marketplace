CREATE OR ALTER PROCEDURE dbo.uspInsertOrderWithDetails
(
    @JsonData NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @OrderHeaderId UNIQUEIDENTIFIER;
        DECLARE @OrderHeaderGuidKey UNIQUEIDENTIFIER;
        DECLARE @CustomerId UNIQUEIDENTIFIER;
        DECLARE @OrderDate DATETIME2;
        DECLARE @ShipCity NVARCHAR(100);
        DECLARE @ShipAddress NVARCHAR(200);
        DECLARE @TotalDetailsCount INT;
        DECLARE @ValidDetailsCount INT;
        DECLARE @InvalidProductOfferCount INT;

        -- تولید شناسه برای OrderHeader
        SET @OrderHeaderId = NEWID();

        -- استخراج اطلاعات Header از JSON
        SELECT
            @CustomerId = CustomerId,
            @OrderHeaderGuidKey = GuidKey,
            @OrderDate = OrderDate,
            @ShipCity = ShipCity,
            @ShipAddress = ShipAddress
        FROM OPENJSON(@JsonData)
        WITH
        (
            CustomerId UNIQUEIDENTIFIER '$.CustomerId',
            GuidKey UNIQUEIDENTIFIER '$.GuidKey',
            OrderDate DATETIME2 '$.OrderDate',
            ShipCity NVARCHAR(100) '$.ShipCity',
            ShipAddress NVARCHAR(200) '$.ShipAddress'
        );

        -- اعتبارسنجی وجود Customer
        IF NOT EXISTS (
            SELECT 1 
            FROM Person.Customer 
            WHERE Id = @CustomerId 
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('Customer not found or is deleted.', 16, 1);
            RETURN;
        END

        -- اعتبارسنجی وجود GuidKey برای Header
        IF @OrderHeaderGuidKey IS NULL
        BEGIN
            RAISERROR('GuidKey is required for OrderHeader.', 16, 1);
            RETURN;
        END

        -- درج OrderHeader
        INSERT INTO Marketplace.OrderHeader
        (
            Id,
            GuidKey,
            ShipCity,
            ShipAddress,
            OrderDate,
            CustomerId,
            IsDeleted
        )
        VALUES
        (
            @OrderHeaderId,
            @OrderHeaderGuidKey,
            @ShipCity,
            @ShipAddress,
            @OrderDate,
            @CustomerId,
            0
        );

        -- شمارش تعداد کل Detailها
        SELECT @TotalDetailsCount = COUNT(*) 
        FROM OPENJSON(@JsonData, '$.PostOrderDetailDtos');

        -- بررسی وجود ProductOfferهای نامعتبر
        SELECT @InvalidProductOfferCount = COUNT(*)
        FROM OPENJSON(@JsonData, '$.PostOrderDetailDtos')
        WITH
        (
            ProductOfferId UNIQUEIDENTIFIER '$.ProductOfferId'
        ) AS Details
        WHERE NOT EXISTS (
            SELECT 1 
            FROM Marketplace.ProductOffer 
            WHERE Id = Details.ProductOfferId 
              AND IsDeleted = 0
              AND IsActive = 1
        );

        -- اگر ProductOffer نامعتبر وجود داشت، خطا بده
        IF @InvalidProductOfferCount > 0
        BEGIN
            RAISERROR('One or more ProductOffers are invalid or inactive.', 16, 1);
            RETURN;
        END

        -- شمارش تعداد Detailهایی که موجودی کافی دارند
        SELECT @ValidDetailsCount = COUNT(*)
        FROM OPENJSON(@JsonData, '$.PostOrderDetailDtos')
        WITH
        (
            ProductOfferId UNIQUEIDENTIFIER '$.ProductOfferId',
            Quantity INT '$.Quantity'
        ) AS Details
        WHERE EXISTS (
            SELECT 1 
            FROM Marketplace.ProductOffer 
            WHERE Id = Details.ProductOfferId 
              AND IsDeleted = 0
              AND IsActive = 1
              AND Stock >= Details.Quantity
        );

        -- بررسی موجودی کافی برای همه Detailها
        IF @ValidDetailsCount < @TotalDetailsCount
        BEGIN
            RAISERROR('One or more ProductOffers have insufficient stock.', 16, 1);
            RETURN;
        END

        -- درج OrderDetail ها به صورت مستقیم
        INSERT INTO Marketplace.OrderDetail
        (
            Id,
            GuidKey,
            UnitPrice,
            Quantity,
            OrderHeaderId,
            ProductOfferId,
            IsDeleted
        )
        SELECT
            NEWID() AS Id,
            GuidKey,
            UnitPrice,
            Quantity,
            @OrderHeaderId AS OrderHeaderId,
            ProductOfferId,
            0 AS IsDeleted
        FROM OPENJSON(@JsonData, '$.PostOrderDetailDtos')
        WITH
        (
            GuidKey UNIQUEIDENTIFIER '$.GuidKey',
            UnitPrice DECIMAL(18,2) '$.UnitPrice',
            Quantity INT '$.Quantity',
            ProductOfferId UNIQUEIDENTIFIER '$.ProductOfferId'
        );

        COMMIT TRANSACTION;
        
        -- برگرداندن نتایج
        SELECT 
            @OrderHeaderId AS OrderHeaderId,
            @OrderHeaderGuidKey AS OrderHeaderGuidKey;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(MAX);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);

    END CATCH
END
GO