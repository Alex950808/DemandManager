using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Users
    {
        /// <summary>
        /// 验证码
        /// </summary>
        private string validateCode;
        public string ValidateCode
        {
            get { return validateCode; }
            set { validateCode = value; }
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
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string loginId;
        public string LoginId
        {
            get { return loginId; }
            set { loginId = value; }
        }

        private string loginPwd;
        public string LoginPwd
        {
            get { return loginPwd; }
            set { loginPwd = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
