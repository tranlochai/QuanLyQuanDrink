CREATE DATABASE QuanLyQuanDrink
GO

USE QuanLyQuanDrink
GO

CREATE TABLE TableDrink
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa có tên',
	status NVARCHAR(100) NOT NULL  DEFAULT N'Trống'  -- trống hoặc có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Chưa có tên',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT N'0',
	Type INT NOT NULL DEFAULT 0 --1: admin && 0:staff
)
GO

CREATE TABLE DrinkCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa có tên'
)

CREATE TABLE Drink
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa có tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0

	FOREIGN KEY (idCategory) REFERENCES dbo.DrinkCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0         --1: đã thanh toán && 0: chưa thanh toán

	FOREIGN KEY (idTable) REFERENCES dbo.TableDrink(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idDrink INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idDrink) REFERENCES dbo.Drink(id)
)
GO

INSERT INTO dbo.Account
(	UserName,  
	DisplayName,
	PassWord,
	Type
)

VALUES (	N'K9',			--UserName (nvarchar100)
			N'RongK9',		--DisplayName, (nvarchar100)
			N'1',			--PassWord, (nvarchar1000)
			1				--	Type (int)
		)

INSERT INTO dbo.Account
(	UserName, DisplayName,PassWord, Type)
VALUES (	N'staff',N'staff',N'1',	0)
GO 

--SELECT * FROM dbo.Account
CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN 
	SELECT * FROM dbo.Account WHERE UserName = @userName
END 
GO

EXEC dbo.USP_GetAccountByUserName @userName = N'K9'  --nvarchar(100)

SELECT COUNT (*) FROM dbo.Account WHERE UserName = N'K9' AND PassWord = N'2'
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS 
BEGIN 
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO


--thêm bàn TableDrink
DECLARE @i INT = 0 
WHILE @i <=10
BEGIN 
	INSERT dbo.TableDrink (name)VALUES (N'Bàn' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO

--SELECT * FROM dbo.TableDrink

CREATE PROC USP_GetTableList
AS  SELECT * FROM dbo.TableDrink
GO

UPDATE dbo.TableDrink SET STATUS = N'Có người' WHERE id = 9 

EXEC dbo.USP_GetTableList
GO


-- thêm category của đồ uống
INSERT dbo.DrinkCategory(name) VALUES	(N'Trà sữa')
INSERT dbo.DrinkCategory(name) VALUES	(N'Cà phê')
INSERT dbo.DrinkCategory(name) VALUES	(N'Rượu')
INSERT dbo.DrinkCategory(name) VALUES	(N'Bia')
INSERT dbo.DrinkCategory(name) VALUES	(N'Sinh tố')
INSERT dbo.DrinkCategory(name) VALUES	(N'Nước ngọt')
	
-- thêm các đồ uống cụ thể
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Trà sữa trân châu', 1, 20000)
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Cà phê sữa', 2, 13000)
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Rượu Vodka', 3, 50000)
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Bia Heineken', 4, 25000)
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Sinh tố dừa', 5, 18000)
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Nước ngọt Coca Cola', 6, 12000)
INSERT dbo.Drink (name, idCategory, price) VALUES (N'Trà lipton', 1, 15000)


--thêm bill
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 1, 0)
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status)VALUES (GETDATE(), NULL, 2, 0)		-- chưa checkout thì là 0
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status)VALUES (GETDATE(), GETDATE(), 2, 1)	--checkout rồi thì là 1 và phải có time checkout
INSERT dbo.Bill(DateCheckIn, DateCheckOut, idTable, status)VALUES (GETDATE(), NULL, 2, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 1, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 3, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 3, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 4, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 3, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 4, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 5, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 12, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 22, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 28, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 29, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 24, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 29, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 28, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 9, 0)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 8, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), GETDATE(), 7, 1)
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status) VALUES (GETDATE(), NULL, 6, 0)
SELECT * FROM dbo.TableDrink


--thêm bill info
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (1, 1, 2 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (1, 3, 4 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (1, 5, 1 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (2, 1, 2 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (2, 6, 2 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (3, 5, 2 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (4, 2, 3 )
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (4, 2, 1)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (5, 3, 2)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (5, 4, 4)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (5, 4, 3)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (10, 3, 6)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (5, 5, 8)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (12, 4, 20)
INSERT dbo.BillInfo (idBill, idDrink, count) VALUES (10, 4, 11)
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (16, 2, 3 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (15, 6, 30)
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (14, 3, 8 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (5, 5, 5 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (6, 6, 6 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (7, 7, 7 )
INSERT dbo.BillInfo(idBill,idDrink,count) VALUES (8, 7, 8 )
GO 

SELECT * FROM dbo.Bill WHERE idTable = 2 AND  status = 0
SELECT  * FROM dbo.BillInfo WHERE idBill = 5

SELECT  d.name, bi.count, d.price, d.price*bi.count AS totalPrice 
FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Drink AS d
WHERE bi.idBill = b.id AND bi.idDrink = d.id AND b.status = 0 AND b.idTable = 3

SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
SELECT * FROM dbo.Drink
SELECT * FROM dbo.DrinkCategory
SELECT * FROM dbo.TableDrink

SELECT * FROM dbo.Drink
GO


CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT dbo.Bill
		(	DateCheckIn,
			DateCheckOut,
			idTable,
			status
		)
	VALUES	(	GETDATE(),
				NULL,
				@idTable,
				0
			)
END
GO

CREATE PROC USP_InsertBillInfo
@idBill INT, @idDrink INT, @count INT
AS
BEGIN
	DECLARE @isExitBillInfo INT;
	DECLARE @drinkCount INT = 1
	
	SELECT @isExitBillInfo = id,  @drinkCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE id = @idBill AND idDrink = @idDrink

	IF(@isExitBillInfo > 0)	
	BEGIN
		DECLARE @newCount INT = @drinkCount + @count
		IF (@newCount >0)
			UPDATE dbo.BillInfo	SET count = @drinkCount + @count WHERE idDrink = @idDrink
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idDrink = @idDrink
	END
	ELSE
	BEGIN
		INSERT dbo.BillInfo(idBill,idDrink,count) 
		VALUES	(@idBill, @idDrink, @count )
	END
END
GO

DELETE dbo.BillInfo
DELETE dbo.Bill

CREATE  TRIGGER UTP_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM Inserted
	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	DECLARE  @count int = 0
	SELECT @count = COUNT (*) FROM dbo.Bill WHERE idTable = @idTable AND status =0

	IF (@count =0) UPDATE dbo.TableDrink SET status=N'Trống' where id=@idTable
END
GO

CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END

SELECT * FROM dbo.Drink
WHERE dbo.fuConvertToUnsign1(name) COLLATE SQL_Latin1_General_CP1251_CS_AS LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') COLLATE SQL_Latin1_General_CP1251_CS_AS + '%'

SELECT MAX(id) FROM dbo.Bill


UPDATE dbo.Bill	SET status = 1 WHERE id = 1



select * from dbo.Account