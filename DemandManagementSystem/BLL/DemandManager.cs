using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class DemandManager
    {
        /// <summary>
        /// 根据产品名称查询需求详情
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public List<DemandInfo> GetDemandInfoByProductName(string ProductName)
        {
            return new DAL.DemandService().GetDemandInfoByProductName(ProductName);
        }
        /// <summary>
        /// 发布需求
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int AddDemandInfo(DemandInfo objDemand)
        {
            return new DAL.DemandService().AddDemandInfo(objDemand);
        }
        /// <summary>
        /// 查询产品名称
        /// </summary>
        /// <returns></returns>
        public List<Products> GetProductName()
        {
            //.GetProductName();
            return new DAL.DemandService().GetProductList();
        }

        /// <summary>
        /// 查询紧急程度名称
        /// </summary>
        /// <returns></returns>
        public List<EmergencyDegree> GetEmergencyName()
        {
            return new DAL.DemandService().GetEmergencyName();
        }

        /// <summary>
        /// 发布需求
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int PublishDemandInfo(DemandInfo objDemand)
        {
            return new DAL.DemandService().AddDemandInfo(objDemand);
        }

        /// <summary>
        /// 根据需求编号查询需求详情
        /// </summary>
        /// <param name="DemandId">需求编号</param>
        /// <returns></returns>
        public DemandInfo GetDemandDetailByDemandId(int DemandInfoId)
        {
            return new DAL.DemandService().GetDemandDetailByDemandId(DemandInfoId);
        }

        /// <summary>
        /// 根据需求编号查询需求详情2
        /// </summary>
        /// <param name="DemandId">需求编号</param>
        /// <returns></returns>
        public DemandInfo GetDemandDetailByConfirmationPerson(int DemandInfoId)
        {
            return new DAL.DemandService().GetDemandDetailByConfirmationPerson(DemandInfoId);
        }

            /// <summary>
            /// 根据需求详情ID编号修改需求
            /// </summary>
            /// <param name="objDemand"></param>
            /// <returns></returns>
            public int ModifyDemandInfoByDemandId(DemandInfo objDemand)
        {
            return new DAL.DemandService().ModifyDemandInfoByDemandId(objDemand);
        }

        /// <summary>
        /// 认领需求
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int ConfirmDemand(DemandInfo objDemand)
        {
            return new DAL.DemandService().ConfirmDemand(objDemand);
        }

        /// <summary>
        /// 根据确认人编号查询需求详情
        /// </summary>
        /// <param name="ConfirmationPerson"></param>
        /// <returns></returns>
        public List<DemandInfo> GetDemandInfoByConfirmationPerson(int ConfirmationPerson)
        {
            return new DAL.DemandService().GetDemandInfoByConfirmationPerson(ConfirmationPerson);
        }

        /// <summary>
        /// 确认上线
        /// </summary>
        /// <param name="objDemand"></param>
        /// <returns></returns>
        public int UpLineDemand(DemandInfo objDemand)
        {
           return new DAL.DemandService().UpLineDemand(objDemand);
        }


        }
}
