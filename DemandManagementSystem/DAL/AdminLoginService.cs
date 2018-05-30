using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.Helper;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AdminLoginService
    {
        public Users Login(Users objUser)
        {
            string sql = $"select Users.[UserId],Users.[LoginId], [UserName], [PhoneNumber], [Email],RoleInfo.RoleId,[RoleName] from Users inner join UserRole on Users.LoginId=UserRole.LoginId inner join RoleInfo on RoleInfo.RoleId=UserRole.RoleId where Users.LoginId=@LoginId and Users.LoginPwd=@LoginPwd";
            //SqlParameter sp = new SqlParameter("@LoginId", objUser.LoginId);
            //sp = new SqlParameter("@LoginPwd", objUser.LoginPwd);
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@LoginId", objUser.LoginId),
                new SqlParameter("@LoginPwd", objUser.LoginPwd)

            };
            SqlDataReader objReader = SQLHelper.GetReader(sql,paras);
            if (objReader.Read())
            {
                objUser.UserName = objReader["UserName"].ToString();
                objUser.UserId = Convert.ToInt32(objReader["UserId"]);
                objUser.RoleId = Convert.ToInt32(objReader["RoleId"]);
                objReader.Close();
            }
            else
            {
                objUser = null;
            }
            return objUser;
        }
    }
}
