using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class CashierDAL : ICashierDAL
    {
        private string connectionString;

        public CashierDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
