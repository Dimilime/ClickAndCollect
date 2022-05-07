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
        public static int nbr = 6; //A CHANGER AVANT DE RENDRE LE PROJET !!!!!!
        private int idPerson;
        private List<Order> orders;

        public Customer() //verifier si peut faire un this
        {
            nbr = nbr + 1;
            IdPerson = nbr;

        }
        public Customer (string ln, string fn, string e, string p, DateTime d, int pn)
            :base(ln,fn,e,p)
        {
            doB = d;
            phoneNumber = pn;
            orders = new List<Order>();
            nbr = nbr + 1;
            IdPerson = nbr;
        }

        [Display(Name = "Date de naissance")]
        [Required(ErrorMessage ="La date de naisse est obligatoire !")]
        [DataType(DataType.Date, ErrorMessage ="La date de naisse est invalide !")]
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

        public int IdPerson
        {
            get { return idPerson; }
            set { idPerson = value; }
        }


        public override void LogOut()
        {
            throw new NotImplementedException();
        }

        public void Register(ICustomerDAL customerDAL)
        {
            customerDAL.AddCustomer(this);
        }

        public bool VerifierMail(ICustomerDAL customerDAL)
        {
            return customerDAL.EmailExists(this);
        }

        public bool VerifierCompte(ICustomerDAL customerDAL)
        {
            return customerDAL.CheckAccount(this);
        }
    }
}
