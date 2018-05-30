using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Web;

namespace BLL
{
    public class AdminLoginManager
    {
        public Users Login(Users objUser)
        {
            objUser = new DAL.AdminLoginService().Login(objUser);

            if (objUser != null)
            {
                return objUser;
            }
            else
            {
                return null;
            }
        }

    }
}
