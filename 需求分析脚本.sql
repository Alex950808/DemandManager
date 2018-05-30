--指向当前要使用的数据库
use master
go
--判断当前数据库是否存在
if exists (select * from sysdatabases where name='DemandManagement')
drop database DemandManagement --删除数据库
go
--创建数据库
create database DemandManagement
on primary
(
	--数据库文件的逻辑名
    name='DemandManagement_data',
    --数据库物理文件名（绝对路径）
    filename='E:\DB\DemandManagement_data.mdf',
    --数据库文件初始大小
    size=10MB,
    --数据文件增长量
    filegrowth=1MB
)
--创建日志文件
log on
(
    name='DemandManagement_log',
    filename='E:\DB\DemandManagement_log.ldf',
    size=2MB,
    filegrowth=1MB
)
go
--指向当前要使用的数据库
use DemandManagement
go
--创建用户表
if exists (select * from sysobjects where name='Users')
drop table Users
go
create table Users
(
    UserId int identity(1,1) primary key,--用户编号，主键，自增长从1开始每次加1
	LoginId nvarchar(20) not null ,--登录账号
	LoginPwd nvarchar(20) not null,--登录密码
    UserName nvarchar(20) not null,--用户姓名
	PhoneNumber nvarchar(20) not null,--电话号码
	Email nvarchar(50) not null--电子邮箱
)
GO
--创建角色表
if exists(select * from sysobjects where name='RoleInfo')
drop table RoleInfo
go
create table RoleInfo
(
	RoleId int primary key identity(1,1),--角色编号，主键
    RoleName nvarchar(20) not null,--角色名称
	RoleDescription nvarchar(50)--角色描述
)
go
--创建菜单表
if exists(select * from sysobjects where name='MenuInfo')
drop table MenuInfo
go
create table MenuInfo
(
    MenuId int identity(1,1) primary key,--菜单编号，主键
    MenuName nvarchar(20) not null--菜单名称
)
go
--创建用户角色表
if exists(select * from sysobjects where name='UserRole')
drop table UserRole
go
create table UserRole
(
	UserRoleId int identity(1,1) primary key,--用户角色编号，主键
    LoginId int not null,--登录账号
    RoleId int not null --角色编号
)
go
--创建角色菜单表
if exists(select * from sysobjects where name='RoleMenu')
drop table RoleMenu
go
create table RoleMenu
(
	RoleMenuId int identity(1,1) primary key,--角色菜单编号
    RoleId int not null,--角色编号
    MenuId int not null--菜单编号
)
go
--创建需求详情表
if exists(select * from sysobjects where name='DemandInfo')
drop table DemandInfo
go
create table DemandInfo
(
	DemandId int not null identity(1,1) primary key,--需求ID编号
	ProductId int,--产品名称
	SubmissionDepartment nvarchar(20),--提交部门
	RequirementDescription text,--需求描述
	--RequirementDescription nvarchar(20),--需求描述
    EmergencyId int,--紧急程度
	Founder int,--创建人
	CreationTime datetime,--创建时间
	Modifier int,--修改人
	ModificationTime datetime,--修改时间
	ConfirmationPerson int,--确认人
	ConfirmTime datetime,--确认时间
	ExpectedOnlineTime datetime,--预计上线时间
	PlanContent nvarchar(max),--上线计划内容
	Publisher int,--发布人
	ScheduleTime datetime,--发布时间
	UpTime datetime--上线时间
)
go
--创建紧急程度表
if exists(select * from sysobjects where name='EmergencyDegree')
drop table EmergencyDegree
go
create table EmergencyDegree
(
	EmergencyId int not null primary key identity(1,1),--紧急Id
	EmergencyName nvarchar(20) not null--紧急名称
)
go
--创建受影响产品表
if exists(select * from sysobjects where name='ProductList')
drop table ProductList
go
create table ProductList
(
	ProductId int identity(1,1) primary key,--产品编号
	VersionNumber nvarchar(50) not null,--版本号
    DemandId int not null--需求ID编号
)
go
--创建产品列表
if exists(select * from sysobjects where name='Products')
drop table Products
go
create table Products
(
	ProductId int identity(1,1) primary key,--产品编号
	ProductName nvarchar(20) not null--产品名称
)
go
------------------------------------------
--添加唯一约束(将LoginId作为主键)
alter table Users
add constraint UQ_LoginId unique(LoginId)
go
--添加数据
--用户表
insert into Users values('1000','123456','赵云','15882182484','670535490@qq.com')
insert into Users values('1001','123456','张飞','13438239609','2219909974@qq.com')
insert into Users values('1002','123456','马超','15882563214','2350758127@qq.com')
insert into Users values('10086','123456','管理员','15882563214','6705354900@qq.com')

