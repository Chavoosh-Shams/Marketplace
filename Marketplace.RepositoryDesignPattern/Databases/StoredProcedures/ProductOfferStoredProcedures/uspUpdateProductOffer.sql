CREATE OR ALTER PROCEDURE dbo.uspUpdateProductOffer
(
    @JsonData NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @ProductOfferId UNIQUEIDENTIFIER;
        DECLARE @ProductId UNIQUEIDENTIFIER;
        DECLARE @SellerId UNIQUEIDENTIFIER;
        DECLARE @GuidKey UNIQUEIDENTIFIER;

        
        SELECT
            @GuidKey = GuidKey,
            @ProductId = ProductId,
            @SellerId = SellerId
        FROM OPENJSON(@JsonData)
        WITH
        (
            GuidKey UNIQUEIDENTIFIER '$.GuidKey',
            ProductId UNIQUEIDENTIFIER '$.ProductId',
            SellerId UNIQUEIDENTIFIER '$.SellerId'
        );

        
        SELECT
            @ProductOfferId = Id
        FROM Marketplace.ProductOffer
        WHERE GuidKey = @GuidKey
          AND IsDeleted = 0;

        
        IF @ProductOfferId IS NULL
        BEGIN
            RAISERROR('ProductOffer not found or is deleted.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (
            SELECT 1 
            FROM Product.Product
            WHERE Id = @ProductId 
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('Product not found or is deleted.', 16, 1);
            RETURN;
        END

        
        IF NOT EXISTS (
            SELECT 1 
            FROM Person.Seller
            WHERE Id = @SellerId 
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('Seller not found or is deleted.', 16, 1);
            RETURN;
        END

      
        IF EXISTS (
            SELECT 1 
            FROM Marketplace.ProductOffer 
            WHERE ProductId = @ProductId 
              AND SellerId = @SellerId 
              AND Id != @ProductOfferId
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('This product is already offered by this seller.', 16, 1);
            RETURN;
        END

       
        UPDATE Marketplace.ProductOffer
        SET
            ProductId = @ProductId,
            SellerId = @SellerId,
            UnitPrice = JsonData.UnitPrice,
            Stock = JsonData.Stock,
            IsActive = JsonData.IsActive,
            CreatedDate = JsonData.CreatedDate
        FROM OPENJSON(@JsonData)
        WITH
        (
            GuidKey UNIQUEIDENTIFIER '$.GuidKey',
            ProductId UNIQUEIDENTIFIER '$.ProductId',
            SellerId UNIQUEIDENTIFIER '$.SellerId',
            UnitPrice DECIMAL(18,2) '$.UnitPrice',
            Stock INT '$.Stock',
            IsActive BIT '$.IsActive',
            CreatedDate DATETIME2 '$.CreatedDate'
        ) AS JsonData
        WHERE ProductOffer.GuidKey = JsonData.GuidKey
          AND ProductOffer.IsDeleted = 0;

        COMMIT TRANSACTION;

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