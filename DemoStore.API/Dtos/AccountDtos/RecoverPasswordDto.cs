using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Dtos.AccountDtos
{
    public class RecoverPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
