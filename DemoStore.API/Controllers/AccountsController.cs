using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DemoStore.API.Dtos.AccountDtos;
using DemoStore.API.Infrastructure;
using DemoStore.API.Interfaces;
using DemoStore.Core.Entities.UserAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DemoStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<UsersListDto>> Get(int pageIndex = 0, int itemsPage = 10,  string search = "")
        {
            var users = await _accountService.ListUsersAsync(pageIndex, itemsPage, search);
            return users;
        }

        // GET: api/Accounts/A
        [HttpGet("{id}", Name = "GetAccount")]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            var user = await _accountService.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] NewUserDto newUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAsync(newUserDto);

                if (result.Errors.Count() > 0)
                {
                    AddErrors(result);
                    return BadRequest(ModelState);
                }

                var user = await _accountService.FindByUserNameAsync(newUserDto.UserName);

                return CreatedAtAction("Get", new { id = user.Id }, user.MapUserDto());
            }

            return BadRequest(ModelState);           
        }


        // PUT: api/Accounts/A
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != userDto.Id)
                {
                    return BadRequest("Id does not match Id in user object");
                }

                var userInDb = await _accountService.FindByIdAsync(id);

                if (userInDb == null)
                {
                    return NotFound();
                }

                var result = await _accountService.UpdateUserAync(userDto);

                if (result.Errors.Count() > 0)
                {
                    AddErrors(result); 
                    return BadRequest(ModelState);
                }

                var userUpdated = await _accountService.FindByIdAsync(id);
                return Ok(new { successMessage = "User updated", user = userUpdated });

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _accountService.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _accountService.DeleteUserAsync(id);

            if (result.Errors.Count() > 0)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(new { successMessage = "User deleted", user }); ;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginAsync(loginUserDto);

                if (result.Succeeded)
                {                   
                    return Ok(new { token = result.Token });
                }

                ModelState.AddModelError("ErrorMessage", "Invalid username or password.");
            }

            ModelState.AddModelError("ErrorMessage", "Error in login attempt");
            return BadRequest(ModelState);
        }


        

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}