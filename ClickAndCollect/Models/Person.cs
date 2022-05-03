using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public abstract class Person
    {
        private string lastName;
        private string firstName;
        private string email;
        private string password;
        
        public Person (string ln, string fn, string e, string p)
        {
            lastName = ln;
            firstName = fn;
            email = e;
            password = p;
        }

        public static void Authenticate(string email, string password)
        {

        }

        public abstract void LogOut();

        public abstract void Register();
    }
}
