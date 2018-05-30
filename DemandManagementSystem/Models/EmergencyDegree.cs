using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class EmergencyDegree
    {
        private int emergencyId;
        public int EmergencyId
        {
            get { return emergencyId; }
            set { emergencyId = value; }
        }

        private string emergencyName;
        public string EmergencyName
        {
            get { return emergencyName; }
            set { emergencyName = value; }
        }
    }
}
