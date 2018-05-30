using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class ProductList
    {
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private string versionNumber;
        public string VersionNumber
        {
            get { return versionNumber; }
            set { versionNumber = value; }
        }

        private int demandId;
        public int DemandId
        {
            get { return demandId; }
            set { demandId = value; }
        }
    }
}
