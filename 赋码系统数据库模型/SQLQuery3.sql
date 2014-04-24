

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Equipment') and o.name = 'FK_EQUIPMEN_REFERENCE_EQUIPMEN')
alter table Equipment
   drop constraint FK_EQUIPMEN_REFERENCE_EQUIPMEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ExportPrintLog') and o.name = 'FK_EXPORTPR_REFERENCE_PACKAGES')
alter table ExportPrintLog
   drop constraint FK_EXPORTPR_REFERENCE_PACKAGES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ExportPrintLog') and o.name = 'FK_EXPORTPR_REFERENCE_USERS')
alter table ExportPrintLog
   drop constraint FK_EXPORTPR_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ExportPrintLog') and o.name = 'FK_EXPORTPR_REFERENCE_PRODUCT')
alter table ExportPrintLog
   drop constraint FK_EXPORTPR_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ExportRelationLog') and o.name = 'FK_EXPORTRE_REFERENCE_TASK')
alter table ExportRelationLog
   drop constraint FK_EXPORTRE_REFERENCE_TASK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ExportRelationLog') and o.name = 'FK_EXPORTRE_REFERENCE_USERS')
alter table ExportRelationLog
   drop constraint FK_EXPORTRE_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PackCodeImportLog') and o.name = 'FK_PACKCODE_REFERENCE_PACKAGES')
alter table PackCodeImportLog
   drop constraint FK_PACKCODE_REFERENCE_PACKAGES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PackageRule') and o.name = 'FK_PACKAGER_REFERENCE_USERS')
alter table PackageRule
   drop constraint FK_PACKAGER_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PackageRuleItem') and o.name = 'FK_PACKAGER_REFERENCE_PACKAGER')
alter table PackageRuleItem
   drop constraint FK_PACKAGER_REFERENCE_PACKAGER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PackageRuleItem') and o.name = 'FK_PACKAGER_REFERENCE_PRINTTEM')
alter table PackageRuleItem
   drop constraint FK_PACKAGER_REFERENCE_PRINTTEM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PackageRuleItem') and o.name = 'FK_PACKAGER_REFERENCE_PACKCODE')
alter table PackageRuleItem
   drop constraint FK_PACKAGER_REFERENCE_PACKCODE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PackageSpecific') and o.name = 'FK_PACKAGES_REFERENCE_PRODUCT')
alter table PackageSpecific
   drop constraint FK_PACKAGES_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Product') and o.name = 'FK_PRODUCT_REFERENCE_PRODUCTD')
alter table Product
   drop constraint FK_PRODUCT_REFERENCE_PRODUCTD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Product') and o.name = 'FK_PRODUCT_REFERENCE_USERS')
alter table Product
   drop constraint FK_PRODUCT_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductDoc') and o.name = 'FK_PRODUCTD_REFERENCE_USERS')
alter table ProductDoc
   drop constraint FK_PRODUCTD_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLine') and o.name = 'FK_PRODUCTL_REFERENCE_WORKCOMP')
alter table ProductLine
   drop constraint FK_PRODUCTL_REFERENCE_WORKCOMP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineData') and o.name = 'FK_PRODUCTL_REFERENCE_WORKCENT')
alter table ProductLineData
   drop constraint FK_PRODUCTL_REFERENCE_WORKCENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineData') and o.name = 'FK_PRODUCTL_REFERENCE_PRODUCTL')
alter table ProductLineData
   drop constraint FK_PRODUCTL_REFERENCE_PRODUCTL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineData') and o.name = 'FK_PRODUCTL_REFERENCE_TASK')
alter table ProductLineData
   drop constraint FK_PRODUCTL_REFERENCE_TASK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineData') and o.name = 'FK_PRODUCTL_REFERENCE_PACKAGES')
alter table ProductLineData
   drop constraint FK_PRODUCTL_REFERENCE_PACKAGES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineRelationship') and o.name = 'FK_PRODUCTLINE_REFERENCE_PRODUCTLINERELATIONSHIP')
