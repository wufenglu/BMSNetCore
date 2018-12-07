IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_User]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Config_User;
END

CREATE TABLE Config_User
(
	ID int identity(1,1) primary key,--���
	UserName varchar(50),--����
	UserCode varchar(50),--����
	Password varchar(50),--����
	IsEnable bit,--�Ƿ�����
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_Modules]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Config_Modules;
END

CREATE TABLE Config_Modules
(
	ID int identity(1,1) primary key,--���
	Name varchar(50),--����
	Code varchar(50),--����
	ParentID INT,--����ID
	[Level] INT,--�㼶
	IsEnable bit,--�Ƿ�����
	OrderBy int,--����
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)


IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_Pages]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Config_Pages;
END

CREATE TABLE Config_Pages
(
	ID int identity(1,1) primary key,--���
	ModuleID INT,--ģ��ID
	Name varchar(50),--����
	Code varchar(50),--����
	Url VARCHAR(256),--ҳ���ַ
	OrderBy int,--����
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_Acions]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
drop table Config_Acions;
END

CREATE TABLE Config_Acions
(
	ID int identity(1,1) primary key,--���
	PageID INT,--ҳ��ID
	Name varchar(50),--����
	Code varchar(50),--����
	OrderBy int,--����
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_Organizations]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
BEGIN
DROP table Config_Organizations;
END

create table Config_Organizations
(
	ID int identity(1,1) primary key,--���
	Name varchar(50),--����
	Code varchar(50),--����
	IsEnable bit,--�Ƿ�����
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_OrganizationDataBase]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
drop table Config_OrganizationDataBase;

create table Config_OrganizationDataBase
(
	ID int identity(1,1) primary key,--���
	OrganizationID INT,--�⻧ID
	DbType varchar(50),--���ݿ�����
	[Server] varchar(256),--���ݿ��ַ
	DatabaseName varchar(50),--���ݿ�����
	UserName varchar(50),--�û���
	[Password] varchar(50),--����
	Port varchar(50),--�˿�
	IsEnable bit,--�Ƿ�����
	IsMaster bit,--�Ƿ�����
	Weight int,--Ȩ��
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)

IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Config_OrganizationModules]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) 
drop table Config_OrganizationModules;

create table Config_OrganizationModules
(
	ID int identity(1,1) primary key,--���
	OrganizationID INT,--�⻧ID
	ModuleID INT,--ģ��ID
	CreaterID INT,--������ID
	Creater varchar(50),--������
	CreatedOn datetime  default getDate(),--��������
	ModifierID INT,--�޸���ID
	Modifier varchar(50),--�޸���
	ModifyOn datetime  default getDate()--�޸�����
)