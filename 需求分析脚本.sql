--ָ��ǰҪʹ�õ����ݿ�
use master
go
--�жϵ�ǰ���ݿ��Ƿ����
if exists (select * from sysdatabases where name='DemandManagement')
drop database DemandManagement --ɾ�����ݿ�
go
--�������ݿ�
create database DemandManagement
on primary
(
	--���ݿ��ļ����߼���
    name='DemandManagement_data',
    --���ݿ������ļ���������·����
    filename='E:\DB\DemandManagement_data.mdf',
    --���ݿ��ļ���ʼ��С
    size=10MB,
    --�����ļ�������
    filegrowth=1MB
)
--������־�ļ�
log on
(
    name='DemandManagement_log',
    filename='E:\DB\DemandManagement_log.ldf',
    size=2MB,
    filegrowth=1MB
)
go
--ָ��ǰҪʹ�õ����ݿ�
use DemandManagement
go
--�����û���
if exists (select * from sysobjects where name='Users')
drop table Users
go
create table Users
(
    UserId int identity(1,1) primary key,--�û���ţ���������������1��ʼÿ�μ�1
	LoginId nvarchar(20) not null ,--��¼�˺�
	LoginPwd nvarchar(20) not null,--��¼����
    UserName nvarchar(20) not null,--�û�����
	PhoneNumber nvarchar(20) not null,--�绰����
	Email nvarchar(50) not null--��������
)
GO
--������ɫ��
if exists(select * from sysobjects where name='RoleInfo')
drop table RoleInfo
go
create table RoleInfo
(
	RoleId int primary key identity(1,1),--��ɫ��ţ�����
    RoleName nvarchar(20) not null,--��ɫ����
	RoleDescription nvarchar(50)--��ɫ����
)
go
--�����˵���
if exists(select * from sysobjects where name='MenuInfo')
drop table MenuInfo
go
create table MenuInfo
(
    MenuId int identity(1,1) primary key,--�˵���ţ�����
    MenuName nvarchar(20) not null--�˵�����
)
go
--�����û���ɫ��
if exists(select * from sysobjects where name='UserRole')
drop table UserRole
go
create table UserRole
(
	UserRoleId int identity(1,1) primary key,--�û���ɫ��ţ�����
    LoginId int not null,--��¼�˺�
    RoleId int not null --��ɫ���
)
go
--������ɫ�˵���
if exists(select * from sysobjects where name='RoleMenu')
drop table RoleMenu
go
create table RoleMenu
(
	RoleMenuId int identity(1,1) primary key,--��ɫ�˵����
    RoleId int not null,--��ɫ���
    MenuId int not null--�˵����
)
go
--�������������
if exists(select * from sysobjects where name='DemandInfo')
drop table DemandInfo
go
create table DemandInfo
(
	DemandId int not null identity(1,1) primary key,--����ID���
	ProductId int,--��Ʒ����
	SubmissionDepartment nvarchar(20),--�ύ����
	RequirementDescription text,--��������
	--RequirementDescription nvarchar(20),--��������
    EmergencyId int,--�����̶�
	Founder int,--������
	CreationTime datetime,--����ʱ��
	Modifier int,--�޸���
	ModificationTime datetime,--�޸�ʱ��
	ConfirmationPerson int,--ȷ����
	ConfirmTime datetime,--ȷ��ʱ��
	ExpectedOnlineTime datetime,--Ԥ������ʱ��
	PlanContent nvarchar(max),--���߼ƻ�����
	Publisher int,--������
	ScheduleTime datetime,--����ʱ��
	UpTime datetime--����ʱ��
)
go
--���������̶ȱ�
if exists(select * from sysobjects where name='EmergencyDegree')
drop table EmergencyDegree
go
create table EmergencyDegree
(
	EmergencyId int not null primary key identity(1,1),--����Id
	EmergencyName nvarchar(20) not null--��������
)
go
--������Ӱ���Ʒ��
if exists(select * from sysobjects where name='ProductList')
drop table ProductList
go
create table ProductList
(
	ProductId int identity(1,1) primary key,--��Ʒ���
	VersionNumber nvarchar(50) not null,--�汾��
    DemandId int not null--����ID���
)
go
--������Ʒ�б�
if exists(select * from sysobjects where name='Products')
drop table Products
go
create table Products
(
	ProductId int identity(1,1) primary key,--��Ʒ���
	ProductName nvarchar(20) not null--��Ʒ����
)
go
------------------------------------------
--���ΨһԼ��(��LoginId��Ϊ����)
alter table Users
add constraint UQ_LoginId unique(LoginId)
go
--�������
--�û���
insert into Users values('1000','123456','����','15882182484','670535490@qq.com')
insert into Users values('1001','123456','�ŷ�','13438239609','2219909974@qq.com')
insert into Users values('1002','123456','��','15882563214','2350758127@qq.com')
insert into Users values('10086','123456','����Ա','15882563214','6705354900@qq.com')

