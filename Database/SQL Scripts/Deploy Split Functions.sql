
CREATE ASSEMBLY SqlSplitList FROM 'C:\Users\Tamer\Documents\Programming Tools\SqlCLRFunctions\bin\Release\SqlSplitList.dll'
go

CREATE FUNCTION SplitCharList(@list NVARCHAR(MAX), @delimiter NCHAR(1) = N',')
RETURNS TABLE (item NVARCHAR(4000))
AS EXTERNAL NAME SqlSplitList.SqlSplitList.SplitCharList
go

CREATE FUNCTION SplitIntList(@list NVARCHAR(MAX), @delimiter NCHAR(1) = N',')
RETURNS TABLE (item int)
AS EXTERNAL NAME SqlSplitList.SqlSplitList.SplitIntList
go
