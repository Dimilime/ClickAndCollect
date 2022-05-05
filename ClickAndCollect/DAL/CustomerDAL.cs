using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class CustomerDAL: ICustomerDAL
    {
        private string connectionString;

        public CustomerDAL (string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
