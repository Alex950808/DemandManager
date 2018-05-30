using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace DemandManagementSystem.Controllers
{
    public class IndexController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //返回视图
            return View();
        }

        [HttpGet]
        public ActionResult UserManager()
        {
            //接收数据
            string UserName = "";
            //调用业务逻辑
            List<UserInfo> userList = new BLL.UserInfoManager().GetUserInfoByName(UserName);
            //保存数据
            ViewBag.UserName = UserName;
            ViewBag.UserInfo = userList;
            //返回视图
            return View("AccountManagement");
        }

        //修改密码
        [HttpGet]
        public ActionResult ModifyPwd()
        {
            return RedirectToAction("ModifyPwd");
        }
        [HttpPost]
        public ActionResult GetUserInfoByUserName(string UserName)
        {
            //接收数据
             //= Request.Params["UserName"];
            //调用业务逻辑
            List<UserInfo> userList = new BLL.UserInfoManager().GetUserInfoByName(UserName);
            //保存数据
            ViewBag.UserName = UserName;
            ViewBag.UserInfo = userList;
            //返回视图
            return View("AccountManagement");
        }

        //跳转到新增用户界面
        [HttpGet]
        public ActionResult AddUser()
        {
            //保存调用查询角色信息业务逻辑
            Session["RoleInfo"] = new BLL.UserInfoManager().GetRoleName();
            //ViewBag.RoleInfo = new BLL.UserInfoManager().GetRoleName();
            //返回视图
            return View("AddUser");
        }

        [HttpGet]
        public ActionResult RoleManager()
        {
            //接收数据
            string RoleName = "";
            //调用业务逻辑
            List<RoleInfo> RoleList = new BLL.UserInfoManager().GetRoleNameByName(RoleName);
            //保存数据
            ViewBag.RoleName = RoleName;
            ViewBag.RoleList = RoleList;
            return View("RoleManager");
        }
        [HttpPost]
        public ActionResult GetRoleInfoByRoleName(string RoleName)
        {
            //接收数据
             //= Request.Params["RoleName"];
            //调用业务逻辑
            List<RoleInfo> RoleList = new BLL.UserInfoManager().GetRoleNameByName(RoleName);
            //保存数据
            ViewBag.RoleName = RoleName;
            ViewBag.RoleList = RoleList;
            //返回视图
            return View("RoleManager");
        }
        [HttpPost]
        public ActionResult AddUserInfo(UserInfo objUserInfo)
        {
            //保存数据
            ViewData["UserName"] = objUserInfo.UserName;
            ViewData["LoginId"] = objUserInfo.LoginId;
            ViewData["LoginPwd"] = objUserInfo.LoginPwd;
            ViewData["ConfirmPwd"] = objUserInfo.ConfirmPwd;
            ViewData["PhoneNumber"] = objUserInfo.PhoneNumber;
            ViewData["Email"] = objUserInfo.Email;

            //根据登录账号查询账号是否存在
            int Only = new BLL.UserInfoManager().UniqueAccount(Request.Params["LoginId"]);
            //如果存在的话就==1，否则不存在
            if (Only == 1)
            {
                ViewBag.Info = "此账号已存在！";
            }
            else if (objUserInfo.RoleId<=0)
            {
                ViewBag.Info = "请选择角色类型";
            }
            else if (objUserInfo.LoginPwd!=objUserInfo.ConfirmPwd)
            {
                ViewBag.Info = "两次输出密码不一致";
            }
            else
            {
                //调用业务逻辑
                int result = new BLL.UserInfoManager().AddUserInfo(objUserInfo);
                if (result > 0)
                {
                    return Content("<script>alert('新增成功！');document.location='" + Url.Action("UserManager", "Index") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('新增失败！');document.location='" + Url.Action("UserManager", "Index") + "';</script>");
                }
            }
            return View("AddUser");
        }

        [HttpGet]
        public JsonResult DeleteInfoById(int userId, int userRoleId)
        {
            //调用业务逻辑
            int result = new BLL.UserInfoManager().DeleteInfoByLoginId(userId, userRoleId);
            if (result > 0)
            {
                //return Json(new { success = 1 });
                return new JsonResult()
                {
                    Data = new { success = 1 },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                //return Content("<script>alert('删除成功！');document.location='" + Url.Action("Index", "Index") + "';</script>");

            }
            else
            {
                return Json(new { success = 0 });
                //return Content("<script>alert('删除失败！');document.location='" + Url.Action("Index", "Index") + "';</script>");
            }
        }
        //跳转到查询页面
        [HttpGet]
        public ActionResult LookUserInfoById(int UserRoleId)
        {
            //调用业务逻辑
            UserInfo objUserInfo = new BLL.UserInfoManager().LookUserInfoById(UserRoleId);
            //保存调用查询角色信息业务逻辑
            ViewBag.RoleInfo = new BLL.UserInfoManager().GetRoleName();
            //返回视图
            return View("UserDetail", objUserInfo);
        }

        //跳转到修改页面
        [HttpGet]
        public ActionResult UpdateUserInfo(int UserRoleId)
        {
            //调用业务逻辑
            UserInfo objUserInfo = new BLL.UserInfoManager().LookUserInfoById(UserRoleId);
            //保存调用查询角色信息业务逻辑
            //ViewBag.RoleInfo = new BLL.UserInfoManager().GetRoleName();
            Session["RoleInfo"] = new BLL.UserInfoManager().GetRoleName();
            //返回视图
            return View("ModifyUserInfo", objUserInfo);
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserInfo objUserInfo)
        {
            //获取数据
            //UserInfo objUserInfo = new UserInfo()
            //{
            //    UserName = Request.Params["UserName"],
            //    LoginId = Request.Params["LoginId"],
            //    LoginPwd = Request.Params["LoginPwd"],
            //    PhoneNumber = Request.Params["PhoneNumber"],
            //    Email = Request.Params["Email"],
            //    RoleId = Convert.ToInt32(Request.Params["RoleId"]),
            //    UserId = Convert.ToInt32(Request.Params["UserId"]),
            //    UserRoleId = Convert.ToInt32(Request.Params["UserRoleId"])
            //};

            if (string.IsNullOrEmpty(objUserInfo.UserName))
            {
                ViewBag.Info = "用户名不能为空";
            }
            else if (string.IsNullOrEmpty(objUserInfo.LoginId))
            {
                ViewBag.Info = "登录账号不能为空";
            }
            else if (string.IsNullOrEmpty(objUserInfo.LoginPwd))
            {
                ViewBag.Info = "登录密码不能为空";
            }
            else if (string.IsNullOrEmpty(objUserInfo.PhoneNumber))
            {
                ViewBag.Info = "电话号码不能为空";
            }
            else if (string.IsNullOrEmpty(objUserInfo.Email))
            {
                ViewBag.Info = "电子邮箱不能为空";
            }
            else if (objUserInfo.RoleId <= 0)
            {
                ViewBag.Info = "请选择角色类型";
            }
            else
            {
                //调用业务逻辑
                int result = new BLL.UserInfoManager().UpdateInfoByLoginId(objUserInfo);
                if (result > 0)
                {
                    return Content("<script>alert('修改成功！');document.location='" + Url.Action("UserManager", "Index") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败！');document.location='" + Url.Action("UserManager", "Index") + "';</script>");
                }
            }
            return View("ModifyUserInfo");
        }

    }
}