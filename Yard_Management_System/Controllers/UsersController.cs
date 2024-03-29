﻿using Database;
using Domain.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Authorize(Policy = "OnlyForAdmin")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApplicationContext db;
        IUserService _userService;

        public UsersController(ApplicationContext context, IUserService userService)
        {
            db = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid userId, CancellationToken token)
        {
            return Ok(await _userService.GetAsync(userId, token));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            return Ok(await _userService.GetAllAsync(token));
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationAndUpdate(UserDto user, CancellationToken token)
        {
            await _userService.RegistrationAndUpdateAsync(user, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken token)
        {
            await _userService.DeleteUserAsync(userId, token);
            return Ok();
        }
    }
}
