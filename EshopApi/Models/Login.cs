using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EshopApi.Models
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