--��ɫ��
insert into RoleInfo values('��ͨ�û�','��ͨ�û�ӵ�з��������޸����󡢲鿴������')
insert into RoleInfo values('��Ʒ����','��Ʒ����ӵ�в鿴������������ȷ��������')
insert into RoleInfo values('����Ա','�˺�Ȩ�޹���')
--�˵���
insert into MenuInfo values('�������')
insert into MenuInfo values('�޸�����')
insert into MenuInfo values('�鿴����')
insert into MenuInfo values('ȷ������')
insert into MenuInfo values('���߼ƻ�')
--�û���ɫ��
insert into UserRole(LoginId,RoleId) values('1000','1')--��������ͨ�û�
insert into UserRole(LoginId,RoleId) values('1001','2')--�ŷ��ǲ�Ʒ����
insert into UserRole(LoginId,RoleId) values('1002','1')--������ͨ�û�
insert into UserRole(LoginId,RoleId) values('10086','3')--������ǳ�������Ա
--��ɫ�˵���
insert into RoleMenu values('1','1')--��ͨ�û�ӵ����������ܲ˵�
insert into RoleMenu values('1','2')--��ͨ�û�ӵ���޸������ܲ˵�
insert into RoleMenu values('1','3')--��ͨ�û�ӵ�в鿴�����ܲ˵�
insert into RoleMenu values('2','3')--��Ʒ����ӵ�в鿴�����ܲ˵�
insert into RoleMenu values('2','4')--��Ʒ����ӵ��ȷ�������ܲ˵�
insert into RoleMenu values('2','5')--��Ʒ����ӵ�����߼ƻ����ܲ˵�
--�����̶ȱ�
insert into EmergencyDegree values('һ��')
insert into EmergencyDegree values('����')

--��Ʒ�б�
insert into Products([ProductName]) values('50yc')
insert into Products([ProductName]) values('EMP')
--���������
--insert into DemandInfo([ProductId], [SubmissionDepartment], [RequirementDescription], [EmergencyId], [Founder],[CreationTime],[UserId]) 
--values('1','�ύ����','��������','1','����','2018-1-24','1')
--��Ӱ���Ʒ��
insert into ProductList([VersionNumber], [DemandId]) 
values('1.0','1')


go

--��������Լ���������û���Products�ʹӱ��û���ɫDemandInfo������ϵ��������ΪLoginId��
ALTER TABLE DemandInfo
ADD CONSTRAINT FK_ProductIds
FOREIGN KEY(ProductId) REFERENCES Products(ProductId)


--��������Լ���������ɫ��Ϣ��RoleInfo�ʹӱ��û���ɫUserRole������ϵ��������ΪRoleId��
ALTER TABLE UserRole
ADD CONSTRAINT FK_UserRole_RoleId
FOREIGN KEY(RoleId) REFERENCES RoleInfo(RoleId)

--��������Լ���������ɫ��Ϣ��RoleInfo�ʹӱ��ɫ�˵���RoleMenu������ϵ��������ΪRoleId��
ALTER TABLE RoleMenu
ADD CONSTRAINT FK_RoleMenu_RoleId
FOREIGN KEY(RoleId) REFERENCES RoleInfo(RoleId)

--��������Լ��������˵���Ϣ��MenuInfo�ʹӱ��ɫ�˵���RoleMenu������ϵ��������ΪMenuId��
ALTER TABLE RoleMenu
ADD CONSTRAINT FK_MenuId
FOREIGN KEY(MenuId) REFERENCES MenuInfo(MenuId)

--��������Լ������������̶ȱ�EmergencyDegree�ʹӱ���������DemandInfo������ϵ��������ΪEmergencyId��
ALTER TABLE DemandInfo
ADD CONSTRAINT FK_EmergencyId
FOREIGN KEY(EmergencyId) REFERENCES EmergencyDegree(EmergencyId)

--��������Լ�����������������DemandInfo�ʹӱ��Ʒ�б�ProductList������ϵ��������ΪDemandId��
--ALTER TABLE ProductList
--ADD CONSTRAINT FK_DemandId
--FOREIGN KEY(DemandId) REFERENCES DemandInfo(DemandId)

--��������Լ���������Ʒ��Products�ʹӱ��Ʒ�б�ProductList������ϵ��������ΪProductId��
ALTER TABLE ProductList
ADD CONSTRAINT FK_ProductId
FOREIGN KEY(ProductId) REFERENCES Products(ProductId)

go
select * from Users
select * from RoleInfo
select * from MenuInfo
select * from UserRole
select * from RoleMenu
select * from DemandInfo
select * from EmergencyDegree
select * from ProductList
select * from Products
