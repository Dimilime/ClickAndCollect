using ClickAndCollect.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Customer : Person
    {
        private DateTime doB;
        private int phoneNumber;   
        private int idPerson;
        private List<Order> orders;
        public int OrderId { get; set; }

        public Customer() { }

        public Customer (string ln, string fn, string e, string p, DateTime d, int pn)
            :base(ln,fn,e,p)
        {
            doB = d;
            phoneNumber = pn;
            orders = new List<Order>();
            
        }

        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        //[Range(typeof(DateTime), "01/01/1900", "31/12/2006", ErrorMessage ="La date est incorrect !")] probleme ???
        [Required(ErrorMessage ="La date de naisse est obligatoire !")]
        public DateTime DoB
        {
            get { return doB; }
            set { doB = value; }
        }
        
        [Display(Name = "Numéro de téléphone")]
        [Required(ErrorMessage ="Le numéro de téléphone est obligatoire !")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Le numéro de téléphone est invalide !")]
        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public bool Register(ICustomerDAL customerDAL)
        {
            return customerDAL.AddCustomer(this);
        }

        public bool VerifierMailCustomer(ICustomerDAL customerDAL)
        {
            return customerDAL.EmailCustomerExists(this);
        }


    }
}
