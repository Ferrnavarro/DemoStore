using DemoStore.API.Dtos.AccountDtos;
using DemoStore.Core.Entities.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(NewUserDto newUserDto);

        Task<IdentityResult> UpdateUserAync(UserDto userDto);

        Task<IdentityResult> DeleteUserAsync(string userId);

        Task<UsersListDto> ListUsersAsync(int pageIndex, int itemsPage, string search);

        Task<IReadOnlyList<UserDto>> ListAllUsersAsync();

        Task<ApplicationUser> FindByUserNameAsync(string userName);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<UserDto> FindByIdAsync(string id);

        Task<bool> ValidateCredentialsAsync(LoginUserDto user);

        Task<LoginInfo> LoginAsync(LoginUserDto userDto);

    }


    public class LoginInfo
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }

    }
}
