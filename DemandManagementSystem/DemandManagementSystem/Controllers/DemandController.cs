using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace DemandManagementSystem.Controllers
{
    public class DemandController : Controller
    {
        // GET: Demand
        public ActionResult Index()
        {
            //接收数据
            string ProductName = "";
            //调用业务逻辑
            List<DemandInfo> DemandList = new BLL.DemandManager().GetDemandInfoByProductName(ProductName);
            //保存数据
            ViewBag.ProductName = ProductName;
            ViewBag.DemandList = DemandList;
            //-----------------------------------------------------------------------------------
            //获取产品名称列表
            List<Products> ProductList = new BLL.DemandManager().GetProductName();
            //获取紧急程度名称列表
            List<EmergencyDegree> EmergencyList = new BLL.DemandManager().GetEmergencyName();
            //保存数据
            Session["ProductList"] = ProductList;
            Session["EmergencyList"] = EmergencyList;
            //------------------------------------------------------------------------------------
            return View("DemandManagement");
        }
        /// <summary>
        /// 根据产品名称查询需求详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDemandInfoByProductName(string ProductName)
        {
            //接收数据
             //= Request.Params["ProductName"];
            //调用业务逻辑
            List<DemandInfo> DemandList = new BLL.DemandManager().GetDemandInfoByProductName(ProductName);
            //保存数据
            ViewBag.ProductName = ProductName;
            ViewBag.DemandList = DemandList;
            //返回视图
            return View("DemandManagement");
        }
        /// <summary>
        /// 跳转到发布需求页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddDemandInfo()
        {
            ////获取产品名称列表
            //List<Products> ProductList = new BLL.DemandManager().GetProductName();
            ////获取紧急程度名称列表
            //List<EmergencyDegree> EmergencyList = new BLL.DemandManager().GetEmergencyName();
            ////保存数据
            //Session["ProductList"] = ProductList;
            //Session["EmergencyList"] = EmergencyList;
            //返回视图
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PublishDemandInfo(DemandInfo objDemand)
        {
            //接收数据
            //DemandInfo objDemand = new DemandInfo()
            //{
            //    ProductId = Convert.ToInt32(Request.Params["ProductId"]),
            //    SubmissionDepartment = Request.Params["SubmissionDepartment"],
            //    RequirementDescription = Request.Params["RequirementDescription"],
            //    EmergencyId = Convert.ToInt32(Request["EmergencyId"]),
            //    Founder = Request.Params["Founder"],
            //    CreationTime = DateTime.Now,
            //    UserId = Convert.ToInt32(Request.Params["UserId"])
            //};
            objDemand.Founder = Convert.ToInt32(Session["UserId"]);
            objDemand.CreationTime = DateTime.Now;

            ViewBag.SubmissionDepartment = objDemand.SubmissionDepartment;
            ViewBag.RequirementDescription = objDemand.RequirementDescription;

            if (string.IsNullOrEmpty(objDemand.SubmissionDepartment))
            {
                ViewBag.Mess = "请填写提交部门";
            }
            else if(string.IsNullOrEmpty(objDemand.RequirementDescription))
            {
                ViewBag.Mess = "请填写需求描述";
            }
            else
            {
                //调用业务逻辑
                int result = new BLL.DemandManager().PublishDemandInfo(objDemand);
                //保存数据
                if (result > 0)
                {
                    return Content("<script>alert('发布成功！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('发布失败！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
                }
            }
            return View("AddDemandInfo");
        }
        
        /// <summary>
        /// 跳转到查看页面
        /// </summary>
        /// <param name="DemandInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LookDemandInfoByDemandId(int DemandInfoId)
        {
            //根据需求编号获取需求数据
            DemandInfo objDemand = new BLL.DemandManager().GetDemandDetailByDemandId(DemandInfoId);
            //返回视图
            return View("DemandDetail", objDemand);
        }

        /// <summary>
        /// 跳转到修改页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdateDemandInfo(int DemandInfoId)
        {
            //根据需求编号获取需求数据
            DemandInfo objDemand = new BLL.DemandManager().GetDemandDetailByDemandId(DemandInfoId);
            //返回视图
            return View("ModifyDemandInfo", objDemand);
        }

        /// <summary>
        /// 修改需求信息
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ModifyDemandInfo(DemandInfo objDemand)
        {
            //DemandInfo objDemand = new DemandInfo()
            //{
            //    DemandId = Convert.ToInt32(Request.Params["DemandId"]),
            //    ProductId = Convert.ToInt32(Request.Params["ProductId"]),
            //    SubmissionDepartment = Request.Params["SubmissionDepartment"],
            //    RequirementDescription = Request.Params["RequirementDescription"],
            //    EmergencyId = Convert.ToInt32(Request.Params["EmergencyId"]),
            //    Modifier = Request.Params["Modifier"],
            //    ModificationTime = DateTime.Now
            //};
            objDemand.Modifier =Convert.ToInt32( Session["UserId"]);
            objDemand.ModificationTime = DateTime.Now;
            if (string.IsNullOrEmpty(objDemand.SubmissionDepartment))
            {
                ViewBag.Mess = "请填写提交部门";
            }
            else if (string.IsNullOrEmpty(objDemand.RequirementDescription))
            {
                ViewBag.Mess = "请填写需求描述";
            }
            else
            {
                //调用业务逻辑
                int result = new BLL.DemandManager().ModifyDemandInfoByDemandId(objDemand);
                //保存数据
                if (result > 0)
                {
                    return Content("<script>alert('修改成功！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
                }
            }
            
            //if (string.IsNullOrEmpty(objDemand.SubmissionDepartment))
            //{
            //    ViewBag.Mess = "提交部门不能为空";
            //}
            //else if (string.IsNullOrEmpty(objDemand.RequirementDescription))
            //{
            //    ViewBag.Mess = "需求描述不能为空";
            //}
            //else
            //{

            //}
            return View("ModifyDemandInfo");
        }

        [HttpGet]
        public ActionResult ConfrimDemand(int DemandInfoId)
        {
            //根据需求编号获取需求数据
            DemandInfo objDemand = new BLL.DemandManager().GetDemandDetailByDemandId(DemandInfoId);
            //返回视图
            return View("ConfrimDemand", objDemand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ConfirmDemandInfo(DemandInfo objDemand)
        {
            //根据需求编号获取需求数据
            var old = objDemand;
            objDemand = new BLL.DemandManager().GetDemandDetailByDemandId(objDemand.DemandId);
            objDemand.PlanContent = old.PlanContent;
            
            objDemand.ExpectedOnlineTime = old.ExpectedOnlineTime;
            //保存数据
            //ViewBag.PlanContent = objDemand.PlanContent;
            //ViewBag.ExpectedOnlineTime = objDemand.ExpectedOnlineTime;

            if (string.IsNullOrEmpty(objDemand.PlanContent))
            {
                ViewBag.M = "上线计划内容不能为空";
            }
            else if (objDemand.ExpectedOnlineTime == null)
            {
                ViewBag.M = "请选择预计上线时间";
            }
            else
            {
                objDemand.ConfirmationPerson = Convert.ToInt32(Session["UserId"]);
                objDemand.ConfirmTime = DateTime.Now;
                //调用业务逻辑
                int result = new BLL.DemandManager().ConfirmDemand(objDemand);
                if (result > 0)
                {
                    return Content("<script>alert('认领成功！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('认领失败！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
                }
            }
            return View("ConfrimDemand", objDemand);
        }
        /// <summary>
        /// 跳转到确认需求页面，根据确认人查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DemandConfrim()
        {
            int ConfirmationPerson =Convert.ToInt32( Session["UserId"]);
            //调用业务逻辑
            List<DemandInfo> objDemandInfo = new BLL.DemandManager().GetDemandInfoByConfirmationPerson(ConfirmationPerson);
            //保存数据
            ViewBag.DemandList = objDemandInfo;
            //返回视图
            return View("DemandConfrim");
        }
        [HttpGet]
        public ActionResult ConfirmDemand()
        {
            //获取数据
            //根据需求编号获取需求数据
            DemandInfo objDemand = new BLL.DemandManager().GetDemandDetailByConfirmationPerson(Convert.ToInt32(Session["RoleId"]));
            //GetDemandInfoByConfirmationPerson
            //返回视图
            return View("Querenxuqiu", objDemand);
        }
        [HttpPost]
        public ActionResult UpLineDemand(DemandInfo objDemand)
        {
            //获取数据
            objDemand.Publisher = Convert.ToInt32(Session["UserId"]);
            objDemand.ScheduleTime = DateTime.Now;
            //调用业务逻辑
            int result = new BLL.DemandManager().UpLineDemand(objDemand);
            if (result > 0)
            {
                return Content("<script>alert('确认成功！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
            }
            else
            {
                return Content("<script>alert('确认失败！');document.location='" + Url.Action("Index", "Demand") + "';</script>");
            }
        }
    }
}