CREATE OR ALTER PROCEDURE dbo.uspDeleteProductOffer
(
    @JsonData NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @GuidKey UNIQUEIDENTIFIER;

       
        SELECT
            @GuidKey = GuidKey
        FROM OPENJSON(@JsonData)
        WITH
        (
            GuidKey UNIQUEIDENTIFIER '$.GuidKey'
        );

      
        IF @GuidKey IS NULL
        BEGIN
            RAISERROR('GuidKey is required and cannot be null.', 16, 1);
            RETURN;
        END

       
        IF NOT EXISTS (
            SELECT 1
            FROM Marketplace.ProductOffer
            WHERE GuidKey = @GuidKey
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('ProductOffer not found or is already deleted.', 16, 1);
            RETURN;
        END

       
        UPDATE Marketplace.ProductOffer
        SET
            IsDeleted = 1
        WHERE GuidKey = @GuidKey
          AND IsDeleted = 0;

        
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No records were updated. ProductOffer may not exist or is already deleted.', 16, 1);
            RETURN;
        END

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