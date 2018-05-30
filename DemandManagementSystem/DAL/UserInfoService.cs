using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;
using DAL.Helper;

namespace DAL
{
    public class UserInfoService
    {
        public List<UserInfo> GetUserInfoByName(string UserName)
        {
            string sql = $"select Users.[UserId],Users.[LoginId], [UserName], [PhoneNumber], [Email],[UserRoleId],[RoleName] from Users inner join UserRole on Users.LoginId=UserRole.LoginId inner join RoleInfo on RoleInfo.RoleId=UserRole.RoleId where UserName like @UserName";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@UserName", $"%{UserName}%")
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            List<UserInfo> list = new List<UserInfo>();
            while (objReader.Read())
            {
                list.Add(new UserInfo()
                {
                    UserRoleId = Convert.ToInt32(objReader["UserRoleId"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    LoginId = objReader["LoginId"].ToString(),
                    UserName = objReader["UserName"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Email = objReader["Email"].ToString(),
                    RoleName = objReader["RoleName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        public List<RoleInfo> GetRoleNameByName(string RoleName)
        {
            string sql = $"select [RoleId], [RoleName], [RoleDescription] from RoleInfo where RoleName like @RoleName";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@RoleName", $"%{RoleName}%")
            };

            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            List<RoleInfo> list = new List<RoleInfo>();
            while (objReader.Read())
            {
                list.Add(new RoleInfo()
                {
                    RoleId = Convert.ToInt32(objReader["RoleId"]),
                    RoleName = objReader["RoleName"].ToString(),
                    RoleDescription = objReader["RoleDescription"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="objUserInfo"></param>
        /// <returns></returns>
        public int AddUserInfo(UserInfo objUserInfo)
        {
            string sql = $"insert into Users([LoginId], [LoginPwd], [UserName], [PhoneNumber], [Email]) values(@LoginId,@LoginPwd,@UserName,@PhoneNumber,@Email);insert into UserRole(LoginId,RoleId) values(@LoginId,@RoleId)";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@LoginId", objUserInfo.LoginId),
                new SqlParameter("@LoginPwd", objUserInfo.LoginPwd),
                new SqlParameter("@UserName", objUserInfo.UserName),
                new SqlParameter("@PhoneNumber", objUserInfo.PhoneNumber),
                new SqlParameter("@Email", objUserInfo.Email),
                new SqlParameter("@RoleId", objUserInfo.RoleId)
            };
            return SQLHelper.Update(sql, paras);
        }

        /// <summary>
        /// 根据用户ID删除用户信息
        /// </summary>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public int DeleteInfoByLoginId(int UserId, int UserRoleId)
        {
            string sql = $"delete Users where UserId=@UserId;delete UserRole where UserRoleId=@UserRoleId;";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@UserRoleId", UserRoleId)
            };
            return SQLHelper.Update(sql, paras);
        }

        public UserInfo LookUserInfoById(int UserRoleId)
        {
            string sql = $"select Users.[UserId],Users.[LoginId], LoginPwd,[UserName], [PhoneNumber], [Email] ,UserRoleId,[RoleName],RoleInfo.RoleId from Users inner join UserRole on Users.LoginId=UserRole.LoginId inner join RoleInfo on RoleInfo.RoleId=UserRole.RoleId where UserRoleId=@UserRoleId";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@UserRoleId", UserRoleId)
            };
            SqlDataReader objReader = SQLHelper.GetReader(sql, paras);
            UserInfo objUserInfo = null;
            if (objReader.Read())
            {
                objUserInfo = new UserInfo()
                {
                    UserRoleId = Convert.ToInt32(objReader["UserRoleId"]),
                    UserName = objReader["UserName"].ToString(),
                    LoginId = objReader["LoginId"].ToString(),
                    LoginPwd = objReader["LoginPwd"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Email = objReader["Email"].ToString(),
                    RoleName = objReader["RoleName"].ToString(),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    RoleId = Convert.ToInt32(objReader["RoleId"])
                };
            }
            objReader.Close();
            return objUserInfo;
        }

        /// <summary>
        /// 查询角色表
        /// </summary>
        /// <returns>角色ID，角色名称</returns>
        public List<RoleInfo> GetRoleName()
        {
            string sql = $"select RoleId,RoleName from RoleInfo";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<RoleInfo> list = new List<RoleInfo>();
            while (objReader.Read())
            {
                list.Add(new RoleInfo()
                {
                    RoleId = Convert.ToInt32(objReader["RoleId"]),
                    RoleName = objReader["RoleName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns>受影响的行数</returns>
        public int UpdateInfoByLoginId(UserInfo objUser)
        {
            string sql = $"update Users set [UserName]=@UserName,[LoginId]=@LoginId, [LoginPwd]=@LoginPwd,  [PhoneNumber]=@PhoneNumber, [Email]=@Email where [UserId]=@UserId;update UserRole set RoleId=@RoleId,[LoginId]=@LoginId where UserRoleId=@UserRoleId;";
            SqlParameter[] paras = new SqlParameter[]
           {
                new SqlParameter("@UserName", objUser.UserName),
                new SqlParameter("@LoginId", objUser.LoginId),
                new SqlParameter("@LoginPwd", objUser.LoginPwd),
                new SqlParameter("@PhoneNumber", objUser.PhoneNumber),
                new SqlParameter("@Email", objUser.Email),
                new SqlParameter("@UserId", objUser.UserId),
                new SqlParameter("@RoleId", objUser.RoleId),
                new SqlParameter("@UserRoleId", objUser.UserRoleId)
            };
            return SQLHelper.Update(sql, paras);
        }

        /// <summary>
        /// 查询登录账号是否存在
        /// </summary>
        /// <param name="LoginId">登陆账号</param>
        /// <returns>如果>0就表示存在，反之不存在</returns>
        public int UniqueAccount(string LoginId)
        {
            string sql = $"select ISNULL((select top(1) 1 from Users where LoginId=@LoginId),0)";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@LoginId", LoginId)
            };
            int result = Convert.ToInt32(SQLHelper.GetStringResult(sql, paras));

            return result;
        }

        

    }
}
