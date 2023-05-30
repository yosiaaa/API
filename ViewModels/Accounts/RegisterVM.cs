﻿using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First Name is Required")] // anotasi untuk validasi()
        /*[Display(Name = "First Name")]*/
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }

        [EmailAddress]
        [NIKEmailPhoneValidation(nameof(Email))]
        public string Email { get; set; }

        [Phone]
        [NIKEmailPhoneValidation(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }

        [Range(0,4, ErrorMessage = "GPA must be between 0 & 4")]
        public float Gpa { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }

        [PasswordValidation(ErrorMessage = "Password must contains at least 1 Uppercase, 1 Lowercase, 1 Symbol, and min 6 chars")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
