using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class LoginVM:IValidatableObject
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool LoginCheck()
        {
            return (this.Username == "Will" && this.Password == "123456");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (!this.LoginCheck())
                yield return new ValidationResult("登入失敗", new string[] { "Username" });


            yield return ValidationResult.Success;
     

        }
    }
}