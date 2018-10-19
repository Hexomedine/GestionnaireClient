using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GestionnaireClient.Model
{
    public class Customer
    {
        private readonly Regex nameEx = new Regex(@"^[A-Za-z ]+$");


        [Key]
        public int Id { get; set; }

        private string firstName;

        [Required(ErrorMessage = "Prénom obligatoire")]
        [Display(Name = "Prénom")]
        public string FirstName
        {
            get { return firstName; }
            set {
                if (!nameEx.Match(value).Success)
                    throw new ArgumentException("Le prenom ne peux contenir que des lettres ou des espaces");

                firstName = value; }
        }

        private string lastName;

        [Required(ErrorMessage = "Nom obligatoire")]
        [Display(Name ="Nom")]
        public string LastName
        {
            get { return lastName; }
            set {
                if (!nameEx.Match(value).Success)
                    throw new ArgumentException("Le prenom ne peux contenir que des lettres ou des espaces");
                lastName = value; }
        }

        private string email;

        [Required(ErrorMessage = "Email obligatoire")]
        [EmailAddress(ErrorMessage = "Email non valide")]
        public string Email
        {
            get { return email; }
            set {
                if (value == null)
                    throw new ArgumentException("Email obligatoire");

                email = value; }
        }


    }
}
