using DemoStore.API.Dtos;
using DemoStore.API.Dtos.AccountDtos;
using DemoStore.API.Infrastructure;
using DemoStore.API.Interfaces;
using DemoStore.Core.Entities.UserAggregate;
using DemoStore.Core.Interfaces;
using DemoStore.Core.Specifications;
using DemoStore.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoStore.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AccountService(UserManager<ApplicationUser> userManager, IUserRepository userRepository, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _configuration = configuration;
            _emailSender = emailSender;
                 
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

        public async Task<UserDto> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                return user.MapUserDto();
            }

            return null;
        }

        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
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

        public async Task<LoginInfo> LoginAsync(LoginUserDto userDto)
        {
            if (await ValidateCredentialsAsync(userDto))
            {
                var user = await FindByEmailAsync(userDto.Email);
                var tokenString = GenerateJsonWebToken(user);

                return new LoginInfo
                {
                    Succeeded = true,
                    Token = tokenString
                   
                };
            }

            return new LoginInfo() { Succeeded = false };
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await FindByEmailAsync(resetPasswordDto.Email);
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

            return result;
        }

        public async Task SendRecoverPasswordMailAsync(RecoverPasswordDto recoverPasswordDto, string url)
        {
            var message = new Message(new string[] { recoverPasswordDto.Email }, "Password Recovery", url);
            await _emailSender.SendEmailAsync(message);
        }

        public async Task<IdentityResult> UpdateUserAync(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);

            user.Email = userDto.Email;
            user.UserName = userDto.UserName;
            user.BirthDate = userDto.BirthDate;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Name = userDto.Name;

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<bool> ValidateCredentialsAsync(LoginUserDto userDto)
        {
            var user = await FindByEmailAsync(userDto.Email);
            return await _userManager.CheckPasswordAsync(user, userDto.Password);
        }

        private string GenerateJsonWebToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
