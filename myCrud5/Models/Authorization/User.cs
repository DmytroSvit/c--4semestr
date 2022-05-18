using System.ComponentModel.DataAnnotations;
using myCrud.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace myCrud.Models.Authorization
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

    }

    public class Register
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression(
            Settings.EmailPattern,
            ErrorMessage = "Not valid email")
        ]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
