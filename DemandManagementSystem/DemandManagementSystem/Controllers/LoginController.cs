using DemandManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;


namespace DemandManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Users objUser, string ValidateCode)
        {
            //获取数据
            //objUser = new Users()
            //{
            //    LoginId = Request.Params["LoginId"].ToString(),
            //    LoginPwd = Request.Params["LoginPwd"]
            //};
            //调用业务逻辑
            ViewBag.LoginId = objUser.LoginId;
            ViewBag.LoginPwd = objUser.LoginPwd;
            if (string.IsNullOrEmpty(objUser.LoginId))
            {
                ViewBag.Login = "用户名不能为空";
            }
            else if (string.IsNullOrEmpty(objUser.LoginPwd))
            {
                ViewBag.Login = "登录密码不能为空";
            }
            else if (string.IsNullOrEmpty(objUser.ValidateCode))
            {
                ViewBag.Login = "验证码不能为空";
            }
            else
            {
                objUser = new BLL.AdminLoginManager().Login(objUser);
                if (objUser != null)
                {
                    if (ModelState.IsValid)
                    {
                        string sourceCode = TempData["ValidateCode"].ToString();
                        //相等的时候为0  其他不同
                        if (string.Compare(sourceCode, ValidateCode, true) != 0)
                        {
                            ModelState.AddModelError("ValidateCode", "验证码不正确,请重新输入");
                            return View("Login");
                        }
                        Session["UserName"] = objUser.UserName;
                        Session["UserId"] = objUser.UserId;
                        Session["RoleId"] = objUser.RoleId;
                        //Session["RoleId"] = objUser.
                        if (objUser.RoleId==3)
                        {
                            return RedirectToAction("UserManager", "Index");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Demand");
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError("ValidateCode", "其他错误");
                        return View("Login");
                    }
                }
                else
                {
                    ViewBag.Login = "用户名或密码错误！";
                    return View("Login");
                }
            }
            return View("Login");
        }

        [HttpGet]
        public ActionResult VerificationCode()
        {
            //【1】生成4位验证码随机字符串
            VerificationCode vCode = new VerificationCode();
            string code = vCode.CreateValidateCode(4);//获取4位数的随机数

            //【2】保存到TempDate里面（因为TempDate默认的机制是Session）
            TempData["ValidateCode"] = code;

            //Session["ValidateCode"] = code;

            //byte[] bytes = vCode.CreateValidateGraphic(code);

            //return File(bytes, @"image/jpeg");
            //【3】返回验证码图片
            return File(vCode.CreateValidateGraphic(code), "image/jpeg");
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            //注销操作
            return RedirectToAction("Index", "Login");
        }

    }
}