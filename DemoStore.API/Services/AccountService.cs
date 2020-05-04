using DemoStore.API.Dtos;
using DemoStore.API.Dtos.AccountDtos;
using DemoStore.API.Infrastructure;
using DemoStore.API.Interfaces;
using DemoStore.Core.Entities.UserAggregate;
using DemoStore.Core.Interfaces;
using DemoStore.Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoStore.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public AccountService(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateUserAsync(NewUserDto newUserDto)
        {
            var user = newUserDto.MapNewApplicationUser();        
            var result = await _userManager.CreateAsync(user, newUserDto.Password);
            return result;           
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<IReadOnlyList<UserDto>> ListAllUsersAsync()
        {
            var users = await _userRepository.ListAllAsync();

            return users.Select(s => s.MapUserDto()).ToList();
        }

        public async Task<UsersListDto> ListUsersAsync(int pageIndex, int itemsPage, string search)
        {
            var filterPaginatedEspecification = new UserFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, search);
            var filterSpecification = new UserFilterSpecification(search);

            var itemsOnPage = await _userRepository.ListAsync(filterPaginatedEspecification);
            var totalItems = await _userRepository.CountAsync(filterSpecification);

            var users = new UsersListDto()
            {
                Users = itemsOnPage.Select(s => s.MapUserDto()).ToList(),

                PaginationInfo = new PaginationInfoDto()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            users.PaginationInfo.Next = (users.PaginationInfo.ActualPage == users.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            users.PaginationInfo.Previous = (users.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return users;
        }

        public Task<IdentityResult> UpdateUserAync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateCredentialsAsync(LoginUserDto userDto)
        {
            var user = await FindByEmailAsync(userDto.Email);
            return await _userManager.CheckPasswordAsync(user, userDto.Password);
        }
    }
}
