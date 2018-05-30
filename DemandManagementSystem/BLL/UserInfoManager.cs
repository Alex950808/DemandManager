using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class UserInfoManager
    {
        public List<UserInfo> GetUserInfoByName(string UserName)
        {
            return new DAL.UserInfoService().GetUserInfoByName(UserName);
        }

        public List<RoleInfo> GetRoleNameByName(string RoleName)
        {
            return new DAL.UserInfoService().GetRoleNameByName(RoleName);
        }

        public int AddUserInfo(UserInfo objUserInfo)
        {
            return new DAL.UserInfoService().AddUserInfo(objUserInfo);
        }

        public int DeleteInfoByLoginId(int UserId, int UserRoleId)
        {
            return new DAL.UserInfoService().DeleteInfoByLoginId(UserId, UserRoleId);
        }

        public UserInfo LookUserInfoById(int UserRoleId)
        {
            return new DAL.UserInfoService().LookUserInfoById(UserRoleId);
        }

        public List<RoleInfo> GetRoleName()
        {
            return new DAL.UserInfoService().GetRoleName();
        }

        public int UpdateInfoByLoginId(UserInfo objUser)
        {
            return new DAL.UserInfoService().UpdateInfoByLoginId(objUser);
        }

        /// <summary>
        /// 查询登录账号是否存在
        /// </summary>
        /// <param name="LoginId">登陆账号</param>
        /// <returns>如果>0就表示存在，反之不存在</returns>
        public int UniqueAccount(string LoginId)
        {
            return new DAL.UserInfoService().UniqueAccount(LoginId);
        }

        }
}