--角色表
insert into RoleInfo values('普通用户','普通用户拥有发布需求、修改需求、查看需求功能')
insert into RoleInfo values('产品经理','产品经理拥有查看需求、认领需求、确认需求功能')
insert into RoleInfo values('管理员','账号权限管理')
--菜单表
insert into MenuInfo values('提出需求')
insert into MenuInfo values('修改需求')
insert into MenuInfo values('查看需求')
insert into MenuInfo values('确认需求')
insert into MenuInfo values('上线计划')
--用户角色表
insert into UserRole(LoginId,RoleId) values('1000','1')--赵云是普通用户
insert into UserRole(LoginId,RoleId) values('1001','2')--张飞是产品经理
insert into UserRole(LoginId,RoleId) values('1002','1')--马超是普通用户
insert into UserRole(LoginId,RoleId) values('10086','3')--诸葛亮是超级管理员
--角色菜单表
insert into RoleMenu values('1','1')--普通用户拥有提出需求功能菜单
insert into RoleMenu values('1','2')--普通用户拥有修改需求功能菜单
insert into RoleMenu values('1','3')--普通用户拥有查看需求功能菜单
insert into RoleMenu values('2','3')--产品经理拥有查看需求功能菜单
insert into RoleMenu values('2','4')--产品经理拥有确认需求功能菜单
insert into RoleMenu values('2','5')--产品经理拥有上线计划功能菜单
--紧急程度表
insert into EmergencyDegree values('一般')
insert into EmergencyDegree values('紧急')

--产品列表
insert into Products([ProductName]) values('50yc')
insert into Products([ProductName]) values('EMP')
--需求详情表
--insert into DemandInfo([ProductId], [SubmissionDepartment], [RequirementDescription], [EmergencyId], [Founder],[CreationTime],[UserId]) 
--values('1','提交部门','需求描述','1','赵云','2018-1-24','1')
--受影响产品表
insert into ProductList([VersionNumber], [DemandId]) 
values('1.0','1')


go

--添加主外键约束（主表用户表Products和从表用户角色DemandInfo建立关系，关联列为LoginId）
ALTER TABLE DemandInfo
ADD CONSTRAINT FK_ProductIds
FOREIGN KEY(ProductId) REFERENCES Products(ProductId)


--添加主外键约束（主表角色信息表RoleInfo和从表用户角色UserRole建立关系，关联列为RoleId）
ALTER TABLE UserRole
ADD CONSTRAINT FK_UserRole_RoleId
FOREIGN KEY(RoleId) REFERENCES RoleInfo(RoleId)

--添加主外键约束（主表角色信息表RoleInfo和从表角色菜单表RoleMenu建立关系，关联列为RoleId）
ALTER TABLE RoleMenu
ADD CONSTRAINT FK_RoleMenu_RoleId
FOREIGN KEY(RoleId) REFERENCES RoleInfo(RoleId)

--添加主外键约束（主表菜单信息表MenuInfo和从表角色菜单表RoleMenu建立关系，关联列为MenuId）
ALTER TABLE RoleMenu
ADD CONSTRAINT FK_MenuId
FOREIGN KEY(MenuId) REFERENCES MenuInfo(MenuId)

--添加主外键约束（主表紧急程度表EmergencyDegree和从表需求详情DemandInfo建立关系，关联列为EmergencyId）
ALTER TABLE DemandInfo
ADD CONSTRAINT FK_EmergencyId
FOREIGN KEY(EmergencyId) REFERENCES EmergencyDegree(EmergencyId)

--添加主外键约束（主表需求详情表DemandInfo和从表产品列表ProductList建立关系，关联列为DemandId）
--ALTER TABLE ProductList
--ADD CONSTRAINT FK_DemandId
--FOREIGN KEY(DemandId) REFERENCES DemandInfo(DemandId)

--添加主外键约束（主表产品表Products和从表产品列表ProductList建立关系，关联列为ProductId）
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