alter table ProductLineRelationship
   drop constraint FK_PRODUCTLINE_REFERENCE_PRODUCTLINERELATIONSHIP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineRelationship') and o.name = 'FK_PRODUCTL_REFERENCE_PRODUCT')
alter table ProductLineRelationship
   drop constraint FK_PRODUCTL_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductLineRelationship') and o.name = 'FK_PRODUCTL_REFERENCE_USERS')
alter table ProductLineRelationship
   drop constraint FK_PRODUCTL_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QualityCheckRecord') and o.name = 'FK_QUALITYCHECKRECORD_CREATUSER_REFERENCE_USERS')
alter table QualityCheckRecord
   drop constraint FK_QUALITYCHECKRECORD_CREATUSER_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QualityCheckRecord') and o.name = 'FK_QUALITYCHECKRECORD_AUDITUSER_REFERENCE_USERS')
alter table QualityCheckRecord
   drop constraint FK_QUALITYCHECKRECORD_AUDITUSER_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QualityCheckRecordItem') and o.name = 'FK_QUALITYC_REFERENCE_TASK')
alter table QualityCheckRecordItem
   drop constraint FK_QUALITYC_REFERENCE_TASK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QualityCheckRecordItem') and o.name = 'FK_QUALITYC_REFERENCE_QUALITYC')
alter table QualityCheckRecordItem
   drop constraint FK_QUALITYC_REFERENCE_QUALITYC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ResourceCode') and o.name = 'FK_RESOURCE_REFERENCE_PRODUCT')
alter table ResourceCode
   drop constraint FK_RESOURCE_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ResourceCode') and o.name = 'FK_RESOURCE_REFERENCE_PACKAGES')
alter table ResourceCode
   drop constraint FK_RESOURCE_REFERENCE_PACKAGES
go



if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Task') and o.name = 'FK_TASK_REFERENCE_PRODUCTLINE')
alter table Task
   drop constraint FK_TASK_REFERENCE_PRODUCTLINE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Task') and o.name = 'FK_TASK_REFERENCE_PACKAGER')
alter table Task
   drop constraint FK_TASK_REFERENCE_PACKAGER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Task') and o.name = 'FK_TASK_REFERENCE_PACKAGES')
alter table Task
   drop constraint FK_TASK_REFERENCE_PACKAGES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Task') and o.name = 'FK_TASK_REFERENCE_PRODUCTM')
alter table Task
   drop constraint FK_TASK_REFERENCE_PRODUCTM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Task') and o.name = 'FK_TASK_REFERENCE_PRODUCT')
alter table Task
   drop constraint FK_TASK_REFERENCE_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('TaskPrintBatch') and o.name = 'FK_TASKPRIN_REFERENCE_TASK')
alter table TaskPrintBatch
   drop constraint FK_TASKPRIN_REFERENCE_TASK
go


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkCenter') and o.name = 'FK_WORKCENT_REFERENCE_PRODUCTL')
alter table WorkCenter
   drop constraint FK_WORKCENT_REFERENCE_PRODUCTL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkCenterData') and o.name = 'FK_WORKCENT_REFERENCE_WORKCENTERDATA')
alter table WorkCenterData
   drop constraint FK_WORKCENT_REFERENCE_WORKCENTERDATA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkCenterData') and o.name = 'FK_WORKCENT_REFERENCE_TASK')
alter table WorkCenterData
   drop constraint FK_WORKCENT_REFERENCE_TASK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkCenterEquipment') and o.name = 'FK_WORKCENT_REFERENCE_EQUIPMEN')
alter table WorkCenterEquipment
   drop constraint FK_WORKCENT_REFERENCE_EQUIPMEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkCenterEquipment') and o.name = 'FK_WORKCENT_REFERENCE_WORKCENT')
alter table WorkCenterEquipment
   drop constraint FK_WORKCENT_REFERENCE_WORKCENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Equipment')
            and   type = 'U')
   drop table Equipment
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EquipmentRelation')
            and   type = 'U')
   drop table EquipmentRelation
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EquipmentSetting')
            and   type = 'U')
   drop table EquipmentSetting
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ExportPrintLog')
            and   type = 'U')
   drop table ExportPrintLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ExportRelationLog')
            and   type = 'U')
   drop table ExportRelationLog
