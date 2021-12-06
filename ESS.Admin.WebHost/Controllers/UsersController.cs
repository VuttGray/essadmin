using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost;
using ESS.Admin.WebHost.Models;
using ESS.Admin.Core.Abstractions.Services;
using AutoMapper;

namespace EssAdmin.WebHost.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserShortResponse>>> GetUsersAsync()
        {
            var values = await _userService.GetActiveAsync();
            var models = _mapper.Map<List<User>, List<UserShortResponse>>(values.ToList());
            return Ok(models);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id">User identifier (GUID)</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponse>> GetUserByIdAsync(Guid id)
        {
            var value = await _userService.GetByIdAsync(id);
            if (value == null) return NotFound();
            var model = _mapper.Map<UserResponse>(value);
            return model;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="request">User request to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUserAsync(CreateOrEditUserRequest request)
        {
            var value = _mapper.Map<User>(request);
            await _userService.AddAsync(value);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = value.RecordId }, null);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id">User identifier (GUID)</param>
        /// <param name="request">User request to be updated</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditUserAsync(Guid id, CreateOrEditUserRequest request)
        {
            var value = await _userService.GetByIdAsync(id);
            if (value == null) return NotFound();
            value = _mapper.Map<User>(request);
            await _userService.UpdateAsync(value);
            return Ok();
        }

        /// <summary>
        /// Lock a user
        /// </summary>
        /// <param name="id">User identifier (GUID)</param>
        /// <returns></returns>
        [HttpPut("/lock/{id:guid}")]
        public async Task<IActionResult> LockUserAsync(Guid id)
        {
            var value = await _userService.GetByIdAsync(id);
            if (value == null) return NotFound();
            await _userService.LockAsync(value);
            return Ok();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">User identifier (GUID)</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var value = await _userService.GetByIdAsync(id);
            if (value == null) return NotFound();
            await _userService.DeleteAsync(value);
            return Ok();
        }
    }
}