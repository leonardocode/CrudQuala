USE[DbAsistQuala]
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leonardo Amaya
-- Create date: 27-05-2024
-- Description:	Editar Sucursal
-- =============================================
CREATE PROCEDURE SP_EditarSucursal
	-- Add the parameters for the stored procedure here
	@codigo int,
	@descripcion varchar(250),
	@direccion varchar(250),
	@identificacion varchar(50),
	@fechaCreacion datetime,
	@idMoneda int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Sucursales
	set descripcion = @descripcion,
	direccion = @direccion,
	identificacion = @identificacion,
	idMoneda = @idMoneda
	where codigo = @codigo
END
GO
