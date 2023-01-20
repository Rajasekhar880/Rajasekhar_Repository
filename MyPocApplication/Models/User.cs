using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPocApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display (Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        [Display(Name = "EmailID")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Email ID is not Valid")]
        public string Email { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }



    }
}