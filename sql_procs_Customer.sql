USE [SummerOfNHibernate]
GO

--|--------------------------------------------------------------------------------
--| [CustomerInsert] - Insert Procedure Script for Customer
--|--------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id (N'[dbo].[CustomerInsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) DROP PROCEDURE [dbo].[CustomerInsert]
GO

CREATE PROCEDURE [dbo].[CustomerInsert]
(
	@CustomerId int = NULL OUTPUT,
	@Version int,
	@Firstname nvarchar(50) = NULL,
	@Lastname nvarchar(50) = NULL
)
AS
	SET NOCOUNT ON

	INSERT INTO [Customer]
	(
		[Version],
		[Firstname],
		[Lastname]
	)
	VALUES
	(
		@Version,
		@Firstname,
		@Lastname
	)

	SELECT @CustomerId = SCOPE_IDENTITY();

	RETURN @@Error
GO

--|--------------------------------------------------------------------------------
--| [CustomerUpdate] - Update Procedure Script for Customer
--|--------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id (N'[dbo].[CustomerUpdate]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) DROP PROCEDURE [dbo].[CustomerUpdate]
GO

CREATE PROCEDURE [dbo].[CustomerUpdate]
(
	@CustomerId int,
	@Version int,
	@Firstname nvarchar(50) = NULL,
	@Lastname nvarchar(50) = NULL
)
AS
	SET NOCOUNT ON
	
	UPDATE [Customer]
	SET
		[Version] = @Version,
		[Firstname] = @Firstname,
		[Lastname] = @Lastname
	WHERE 
		[CustomerId] = @CustomerId

	RETURN @@Error
GO

--|--------------------------------------------------------------------------------
--| [CustomerDelete] - Update Procedure Script for Customer
--|--------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id (N'[dbo].[CustomerDelete]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) DROP PROCEDURE [dbo].[CustomerDelete]
GO

CREATE PROCEDURE [dbo].[CustomerDelete]
(
	@CustomerId int
)
AS
	SET NOCOUNT ON

	DELETE 
	FROM   [Customer]
	WHERE  
		[CustomerId] = @CustomerId

	RETURN @@Error
GO

