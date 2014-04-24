select * into detailtypetmp from DetailType
select * into equipmentSettingtmp from EquipmentSetting
select * into ProductCategorytmp from ProductCategory
select * into ProductDocTmp from ProductDoc


INSERT INTO [SMKJ_FM_SYS].[dbo].[ProductCategory]
           ([CategoryCode]
           ,[CategoryName]
           ,[GetCodeType])
    select categorycode,categoryname,getcodetype from ProductCategorytmp
GO


INSERT INTO [SMKJ_FM_SYS].[dbo].[DetailType]
           ([DetailTypeCode]
           ,[DetailTypeName]
           ,[CategoryID])
    select detailtypecode,detailtypename,categoryid from DetailTypetmp
GO

INSERT INTO [SMKJ_FM_SYS].[dbo].[ProductDoc]
           ([DocCode]
           ,[DocName]
           ,[CreateUserID]
           ,[UpdateTime]
           ,[Remark])
    select doccode,docname,createuserid,updatetime,remark from ProductDoctmp
GO




INSERT INTO [SMKJ_FM_SYS].[dbo].[EquipmentSetting]
           ([Category]
           ,[InOutType]
           ,[DriverType]
           ,[Name]
           ,[FullClassName]
           ,[PropertyObj]
           ,[Status])
     select category,inouttype,drivertype,name,fullclassname,propertyobj,status from EquipmentSettingtmp
GO

