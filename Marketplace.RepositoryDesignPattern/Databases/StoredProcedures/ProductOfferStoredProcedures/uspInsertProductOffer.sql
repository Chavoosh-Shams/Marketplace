CREATE OR ALTER PROCEDURE dbo.uspInsertProductOffer
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

     
        SET @ProductOfferId = NEWID();

       
        SELECT
            @ProductId = ProductId,
            @SellerId = SellerId,
            @GuidKey = GuidKey
        FROM OPENJSON(@JsonData)
        WITH
        (
            ProductId UNIQUEIDENTIFIER '$.ProductId',
            SellerId UNIQUEIDENTIFIER '$.SellerId',
            GuidKey UNIQUEIDENTIFIER '$.GuidKey'
        );

        
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
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('This product is already offered by this seller.', 16, 1);
            RETURN;
        END

      
        INSERT INTO Marketplace.ProductOffer
        (
            Id,
            GuidKey,
            ProductId,
            SellerId,
            UnitPrice,
            Stock,
            IsActive,
            CreatedDate,
            IsDeleted
        )
        SELECT
            @ProductOfferId, 
            @GuidKey,         
            @ProductId,      
            @SellerId,        
            UnitPrice,
            Stock,
            IsActive,
            CreatedDate,
            0
        FROM OPENJSON(@JsonData)
        WITH
        (
            UnitPrice DECIMAL(18,2) '$.UnitPrice',
            Stock INT '$.Stock',
            IsActive BIT '$.IsActive',
            CreatedDate DATETIME2 '$.CreatedDate'
        );

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