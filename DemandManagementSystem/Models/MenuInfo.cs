using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class MenuInfo
    {
        private int menuId;
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        private string menuName;
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }
    }
}
