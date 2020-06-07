using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nickel.Web.Models
{
    [Bind(Exclude = "Id")]
    public class AccountModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2, 5})$", ErrorMessage = "Invalid address email") ]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords are differents")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Photo{ get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public AccountModel() {}
    }
}