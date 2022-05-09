using ClickAndCollect.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Person
    {
        public int Id { get; set; }
        private string lastName;
        private string firstName;
        private string email;
        private string password;
        private string type;
        
        public Person()
        {

        }
        public Person (string ln, string fn, string e, string p)
        {
            lastName = ln;
            firstName = fn;
            email = e;
            password = p;
        }

        [Display(Name = "Nom de famille")]
        [Required(ErrorMessage = "Le nom de famille est obligatoire !")]
        [StringLength(20, MinimumLength = 3, ErrorMessage ="Le nom de famille est invalide !")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage ="Le prénom est obligatoire !")]
        [StringLength(20, MinimumLength = 3, ErrorMessage ="Le prénom est invalide !")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [Display(Name ="Adresse email")]
        [Required(ErrorMessage ="L'adresse email est obligatoire !")]
        [DataType(DataType.EmailAddress, ErrorMessage ="L'adresse email est invalide !")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage ="Le mot de passe est obligatoire !")]
        [DataType(DataType.Password, ErrorMessage ="Le mot de passe est invalide !")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public static void Authenticate(string email, string password)
        {

        }

        public bool VerifierCompte(IPersonDAL PersonDAL)
        {
            return PersonDAL.AccountExists(this);
        }

        public void GetUser(IPersonDAL personDAL)
        {

        }


        public void LogOut()
        {

        }

    }
}