go



if exists (select 1
            from  sysobjects
           where  id = object_id('PackCodeImportLog')
            and   type = 'U')
   drop table PackCodeImportLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PackCodeRule')
            and   type = 'U')
   drop table PackCodeRule
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PackageRule')
            and   type = 'U')
   drop table PackageRule
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PackageRuleItem')
            and   type = 'U')
   drop table PackageRuleItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PackageSpecific')
            and   type = 'U')
   drop table PackageSpecific
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PrintTemplate')
            and   type = 'U')
   drop table PrintTemplate
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PrintTemplateParam')
            and   type = 'U')
   drop table PrintTemplateParam
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Product')
            and   type = 'U')
   drop table Product
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductDoc')
            and   type = 'U')
   drop table ProductDoc
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductLine')
            and   type = 'U')
   drop table ProductLine
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductLineData')
            and   type = 'U')
   drop table ProductLineData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductLineRelationship')
            and   type = 'U')
   drop table ProductLineRelationship
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductMaterials')
            and   type = 'U')
   drop table ProductMaterials
go

if exists (select 1
            from  sysobjects
           where  id = object_id('QualityCheckRecord')
            and   type = 'U')
   drop table QualityCheckRecord
go

if exists (select 1
            from  sysobjects
           where  id = object_id('QualityCheckRecordItem')
            and   type = 'U')
   drop table QualityCheckRecordItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ResourceCode')
            and   type = 'U')
   drop table ResourceCode
go




if exists (select 1
            from  sysobjects
           where  id = object_id('Task')
            and   type = 'U')
   drop table Task
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TaskPrintBatch')
            and   type = 'U')
   drop table TaskPrintBatch
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TaskScheduler')
            and   type = 'U')
   drop table TaskScheduler
go



if exists (select 1
            from  sysobjects
           where  id = object_id('WorkCenter')
            and   type = 'U')
   drop table WorkCenter
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkCenterData')
            and   type = 'U')
   drop table WorkCenterData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkCenterEquipment')
            and   type = 'U')
   drop table WorkCenterEquipment
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkComputer')
            and   type = 'U')
   drop table WorkComputer
go

/*==============================================================*/
/* Table: Equipment                                             */
/*==============================================================*/
create table Equipment (
   ID                   numeric              identity,
   EquipmentSettingID   numeric              null,
   EquipmentCode        nvarchar(60)         null,
   IP                   nvarchar(20)         null,
   Port                 nvarchar(10)         null,
   BaudRate             smallint             null,
   DataBits             smallint             null,
   StopBits             smallint             null,
   Parity               smallint             null,
   PropertyObj          nvarchar(Max)        null,
   Remark               nvarchar(200)        null,
   constraint PK_EQUIPMENT primary key (ID)
)
go

/*==============================================================*/
/* Table: EquipmentRelation                                     */
/*==============================================================*/
create table EquipmentRelation (
   InputEquipmentID     numeric              null,
   OutputEquipmentID    numeric              null,
   "Order"              smallint             null,
   SleepTime            int                  null,
   RuleExp              nvarchar(50)         null
)
go

/*==============================================================*/
/* Table: EquipmentSetting                                      */
/*==============================================================*/
create table EquipmentSetting (
   ID                   numeric              identity,
   Category             smallint             null,
   InOutType            smallint             null,
   DriverType           smallint             null,
   Name                 nvarchar(60)         null,
   FullClassName        nvarchar(Max)        null,
   PropertyObj          nvarchar(Max)        null,
   Status               smallint             null,
   constraint PK_EQUIPMENTSETTING primary key (ID)
)
go

