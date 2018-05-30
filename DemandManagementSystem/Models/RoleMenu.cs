using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class RoleMenu
    {
        private int roleMenuId;
        public int RoleMenuId
        {
            get { return roleMenuId; }
            set { roleMenuId = value; }
        }

        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private int menuId;
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }
    }
}
