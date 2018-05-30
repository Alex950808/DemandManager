using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extensions
{
   public static class ConvertExtensions
    {
        public static DateTime? ConvertReaderDateTime(this string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
            {
                //DateTime output;
                //if (DateTime.TryParse(dateStr, out output))
                //{
                //    return output;
                //}
                //else
                //{
                //    //return DateTime.MinValue;
                //    return null;
                //}
                return null;
            }
            else
            {
                DateTime output;
                if (DateTime.TryParse(dateStr, out output))
                {
                    return output;
                }
                else
                {
                    return null;
                }
            }
            
        }
    }
}