/*==============================================================*/
/* Table: ExportPrintLog                                        */
/*==============================================================*/
create table ExportPrintLog (
   ExportUserID         numeric              null,
   ExportDate           datetime             null,
   ProductID            numeric              null,
   PackageSpecID        numeric              null,
   PlanAmout            int                  null,
   Amount               int                  null,
   LevelNo              smallint             null,
   PrintBatchNo         nvarchar(60)         null,
   FileName             nvarchar(Max)        null,
   DownloadTimes        smallint             null,
   IsSuccess            bit                  null,
   ExportMessage        nvarchar(Max)        null
)
go

/*==============================================================*/
/* Table: ExportRelationLog                                     */
/*==============================================================*/
create table ExportRelationLog (
   ID                   numeric              identity,
   ExportUserID         numeric              null,
   TaskID               numeric              null,
   ExportDate           datetime             null,
   IsReExport           bit                  null,
   FileName             nvarchar(Max)        null,
   RelationInfo         nvarchar(Max)        null,
   DownloadTimes        smallint             null,
   constraint PK_EXPORTRELATIONLOG primary key (ID)
)
go



/*==============================================================*/
/* Table: PackCodeImportLog                                     */
/*==============================================================*/
create table PackCodeImportLog (
   ID                   numeric              identity,
   FileID               nvarchar(500)        null,
   FileHead             nvarchar(100)        null,
   ImportedCount        int                  null,
   ImportFlag           smallint             null,
   ImportMessage        nvarchar(Max)        null,
   FileName             nvarchar(Max)        null,
   ImportDate           datetime             null,
   ImportUserID         numeric              null,
   PackageSpecID        numeric              null,
   LevelNo              smallint             null,
   PrintStatus          smallint             null,
   constraint PK_PACKCODEIMPORTLOG primary key (ID)
)
go

/*==============================================================*/
/* Table: PackCodeRule                                          */
/*==============================================================*/
create table PackCodeRule (
   ID                   numeric              identity,
   RuleCode             nvarchar(60)         null,
   RuleName             nvarchar(60)         null,
   CodeLength           smallint             null,
   Status               smallint             null,
   Description          nvarchar(200)        null,
   StartIndex           smallint             null,
   EndIndex             smallint             null,
   EncryptModule        nvarchar(Max)        null,
   IsEncrypt            bit                  null,
   constraint PK_PACKCODERULE primary key (ID)
)
go

/*==============================================================*/
/* Table: PackageRule                                           */
/*==============================================================*/
create table PackageRule (
   ID                   numeric              identity,
   PackageRuleName      nvarchar(60)         null,
   LevelCount           smallint             null,
   Status               smallint             null,
   CascadeCode          nvarchar(20)         null,
   CreateUserID         numeric              null,
   CreateDate           datetime             null,
   Remark               nvarchar(Max)        null,
   constraint PK_PACKAGERULE primary key (ID)
)
go

/*==============================================================*/
/* Table: PackageRuleItem                                       */
/*==============================================================*/
create table PackageRuleItem (
   ID                   numeric              identity,
   PackageRuleID        numeric              null,
   LevelNo              smallint             null,
   Amount               smallint             null,
   VirtualAmount        smallint             null,
   PrintAmount          smallint             null,
   PrintTemplateID      numeric              null,
   PackCodeRuleID       numeric              null,
   CodeAmount           smallint             null,
   Remark               nvarchar(Max)        null,
   constraint PK_PACKAGERULEITEM primary key (ID)
)
go

/*==============================================================*/
/* Table: PackageSpecific                                       */
/*==============================================================*/
create table PackageSpecific (
   ID                   numeric              identity,
   PackageRatio         nvarchar(60)         null,
   PackageSpec          nvarchar(60)         null,
   ProductID            numeric              null,
   Weight               decimal(8,2)         null,
   Remark1              nvarchar(Max)        null,
   Remark2              nvarchar(Max)        null,
   constraint PK_PACKAGESPECIFIC primary key (ID)
)
go

/*==============================================================*/
/* Table: PrintTemplate                                         */
/*==============================================================*/
create table PrintTemplate (
   ID                   numeric              identity,
   TemplateName         nvarchar(60)         null,
   Width                smallint             null,
   Height               smallint             null,
   DesignData           image                null,
   Detail               nvarchar(200)        null,
   CreateDate           datetime             null,
   UpdateDate           datetime             null,
   constraint PK_PRINTTEMPLATE primary key (ID)
)
go

