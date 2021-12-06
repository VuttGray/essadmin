using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost;
using ESS.Admin.WebHost.Mappers;
using ESS.Admin.WebHost.Models;
using Microsoft.Extensions.Options;
using ESS.Admin.Core.Abstractions.Services;

namespace EssAdmin.WebHost.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserMapper _userMapper;
        private readonly IUserService _userService;
        private readonly AppOptions _appOptions;

        public UsersController(IUserMapper userMapper, IUserService userService, AppOptions options)
        {
            _userMapper = userMapper;
            _userService = userService;
            _appOptions = options;
        }
        
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserShortResponse>> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();

            var adminsModelList = users
                .Where(u => !u.IsDeleted || _appOptions.ShowDeleted)
                .Select(x => 
                new UserShortResponse()
                    {
                        Id = x.RecordId,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return adminsModelList;
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id">User identifier (GUID)</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponse>> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();
            
            var userModel = new UserResponse()
            {
                Id = user.RecordId,
                Email = user.Email,
                FullName = user.FullName,
            };

            return userModel;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="request">User request to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUserAsync(CreateOrEditUserRequest request)
        {
            var user = _userMapper.MapFromModel(request);
            await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = user.RecordId }, null);
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
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            user = _userMapper.MapFromModel(request, user);
            await _userService.UpdateAsync(user);
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
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            await _userService.LockAsync(user);
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
            var entity = await _userService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _userService.DeleteAsync(entity);
            return Ok();
        }
    }
}