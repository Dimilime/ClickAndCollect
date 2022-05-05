using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class PersonDAL : IPersonDAL
    {
        private string connectionString;

        public PersonDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }
    }
}