/*==============================================================*/
/* Table: PrintTemplateParam                                    */
/*==============================================================*/
create table PrintTemplateParam (
   ID                   numeric              identity,
   PrintItemType        nvarchar(60)         null,
   BindProperty         nvarchar(60)         null,
   绑定参数名称               nvarchar(60)         null,
   OrderNo              smallint             null,
   CreateDate           datetime             null,
   constraint PK_PRINTTEMPLATEPARAM primary key (ID)
)
go

/*==============================================================*/
/* Table: Product                                               */
/*==============================================================*/
create table Product (
   id                   numeric              identity,
   ProductCode          varchar(60)          not null,
   ProductName          nvarchar(60)         not null,
   SubNo                nvarchar(60)         null,
   SubName              nvarchar(60)         null,
   Status               smallint             null,
   ProductType          nvarchar(60)         null,
   Spec                 nvarchar(60)         null,
   PermitNo             nvarchar(60)         null,
   PackSpec             nvarchar(60)         null,
   GetCodeType          smallint             null,
   ProductCategory      smallint             null,
   CodeLen              smallint             null,
   DetailType           smallint             null,
   DocID                numeric              null,
   "Order"              smallint             null,
   ValidTime            smallint             null,
   ValidTimeUnit        smallint             null,
   PackUnit             nvarchar(60)         null,
   CreateUserID           numeric              null,
   CreateDate           datetime             null,
   Remark               nvarchar(Max)        null,
   constraint PK_PRODUCT primary key (id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品表',
   'user', @CurrentUser, 'table', 'Product'
go

/*==============================================================*/
/* Table: ProductDoc                                            */
/*==============================================================*/
create table ProductDoc (
   ID                   numeric              identity,
   DocCode              nvarchar(60)         null,
   DocName              nvarchar(60)         null,
   CreateUserID         numeric              null,
   UpdateTime           datetime             null,
   Remark               nvarchar(200)        null,
   constraint PK_PRODUCTDOC primary key (ID)
)
go

/*==============================================================*/
/* Table: ProductLine                                           */
/*==============================================================*/
create table ProductLine (
   ID                   numeric              identity,
   WorkComputerID       numeric              null,
   LineCode             nvarchar(60)         null,
   LineName             nvarchar(60)         null,
   Status               smallint             null,
   ManageUser           nvarchar(60)         null,
   Remark               nvarchar(200)        null,
   WorkShop             nvarchar(60)         null,
   constraint PK_PRODUCTLINE primary key (ID)
)
go

/*==============================================================*/
/* Table: ProductLineData                                       */
/*==============================================================*/
create table ProductLineData (
   ID                   numeric              identity,
   ProductLineId        numeric              null,
   WorkCenterId         numeric              null,
   PackageSpecificId    numeric              null,
   TaskId               numeric              null,
   LastCode             nvarchar(60)         null,
   Remark               nvarchar(200)        null,
   constraint PK_PRODUCTLINEDATA primary key (ID)
)
go

/*==============================================================*/
/* Table: ProductLineRelationship                               */
/*==============================================================*/
create table ProductLineRelationship (
   ID                   numeric              identity,
   ProductLineId        numeric              null,
   UserId               numeric              null,
   ProductId            numeric              null,
   constraint PK_PRODUCTLINERELATIONSHIP primary key (ID)
)
go

/*==============================================================*/
/* Table: ProductMaterials                                      */
/*==============================================================*/
create table ProductMaterials (
   ID                   numeric              identity,
   PackageSpecID        numeric              null,
   MaterialNo           nvarchar(60)         null,
   MaterialName         nvarchar(60)         null,
   TrayCount            smallint             null,
   Remark               nvarchar(Max)        null,
   constraint PK_PRODUCTMATERIALS primary key (ID)
)
go

/*==============================================================*/
/* Table: QualityCheckRecord                                    */
/*==============================================================*/
create table QualityCheckRecord (
   ID                   numeric              identity,
   RecordName           nvarchar(60)         null,
   Status               smallint             null,
   CheckDate            datetime             null,
   CreateUserID         numeric              null,
   CreateDate           datetime             null,
   AuditUserID          numeric              null,
   AuditDate            datetime             null,
   constraint PK_QUALITYCHECKRECORD primary key (ID)
)
go

/*==============================================================*/
/* Table: QualityCheckRecordItem                                */
/*==============================================================*/
create table QualityCheckRecordItem (
   ID                   numeric              identity,
   RecordID             numeric              null,
   PackCode             nvarchar(60)         null,
   ReplacePackCode      nvarchar(60)         null,
   PackageCode          nvarchar(60)         null,
   LevelNo              smallint             null,
   TaskID               numeric              null,
   constraint PK_QUALITYCHECKRECORDITEM primary key (ID)
)
go

/*==============================================================*/
/* Table: ResourceCode                                          */
/*==============================================================*/
create table ResourceCode (
   ID                   numeric              identity,
   LevelNo              smallint             null,
   CodeVersion          nvarchar(60)         null,
   ProdCode             nvarchar(60)         null,
   PackageSpecID        numeric              null,
   ProductID            numeric              null,
   constraint PK_RESOURCECODE primary key (ID)
)
go







/*==============================================================*/
/* Table: Task                                                  */
/*==============================================================*/
create table Task (
   ID                   numeric              identity,
   ProductLineID        numeric              null,
   TaskCode             numeric              null,
   Status               smallint             null,
   PackageRuleID        numeric              null,
   ProductOrderID       bigint               null,
   ProductID            numeric              null,
   ProductMaterialID    numeric              null,
   PackageSpecID        numeric              null,
   BatchNo              nvarchar(60)         null,
   PackageBatchNo       nvarchar(60)         null,
   SubBatchNo           nvarchar(60)         null,
   GroupNo              nvarchar(60)         null,
   PlanAmount           bigint               null,
   TaskAmount           bigint               null,
   DoneAmount           bigint               null,
   CreateUserID         numeric              null,
   CreateDate           datetime             null,
   StartDate            datetime             null,
   EndDate              datetime             null,
   ProductDate          datetime             null,
   InvalidDate          datetime             null,
   AuditUser            numeric              null,
   AuditDate            datetime             null,
   TaskGuid             nvarchar(60)         null,
   Remark               nvarchar(200)        null,
   constraint PK_TASK primary key (ID)
)
go

/*==============================================================*/
/* Table: TaskPrintBatch                                        */
/*==============================================================*/
create table TaskPrintBatch (
   TaskID               numeric              null,
   PrintBatchNo         nvarchar(60)         null
)
go

/*==============================================================*/
/* Table: TaskScheduler                                         */
/*==============================================================*/
create table TaskScheduler (
   ID                   numeric              identity,
   SchedulerName        nvarchar(60)         null,
   SchedulerStatus      smallint             null,
   SchedulerType        smallint             null,
   Year                 nvarchar(4)          null,
   Month                nvarchar(2)          null,
   DayOfMonth           nvarchar(2)          null,
   DayOfWeek            nvarchar(1)          null,
   Hour                 nvarchar(2)          null,
   Minute               nvarchar(2)          null,
   Second               nvarchar(2)          null,
   TaskName             nvarchar(60)         null,
   TaskFullClassName    nvarchar(Max)        null,
   TaskParameter        nvarchar(Max)        null,
   CreateUserID         numeric              null,
   CreateDate           datetime             null,
   UpdateDate           datetime             null,
   Remark               nvarchar(200)        null,
   constraint PK_TASKSCHEDULER primary key (ID)
)
go




/*==============================================================*/
/* Table: WorkCenter                                            */
/*==============================================================*/
create table WorkCenter (
   ID                   numeric              identity,
   ProductLineID        numeric              null,
   WorkCenterCode       nvarchar(60)         null,
   LevelNo              smallint             null,
   Remark               nvarchar(Max)        null,
   PropertyObj          nvarchar(Max)        null,
   X                    nvarchar(10)         null,
   Y                    nvarchar(10)         null,
   PreWorkCenterID      numeric              null,
   PostWorkCenterID     numeric              null,
   constraint PK_WORKCENTER primary key (ID)
)
go

/*==============================================================*/
/* Table: WorkCenterData                                        */
/*==============================================================*/
create table WorkCenterData (
   TaskID               numeric              not null,
   WorkCenterId         numeric              null,
   CurrentNum           int                  null,
   FinishedNum          int                  null,
   PackageNum           int                  null,
   KickoutNum           int                  null,
   EnforcePackageNum    int                  null,
   BoxNum               int                  null,
   PackageFlag          smallint             null,
   Remark               nvarchar(200)        null
)
go

/*==============================================================*/
/* Table: WorkCenterEquipment                                   */
/*==============================================================*/
create table WorkCenterEquipment (
   WorkCenterID         numeric              null,
   EquipmentID          numeric              null,
   X                    nvarchar(60)         null,
   Y                    nvarchar(60)         null
)
go

/*==============================================================*/
/* Table: WorkComputer                                          */
/*==============================================================*/
create table WorkComputer (
   ID                   numeric              identity,
   WorkComputerName     nvarchar(60)         null,
   ComputerIP           nvarchar(60)         null,
   HardwareConfig       nvarchar(60)         null,
   CreateDate           datetime             null,
   constraint PK_WORKCOMPUTER primary key (ID)
)
go

alter table Equipment
   add constraint FK_EQUIPMEN_REFERENCE_EQUIPMEN foreign key (EquipmentSettingID)
      references EquipmentSetting (ID)
go

alter table ExportPrintLog
   add constraint FK_EXPORTPR_REFERENCE_PACKAGES foreign key (PackageSpecID)
      references PackageSpecific (ID)
go

alter table ExportPrintLog
   add constraint FK_EXPORTPR_REFERENCE_USERS foreign key (ExportUserID)
      references Users (id)
go

alter table ExportPrintLog
   add constraint FK_EXPORTPR_REFERENCE_PRODUCT foreign key (ProductID)
      references Product (id)
go

alter table ExportRelationLog
   add constraint FK_EXPORTRE_REFERENCE_TASK foreign key (TaskID)
      references Task (ID)
go

alter table ExportRelationLog
   add constraint FK_EXPORTRE_REFERENCE_USERS foreign key (ExportUserID)
      references Users (id)
go

alter table PackCodeImportLog
   add constraint FK_PACKCODE_REFERENCE_PACKAGES foreign key (PackageSpecID)
      references PackageSpecific (ID)
go

alter table PackageRule
   add constraint FK_PACKAGER_REFERENCE_USERS foreign key (CreateUserID)
      references Users (id)
go

alter table PackageRuleItem
   add constraint FK_PACKAGER_REFERENCE_PACKAGER foreign key (PackageRuleID)
      references PackageRule (ID)
go

alter table PackageRuleItem
   add constraint FK_PACKAGER_REFERENCE_PRINTTEM foreign key (PrintTemplateID)
      references PrintTemplate (ID)
go

alter table PackageRuleItem
   add constraint FK_PACKAGER_REFERENCE_PACKCODE foreign key (PackCodeRuleID)
      references PackCodeRule (ID)
go

alter table PackageSpecific
   add constraint FK_PACKAGES_REFERENCE_PRODUCT foreign key (ProductID)
      references Product (id)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_PRODUCTD foreign key (DocID)
      references ProductDoc (ID)
go

alter table Product
   add constraint FK_PRODUCT_REFERENCE_USERS foreign key (CreateUserID)
      references Users (id)
go

alter table ProductDoc
   add constraint FK_PRODUCTD_REFERENCE_USERS foreign key (CreateUserID)
      references Users (id)
go

alter table ProductLine
   add constraint FK_PRODUCTL_REFERENCE_WORKCOMP foreign key (WorkComputerID)
      references WorkComputer (ID)
go

alter table ProductLineData
   add constraint FK_PRODUCTL_REFERENCE_WORKCENT foreign key (WorkCenterId)
      references WorkCenter (ID)
go

alter table ProductLineData
   add constraint FK_PRODUCTL_REFERENCE_PRODUCTL foreign key (ProductLineId)
      references ProductLine (ID)
go

alter table ProductLineData
   add constraint FK_PRODUCTL_REFERENCE_TASK foreign key (TaskId)
      references Task (ID)
go

alter table ProductLineData
   add constraint FK_PRODUCTL_REFERENCE_PACKAGES foreign key (PackageSpecificId)
      references PackageSpecific (ID)
go

alter table ProductLineRelationship
   add constraint FK_PRODUCTLINE_REFERENCE_PRODUCTLINERELATIONSHIP foreign key (ProductLineId)
      references ProductLine (ID)
go

alter table ProductLineRelationship
   add constraint FK_PRODUCTL_REFERENCE_PRODUCT foreign key (ProductId)
      references Product (id)
go

alter table ProductLineRelationship
   add constraint FK_PRODUCTL_REFERENCE_USERS foreign key (UserId)
      references Users (id)
go

alter table QualityCheckRecord
   add constraint FK_QUALITYCHECKRECORD_CREATUSER_REFERENCE_USERS foreign key (CreateUserID)
      references Users (id)
go

alter table QualityCheckRecord
   add constraint FK_QUALITYCHECKRECORD_AUDITUSER_REFERENCE_USERS foreign key (AuditUserID)
      references Users (id)
go

alter table QualityCheckRecordItem
   add constraint FK_QUALITYC_REFERENCE_TASK foreign key (TaskID)
      references Task (ID)
go

alter table QualityCheckRecordItem
   add constraint FK_QUALITYC_REFERENCE_QUALITYC foreign key (RecordID)
      references QualityCheckRecord (ID)
go

alter table ResourceCode
   add constraint FK_RESOURCE_REFERENCE_PRODUCT foreign key (ProductID)
      references Product (id)
go

alter table ResourceCode
   add constraint FK_RESOURCE_REFERENCE_PACKAGES foreign key (PackageSpecID)
      references PackageSpecific (ID)
go



alter table Task
   add constraint FK_TASK_REFERENCE_PRODUCTLINE foreign key (ProductLineID)
      references ProductLine (ID)
go

alter table Task
   add constraint FK_TASK_REFERENCE_PACKAGER foreign key (PackageRuleID)
      references PackageRule (ID)
go

alter table Task
   add constraint FK_TASK_REFERENCE_PACKAGES foreign key (PackageSpecID)
      references PackageSpecific (ID)
go

alter table Task
   add constraint FK_TASK_REFERENCE_PRODUCTM foreign key (ProductMaterialID)
      references ProductMaterials (ID)
go

alter table Task
   add constraint FK_TASK_REFERENCE_PRODUCT foreign key (ProductID)
      references Product (id)
go

alter table TaskPrintBatch
   add constraint FK_TASKPRIN_REFERENCE_TASK foreign key (TaskID)
      references Task (ID)
go


alter table WorkCenter
   add constraint FK_WORKCENT_REFERENCE_PRODUCTL foreign key (ProductLineID)
      references ProductLine (ID)
go

alter table WorkCenterData
   add constraint FK_WORKCENT_REFERENCE_WORKCENTERDATA foreign key (WorkCenterId)
      references WorkCenter (ID)
go

alter table WorkCenterData
   add constraint FK_WORKCENT_REFERENCE_TASK foreign key (TaskID)
      references Task (ID)
go

alter table WorkCenterEquipment
   add constraint FK_WORKCENT_REFERENCE_EQUIPMEN foreign key (EquipmentID)
      references Equipment (ID)
go

alter table WorkCenterEquipment
   add constraint FK_WORKCENT_REFERENCE_WORKCENT foreign key (WorkCenterID)
      references WorkCenter (ID)
go

