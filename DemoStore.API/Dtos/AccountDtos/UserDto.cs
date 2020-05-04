using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Dtos.AccountDtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Number")]
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
     
    }
}
