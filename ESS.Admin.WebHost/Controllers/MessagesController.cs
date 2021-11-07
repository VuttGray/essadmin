using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost.Models;

namespace ESS.Admin.WebHost.Controllers
{
    /// <summary>
    /// Messages
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessagesController
    {
        private readonly IRepository<Message> _messagesRepository;

        public MessagesController(IRepository<Message> messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }
        
        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MessageResponse>> GetMessagesAsync()
        {
            var messages = await _messagesRepository.GetAllAsync();

            var messagesModelList = messages.Select(x => 
                new MessageResponse()
                {
                    Id = x.Id,
                    Subject = x.Subject,
                    BodyPreview = x.BodyPreview,
                    SentDate = x.SentDate,
                }).ToList();

            return messagesModelList;
        }
    }
}