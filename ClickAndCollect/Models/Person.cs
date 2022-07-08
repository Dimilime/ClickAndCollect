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
        [EmailAddress (ErrorMessage ="L'adresse email est invalide !")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage ="Le mot de passe est obligatoire !")]
        [DataType(DataType.Password, ErrorMessage ="Le mot de passe est invalide !")]
        [RegularExpression(@"^(?=.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$", ErrorMessage = "Le mot de passe doit contenir au moins 1 majuscule, 1 minuscule, 1 chiffre, 1 caractère spécial et une longueur d'au moins 8 !")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool CheckIfAccountExists(IPersonDAL PersonDAL)
        {
            return PersonDAL.CheckIfAccountExists(this);
        }

        public Person GetAllFromUser(IPersonDAL personDAL)
        {
            return personDAL.GetAllFromUser(this);
        }

    }
}
