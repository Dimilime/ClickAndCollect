using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Validations
{
    public class DoBAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool valid = false;
            DateTime d1 = new DateTime(DateTime.Now.Year - 99, 1, 1);
            DateTime d2 = new DateTime(DateTime.Now.Year - 16, 1, 1);
            DateTime dob = (DateTime)value;
            if (dob <= d2 && dob >= d1)
            {
                valid = true;
            }
            ErrorMessage = $"L'année de naissance doit être comprise entre {d1:yyyy} et {d2:yyyy}";
            return valid;
        }
    }
}
