using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter a valid username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }
    }
}
