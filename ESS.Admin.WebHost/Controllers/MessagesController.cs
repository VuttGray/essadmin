using System;
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
    public class MessagesController : ControllerBase
    {
        private readonly IRepository<Message> _repository;

        public MessagesController(IRepository<Message> messagesRepository)
        {
            _repository = messagesRepository;
        }

        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<MessageResponse>>> GetMessagesAsync()
        {
            var messages = await _repository.GetAllAsync();
            var response = messages.Select(message => new MessageResponse(message)).ToList();
            return Ok(response);
        }

        /// <summary>
        /// Get active messages
        /// </summary>
        /// <returns></returns>
        [HttpGet("/active")]
        public async Task<ActionResult<List<MessageResponse>>> GetActiveMessagesAsync()
        {
            var messages = await _repository.GetActiveAsync();
            var response = messages.Select(message => new MessageResponse(message)).ToList();
            return Ok(response);
        }

        /// <summary>
        /// Get message by Id
        /// </summary>
        /// <param name="id">Message identifier (GUID)</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MessageResponse>> GetMessageAsync(Guid id)
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null) return NotFound();
            var response = new MessageResponse(message);
            return Ok(response);
        }

        /// <summary>
        /// Create a new message
        /// </summary>
        /// <param name="request">Message request to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MessageResponse>> CreateMessageAsync(CreateMessageRequest request)
        {
            var message = new Message()
            {
                RecordId = Guid.NewGuid(),
                Subject = request.Subject,
                Body = request.Body,
                CreatedDate = DateTime.Now,
                Attempts = 0,
            };

            await _repository.AddAsync(message);

            return CreatedAtAction(nameof(GetMessageAsync), new { id = message.RecordId }, null);
        }

        /// <summary>
        /// Update a message
        /// </summary>
        /// <param name="id">Message identifier (GUID)</param>
        /// <param name="request">Message request to be updated</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditMessageAsync(Guid id, EditMessageRequest request)
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null) return NotFound();

            message.SentDate = request.SentDate;
            message.Attempts = request.Attempts;

            await _repository.UpdateAsync(message);

            return Ok();
        }

        /// <summary>
        /// Delete a message
        /// </summary>
        /// <param name="id">Message identifier (GUID)</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            await _repository.DeleteAsync(entity);
            return Ok();
        }
    }
}