using ClickAndCollect.DAL;
using ClickAndCollect.Validations;
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
        public int OrderId { get; set; }

        public Customer() { }

        [Display(Name = "Date de naissance")]
        [DoB(), DataType(DataType.Date)]
        [Required(ErrorMessage ="La date de naisse est obligatoire !")]
        public DateTime DoB
        {
            get { return doB; }
            set { doB = value; }
        }

        [Display(Name = "Numéro de téléphone")]
        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire !")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Le numéro de téléphone est invalide !")]
        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public bool Register(ICustomerDAL customerDAL)
        {
            return customerDAL.Register(this);
        }
        public static Customer GetInfoCustomer(ICustomerDAL customerDAL, int id)
        {
            return customerDAL.GetInfoCustomer(id);
        }

        public bool CheckIfEmailCustomerExists(ICustomerDAL customerDAL)
        {
            return customerDAL.CheckIfEmailCustomerExists(this);
        }


    }
}
