using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost;
using ESS.Admin.WebHost.Models;
using Microsoft.Extensions.Options;

namespace EssAdmin.WebHost.Controllers
{
    /// <summary>
    /// Users
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly AppOptions _appOptions;

        public UsersController(IRepository<User> userRepository, AppOptions options)
        {
            _userRepository = userRepository;
            // Using options is not required here - just to test the functionality
            _appOptions = options;
        }
        
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserShortResponse>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var adminsModelList = users
                .Where(u => !u.IsDeleted || _appOptions.ShowDeleted)
                .Select(x => 
                new UserShortResponse()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return adminsModelList;
        }
        
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponse>> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();
            
            var userModel = new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
            };

            return userModel;
        }
    }
}