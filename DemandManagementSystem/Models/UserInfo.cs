using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        private int userRoleId;
        public int UserRoleId
        {
            get { return userRoleId; }
            set { userRoleId = value; }
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        private string loginId;
        public string LoginId
        {
            get { return loginId; }
            set { loginId = value; }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        private string loginPwd;
        public string LoginPwd
        {
            get { return loginPwd; }
            set { loginPwd = value; }
        }

        /// <summary>
        /// 确认密码
        /// </summary>
        private string confirmPwd;
        public string ConfirmPwd
        {
            get { return confirmPwd; }
            set { confirmPwd = value; }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        /// <summary>
        /// 角色编号
        /// </summary>
        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }


    }
}
