using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public interface IPersonDAL
    {
        public bool Authenticate (string email, string password);
        public void LogOut();
        public void Register();
    }
}
