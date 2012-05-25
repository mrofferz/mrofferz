
-- Enables the CLR
EXEC sp_configure 'CLR enabled', 1
GO

--Reconfigure the Server after Enabling the CLR
RECONFIGURE
GO