
DROP TABLE IF EXISTS dbo.CompanyResponse;
DROP TABLE IF EXISTS dbo.EmployeeResponse;
DROP TABLE IF EXISTS dbo.[Resume];
DROP TABLE IF EXISTS dbo.Vacancy;
DROP TABLE IF EXISTS dbo.Company;
DROP TABLE IF EXISTS dbo.CompanySize;
DROP TABLE IF EXISTS dbo.CompanyType;
DROP TABLE IF EXISTS dbo.Employee;
DROP TABLE IF EXISTS dbo.Logins;


create table Logins (
	ID int primary key identity (1,1),
		-- ������������ ��������
	Username varchar(50) not null unique,
		-- �� ������ �����
	Pass varchar(100) not null,
		-- Admin, Company, Employee
	UserType varchar(15) not null
);
	-- ��� ��� Pa$$w0rd
insert into Logins values ('admin', 'SVNNdktYcFhwYWREaVVvTzpzIdnCQZ4ak4ACsEVFSpf7m/B/', 'Admin');


create table Employee (
	ID int primary key identity (1,1),
	LoginID int not null,
	constraint FK_Employee_Logins foreign key (LoginID) references Logins (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
		-- ���������� ������ ������
	ShowName varchar(50) not null unique,
	EMail varchar(50) not null,
	PhoneNumber varchar(15) not null,
	BirthDate datetime not null,
		-- ��� ��� ����� ����� ��������������
	Verified bit default 0,
	RegistrationDate datetime default getdate(),
);

create table CompanyType (
	ID int primary key identity (1,1),
	[Type] varchar(15) not null,
);

insert into CompanyType values ('���');
insert into CompanyType values ('��');
insert into CompanyType values ('���');
insert into CompanyType values ('��');
insert into CompanyType values ('���');
insert into CompanyType values ('�����. ���.');
insert into CompanyType values ('���. ���.');
insert into CompanyType values ('����');
insert into CompanyType values ('���. ����.');
insert into CompanyType values ('��');
insert into CompanyType values ('���');
insert into CompanyType values ('���');
insert into CompanyType values ('������');


create table CompanySize (
	ID int primary key identity (1,1),
	[Size] varchar(30) not null,
);
insert into CompanySize values ('����� 50 �����������');
insert into CompanySize values ('�� 51 �� 100 �����������');
insert into CompanySize values ('�� 101 �� 250 �����������');
insert into CompanySize values ('�� 251 �� 500 �����������');
insert into CompanySize values ('����� 500 �����������');


create table Company (
	ID int primary key identity (1,1),
	LoginID int not null,
	constraint FK_Company_Logins foreign key (LoginID) references Logins (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
		-- ��� ��� ����� ����� ��������������
	Verified bit default 0,
	RegistrationDate datetime default getdate(),
		-- ����� ������� � �������
		-- �� ��������
	ShowName varchar(50) not null unique,
		-- ��� ��������
	CompanyTypeID int not null,
	constraint FK_Company_CompanyType foreign key (CompanyTypeID) references CompanyType (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
		-- ���� ����������
	WebSite varchar(100) not null,
		-- ������ ��������
	CompanySizeID int not null,
	constraint FK_Company_CompanySize foreign key (CompanySizeID) references CompanySize (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
		-- �����-�� ���� ���� �������
	City varchar(30) not null,
		-- ���������� ������
	ContactName varchar(30) not null,
	ContactSurname varchar(30) not null,
	ContactEmail varchar(50) not null,
	ContactPhone varchar(15) not null,
);

	-- �����, ����� ���������
create table Vacancy (
	ID int primary key identity (1,1),
	CompanyID int not null,
	constraint FK_Vacancy_Company foreign key (CompanyID) references Company (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	Title varchar(100) not null,
	[Description] varchar(max) not null
);

create table [Resume] (
	ID int primary key identity (1,1),
	EmployeeID int not null,
	constraint FK_Resume_Employee foreign key (EmployeeID) references Employee (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	Title varchar(100) not null,
	[Description] varchar(max) not null
);


create table EmployeeResponse (
	EmployeeID int not null,
	VacancyID int not null,
		-- ���� �������� ������ ������, �� ��� ��������
	[Description] varchar(max),
		-- ���� ��������� ��� ������������
	ResponseDate datetime default getdate(),
	constraint PK_EmployeeResponse primary key (EmployeeID, VacancyID),
	constraint FK_EmployeeResponse_Employee foreign key (EmployeeID) references Employee (ID)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
	constraint FK_EmployeeResponse_Vacancy foreign key (VacancyID) references Vacancy (ID)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

create table CompanyResponse (
	CompanyID int not null,
	ResumeID int not null,
		-- ���� �������� ������ ������, �� ��� ��������
	[Description] varchar(max),
		-- ���� ��������� ��� ������������
	ResponseDate datetime default getdate(),
	constraint PK_CompanyResponse primary key (CompanyID, ResumeID),
	constraint FK_CompanyResponse_Company foreign key (CompanyID) references Company (ID)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
	constraint FK_CompanyResponse_Resume foreign key (ResumeID) references [Resume] (ID)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
);