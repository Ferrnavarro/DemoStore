using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Dtos.AccountDtos
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
