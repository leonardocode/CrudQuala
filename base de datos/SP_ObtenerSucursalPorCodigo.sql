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
-- Create date: 27/05/2024
-- Description:	obtener sucursal por id
-- =============================================
CREATE PROCEDURE SP_ObtenerSucursalPorCodigo
	-- Add the parameters for the stored procedure here
	@codigo int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	s.codigo  as 'codigo',
	s.descripcion as 'descripcion',
	s.direccion as 'direccion',
	s.identificacion as 'identificacion',
	s.fechaCreacion as 'fechaCreacion',
	m.id as 'idMoneda',
	m.moneda as 'Moneda'
	FROM Sucursales as s
	INNER JOIN Moneda as m
	ON s.idMoneda = m.id
	WHERE S.codigo = @codigo
END
GO
