


Create table Employees
(
	ID int primary key identity,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Gender nvarchar(50),
	Salary int
)

Go

Insert into Employees values ('Mark', 'Hastings', 'Male',60000)



