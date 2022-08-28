using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public interface IPersonDAL
    {
        bool CheckIfAccountExists(Person p);
        Person GetAllFromUser(Person p);
    }
}
