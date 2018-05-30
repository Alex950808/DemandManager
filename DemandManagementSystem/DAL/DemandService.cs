using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL.Helper;
using DAL.Extensions;

namespace DAL
{
    public class DemandService
    {
        /// <summary>
        /// 根据产品名称查询需求详情
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public List<DemandInfo> GetDemandInfoByProductName(string ProductName)
        {
            string sql = $"select ConfirmTime,DemandId,ProductName,SubmissionDepartment, Founder,Users.UserName,PhoneNumber,Email,EmergencyName from DemandInfo left join Users on Users.UserId=DemandInfo.Founder left join Products on Products.ProductId=DemandInfo.ProductId left join EmergencyDegree on EmergencyDegree.EmergencyId=DemandInfo.EmergencyId where ProductName like @name";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@name",  $"%{ProductName}%")
            };

            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            List<DemandInfo> list = new List<DemandInfo>();
            while (objReader.Read())
            {
                list.Add(new DemandInfo()
                {
                    ConfirmTime = (objReader["ConfirmTime"]?.ToString()).ConvertReaderDateTime(),
                    EmergencyName = objReader["EmergencyName"].ToString(),
                    DemandId = Convert.ToInt32(objReader["DemandId"]),
                    ProductName = objReader["ProductName"].ToString(),
                    SubmissionDepartment = objReader["SubmissionDepartment"].ToString(),
                    Founder = Convert.ToInt32(objReader["Founder"]),
                    UserName = objReader["UserName"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Email = objReader["Email"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 发布需求
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int AddDemandInfo(DemandInfo objDemand)
        {
            string sql = $"insert into DemandInfo(ProductId,SubmissionDepartment,RequirementDescription,EmergencyId,Founder,CreationTime) values(@ProductId,@SubmissionDepartment,@RequirementDescription,@EmergencyId,@Founder,@CreationTime)";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@ProductId", objDemand.ProductId),
                new SqlParameter("@SubmissionDepartment", objDemand.SubmissionDepartment),
                new SqlParameter("@RequirementDescription", objDemand.RequirementDescription),
                new SqlParameter("@EmergencyId", objDemand.EmergencyId),
                new SqlParameter("@Founder", objDemand.Founder),
                new SqlParameter("@CreationTime", objDemand.CreationTime)
            };
            return SQLHelper.Update(sql, paras);
        }
        /// <summary>
        /// 查询产品名称
        /// </summary>
        /// <returns></returns>
        public List<Products> GetProductList()
        {
            string sql = $"select ProductId,ProductName from Products";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Products> list = new List<Products>();
            while (objReader.Read())
            {
                list.Add(new Products()
                {
                    ProductId = Convert.ToInt32(objReader["ProductId"]),
                    ProductName = objReader["ProductName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 查询紧急程度名称
        /// </summary>
        /// <returns></returns>
        public List<EmergencyDegree> GetEmergencyName()
        {
            string sql = $"select EmergencyId, EmergencyName from EmergencyDegree";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<EmergencyDegree> list = new List<EmergencyDegree>();
            while (objReader.Read())
            {
                list.Add(new EmergencyDegree()
                {
                    EmergencyId = Convert.ToInt32(objReader["EmergencyId"]),
                    EmergencyName = objReader["EmergencyName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 根据需求编号查询需求详情
        /// </summary>
        /// <param name="DemandId">需求编号</param>
        /// <returns></returns>
        public DemandInfo GetDemandDetailByDemandId(int DemandInfoId)
        {
            string sql = @"select d.[DemandId],d.ProductId,p.ProductName,d.[SubmissionDepartment],d.[RequirementDescription],
d.EmergencyId, e.EmergencyName, d.Founder,u.UserName, d.[CreationTime], 
d.Modifier,ModifierName=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.Modifier), d.ModificationTime, 
d.ConfirmationPerson, ConfirmationName=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.ConfirmationPerson),
d.ConfirmTime, d.ExpectedOnlineTime, d.PlanContent,
d.Publisher,Upline=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.Publisher),
d.ScheduleTime, d.UpTime from DemandInfo AS d
left join EmergencyDegree AS e on d.EmergencyId = e.EmergencyId
left join Products AS p on d.ProductId = p.ProductId
left join Users AS u on d.Founder = u.UserId 
 where [DemandId]=@DemandInfoId";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@DemandInfoId", DemandInfoId)
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            DemandInfo demand = null;
            if (objReader.Read())
            {
                //DemandInfo entity = new DemandInfo();
                //demand = entity;
                //entity.DemandId = Convert.ToInt32(objReader["DemandId"]);
                //entity.ProductId = Convert.ToInt32(objReader["ProductId"]);
                //if (objReader["DemandId"] is DBNull){
                //    entity.ModificationTime = null;
                //}
                //else
                //{
                //    entity.ModificationTime = Convert.ToDateTime(objReader["DemandId"]);
                //}
                //if (objReader["ProductId"] is DBNull)
                //{
                //    entity.ModificationTime = null;
                //}
                //else
                //{
                //    entity.ModificationTime = Convert.ToDateTime(objReader["ProductId"]);
                //}


                demand = new DemandInfo()
                {
                    DemandId = ConvertInt(objReader["DemandId"].ToString()),//需求详情编号
                    ProductId = ConvertInt(objReader["ProductId"].ToString()),//产品编号
                    ProductName = objReader["ProductName"]?.ToString(),//产品名称
                    SubmissionDepartment = objReader["SubmissionDepartment"]?.ToString(),//提交部门
                    RequirementDescription = objReader["RequirementDescription"]?.ToString(),//需求描述
                    EmergencyId = ConvertInt(objReader["EmergencyId"].ToString()),//紧急程度编号
                    EmergencyName = objReader["EmergencyName"]?.ToString(),//紧急程度名称
                    Founder = ConvertInt(objReader["Founder"].ToString()),//创建人编号
                    UserName = objReader["UserName"]?.ToString(),//创建人
                    CreationTime = (objReader["CreationTime"]?.ToString()).ConvertReaderDateTime(),//创建时间
                    Modifier = ConvertInt(objReader["Modifier"].ToString()),//修改人编号
                    ModifierName = objReader["ModifierName"]?.ToString(),//修改人
                    ModificationTime = (objReader["ModificationTime"]?.ToString()).ConvertReaderDateTime(),//修改时间
                    ConfirmationPerson = ConvertInt(objReader["ConfirmationPerson"].ToString()),//确认人编号
                    ConfirmationName = objReader["ConfirmationName"]?.ToString(),//确认人
                    ConfirmTime = (objReader["ConfirmTime"]?.ToString()).ConvertReaderDateTime(),//确认时间
                    ExpectedOnlineTime = (objReader["ExpectedOnlineTime"]?.ToString()).ConvertReaderDateTime(),//预计上线时间
                    PlanContent = objReader["PlanContent"]?.ToString(),//上线计划内容
                    Publisher = ConvertInt(objReader["Publisher"].ToString()),//发布人编号
                    Upline = objReader["Upline"]?.ToString(),//发布人
                    ScheduleTime = (objReader["ScheduleTime"]?.ToString()).ConvertReaderDateTime(),//发布时间
                    UpTime = (objReader["UpTime"]?.ToString()).ConvertReaderDateTime(),//上线时间
                };
            }
            objReader.Close();
            return demand;
        }

        /// <summary>
        /// 根据需求编号查询需求详情
        /// </summary>
        /// <param name="DemandId">需求编号</param>
        /// <returns></returns>
        public DemandInfo GetDemandDetailByConfirmationPerson(int DemandInfoId)
        {
            string sql = @"select d.[DemandId],d.ProductId,p.ProductName,d.[SubmissionDepartment],d.[RequirementDescription],
d.EmergencyId, e.EmergencyName, d.Founder,u.UserName, d.[CreationTime], 
d.Modifier,ModifierName=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.Modifier), d.ModificationTime, 
d.ConfirmationPerson, ConfirmationName=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.ConfirmationPerson),
d.ConfirmTime, d.ExpectedOnlineTime, d.PlanContent,
d.Publisher,Upline=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.Publisher),
d.ScheduleTime, d.UpTime from DemandInfo AS d
left join EmergencyDegree AS e on d.EmergencyId = e.EmergencyId
left join Products AS p on d.ProductId = p.ProductId
left join Users AS u on d.Founder = u.UserId 
 where [ConfirmationPerson]=@DemandInfoId";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@DemandInfoId", DemandInfoId)
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            DemandInfo demand = null;
            if (objReader.Read())
            {
                //DemandInfo entity = new DemandInfo();
                //demand = entity;
                //entity.DemandId = Convert.ToInt32(objReader["DemandId"]);
                //entity.ProductId = Convert.ToInt32(objReader["ProductId"]);
                //if (objReader["DemandId"] is DBNull){
                //    entity.ModificationTime = null;
                //}
                //else
                //{
                //    entity.ModificationTime = Convert.ToDateTime(objReader["DemandId"]);
                //}
                //if (objReader["ProductId"] is DBNull)
                //{
                //    entity.ModificationTime = null;
                //}
                //else
                //{
                //    entity.ModificationTime = Convert.ToDateTime(objReader["ProductId"]);
                //}


                demand = new DemandInfo()
                {
                    DemandId = ConvertInt(objReader["DemandId"].ToString()),//需求详情编号
                    ProductId = ConvertInt(objReader["ProductId"].ToString()),//产品编号
                    ProductName = objReader["ProductName"]?.ToString(),//产品名称
                    SubmissionDepartment = objReader["SubmissionDepartment"]?.ToString(),//提交部门
                    RequirementDescription = objReader["RequirementDescription"]?.ToString(),//需求描述
                    EmergencyId = ConvertInt(objReader["EmergencyId"].ToString()),//紧急程度编号
                    EmergencyName = objReader["EmergencyName"]?.ToString(),//紧急程度名称
                    Founder = ConvertInt(objReader["Founder"].ToString()),//创建人编号
                    UserName = objReader["UserName"]?.ToString(),//创建人
                    CreationTime = (objReader["CreationTime"]?.ToString()).ConvertReaderDateTime(),//创建时间
                    Modifier = ConvertInt(objReader["Modifier"].ToString()),//修改人编号
                    ModifierName = objReader["ModifierName"]?.ToString(),//修改人
                    ModificationTime = (objReader["ModificationTime"]?.ToString()).ConvertReaderDateTime(),//修改时间
                    ConfirmationPerson = ConvertInt(objReader["ConfirmationPerson"].ToString()),//确认人编号
                    ConfirmationName = objReader["ConfirmationName"]?.ToString(),//确认人
                    ConfirmTime = (objReader["ConfirmTime"]?.ToString()).ConvertReaderDateTime(),//确认时间
                    ExpectedOnlineTime = (objReader["ExpectedOnlineTime"]?.ToString()).ConvertReaderDateTime(),//预计上线时间
                    PlanContent = objReader["PlanContent"]?.ToString(),//上线计划内容
                    Publisher = ConvertInt(objReader["Publisher"].ToString()),//发布人编号
                    Upline = objReader["Upline"]?.ToString(),//发布人
                    ScheduleTime = (objReader["ScheduleTime"]?.ToString()).ConvertReaderDateTime(),//发布时间
                    UpTime = (objReader["UpTime"]?.ToString()).ConvertReaderDateTime(),//上线时间
                };
            }
            objReader.Close();
            return demand;
        }

        private int ConvertInt(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            else
            {
                int output;
                if (int.TryParse(input, out output))
                {
                    return output;
                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// 根据需求详情ID编号修改需求
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int ModifyDemandInfoByDemandId(DemandInfo objDemand)
        {
            string sql = $"update DemandInfo set [ProductId]=@ProductId, [SubmissionDepartment]=@SubmissionDepartment, [RequirementDescription]=@RequirementDescription, [EmergencyId]=@EmergencyId,Modifier=@Modifier,ModificationTime=@ModificationTime where DemandId=@DemandId";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@ProductId", objDemand.ProductId),
                new SqlParameter("@SubmissionDepartment", objDemand.SubmissionDepartment),
                new SqlParameter("@RequirementDescription", objDemand.RequirementDescription),
                new SqlParameter("@EmergencyId", objDemand.EmergencyId),
                new SqlParameter("@Modifier", objDemand.Modifier),
                new SqlParameter("@ModificationTime", objDemand.ModificationTime),
                new SqlParameter("@DemandId", objDemand.DemandId)
            };
            return SQLHelper.Update(sql, paras);
        }

        /// <summary>
        /// 认领需求
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int ConfirmDemand(DemandInfo objDemand)
        {
            string sql = $"update DemandInfo set PlanContent=@PlanContent,ExpectedOnlineTime=@ExpectedOnlineTime,ConfirmationPerson=@ConfirmationPerson,ConfirmTime=@ConfirmTime where DemandId=@DemandId";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@PlanContent",objDemand.PlanContent),
                new SqlParameter("@ExpectedOnlineTime",objDemand.ExpectedOnlineTime),
                new SqlParameter("@ConfirmationPerson",objDemand.ConfirmationPerson),
                new SqlParameter("@ConfirmTime",objDemand.ConfirmTime),
                new SqlParameter("@DemandId",objDemand.DemandId)
            };
            return SQLHelper.Update(sql, paras);
        }

        /// <summary>
        /// 根据确认人编号查询需求详情
        /// </summary>
        /// <param name="ConfirmationPerson"></param>
        /// <returns></returns>
        public List<DemandInfo> GetDemandInfoByConfirmationPerson(int ConfirmationPerson)
        {
            string sql = @" select d.[DemandId],d.ProductId,p.ProductName,d.[SubmissionDepartment],d.[RequirementDescription],
d.EmergencyId, e.EmergencyName, d.Founder,u.UserName, d.[CreationTime], 
d.Modifier,ModifierName=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.Modifier), d.ModificationTime, 
d.ConfirmationPerson, ConfirmationName=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.ConfirmationPerson),
d.ConfirmTime, d.ExpectedOnlineTime, d.PlanContent,
d.Publisher,Upline=(SELECT ur.UserName FROM Users AS ur WHERE ur.UserId=d.Publisher),
d.ScheduleTime, d.UpTime from DemandInfo AS d
left join EmergencyDegree AS e on d.EmergencyId = e.EmergencyId
left join Products AS p on d.ProductId = p.ProductId
left join Users AS u on d.Founder = u.UserId 
 where ConfirmationPerson=@ConfirmationPerson";
            SqlParameter[] paras = new SqlParameter[]
           {
                new SqlParameter("@ConfirmationPerson", ConfirmationPerson)
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            List<DemandInfo> list = new List<DemandInfo>();

            if (objReader.Read())
            {
                list.Add(new DemandInfo()
                {
                    DemandId = ConvertInt(objReader["DemandId"].ToString()),//需求详情编号
                    ProductId = ConvertInt(objReader["ProductId"].ToString()),//产品编号
                    ProductName = objReader["ProductName"]?.ToString(),//产品名称
                    SubmissionDepartment = objReader["SubmissionDepartment"]?.ToString(),//提交部门
                    RequirementDescription = objReader["RequirementDescription"]?.ToString(),//需求描述
                    EmergencyId = ConvertInt(objReader["EmergencyId"].ToString()),//紧急程度编号
                    EmergencyName = objReader["EmergencyName"]?.ToString(),//紧急程度名称
                    Founder = ConvertInt(objReader["Founder"].ToString()),//创建人编号
                    UserName = objReader["UserName"]?.ToString(),//创建人
                    CreationTime = (objReader["CreationTime"]?.ToString()).ConvertReaderDateTime(),//创建时间
                    Modifier = ConvertInt(objReader["Modifier"].ToString()),//修改人编号
                    ModifierName = objReader["ModifierName"]?.ToString(),//修改人
                    ModificationTime = (objReader["ModificationTime"]?.ToString()).ConvertReaderDateTime(),//修改时间
                    ConfirmationPerson = ConvertInt(objReader["ConfirmationPerson"].ToString()),//确认人编号
                    ConfirmationName = objReader["ConfirmationName"]?.ToString(),//确认人
                    ConfirmTime = (objReader["ConfirmTime"]?.ToString()).ConvertReaderDateTime(),//确认时间
                    ExpectedOnlineTime = (objReader["ExpectedOnlineTime"]?.ToString()).ConvertReaderDateTime(),//预计上线时间
                    PlanContent = objReader["PlanContent"]?.ToString(),//上线计划内容
                    Publisher = ConvertInt(objReader["Publisher"].ToString()),//发布人编号
                    Upline = objReader["Upline"]?.ToString(),//发布人
                    ScheduleTime = (objReader["ScheduleTime"]?.ToString()).ConvertReaderDateTime(),//发布时间
                    UpTime = (objReader["UpTime"]?.ToString()).ConvertReaderDateTime(),//上线时间
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 确认上线
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int UpLineDemand(DemandInfo objDemand)
        {
            string sql = $"update DemandInfo set Publisher=@Publisher,ScheduleTime=@ScheduleTime,UpTime=@UpTime where DemandId=@DemandId";
            SqlParameter[] paras = new SqlParameter[]
           {
                new SqlParameter("@Publisher", objDemand.Publisher),
                new SqlParameter("@ScheduleTime", objDemand.ScheduleTime),
                new SqlParameter("@UpTime", objDemand.UpTime),
                new SqlParameter("@DemandId", objDemand.DemandId),
            };
            return SQLHelper.Update(sql, paras);
        }
    }
}
