RESTORE FILELISTONLY  
FROM DISK = 'C:\AlizaZeru\ASP.NET\Northwind\Northwind.bak'  
GO 

RESTORE DATABASE Northwind  
FROM DISK = 'C:\AlizaZeru\ASP.NET\Northwind\Northwind.bak'  
WITH MOVE 'Northwind' TO 'C:\AlizaZeru\ASP.NET\Northwind\Northwind.mdf',  
MOVE 'Northwind_log' TO 'C:\AlizaZeru\ASP.NET\Northwind\Northwind.ldf'  