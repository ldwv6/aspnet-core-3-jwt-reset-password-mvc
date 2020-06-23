CREATE TABLE EMployees 
(
	ID int primary key identity,
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	Gender NVARCHAR(50),
	Salary int 
)


insert into EMployees Values('Mark','Hastings','Male',60000)
insert into EMployees Values('Steve','Pound','Male',45000)
insert into EMployees Values('Ben','Hoskins','Male',70000)
insert into EMployees Values('Phlip','Hastings','Male',45000)
insert into EMployees Values('Mary','Lambeth','Female',30000)
insert into EMployees Values('Valarie','Vikings','Female',35000)