using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Dtos.AccountDtos
{
    public class UsersListDto
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public PaginationInfoDto PaginationInfo { get; set; }
    }
}
