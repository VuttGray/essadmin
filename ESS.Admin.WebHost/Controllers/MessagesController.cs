using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESS.Admin.WebHost.Mappers;
using ESS.Admin.WebHost.Models;
using ESS.Admin.Core.Abstractions.Services;

namespace ESS.Admin.WebHost.Controllers
{
    /// <summary>
    /// Messages
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageMapper _messageMapper;
        private readonly IMessageService _messageService;

        public MessagesController(IMessageMapper messageMapper, IMessageService messageService)
        {
            _messageMapper = messageMapper;
            _messageService = messageService;
        }

        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<MessageResponse>>> GetMessagesAsync()
        {
            var messages = await _messageService.GetAllAsync();
            var response = messages.Select(message => new MessageResponse(message)).ToList();
            return Ok(response);
        }

        /// <summary>
        /// Get messages to sent
        /// </summary>
        /// <returns></returns>
        [HttpGet("/toSent")]
        public async Task<ActionResult<List<MessageResponse>>> GetMessagesToSendAsync()
        {
            var messages = await _messageService.GetMessagesToSendAsync();
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
            var message = await _messageService.GetByIdAsync(id);
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
            var message = _messageMapper.MapFromModel(request);
            await _messageService.AddAsync(message);
            return CreatedAtAction(nameof(GetMessageAsync), new { id = message.RecordId }, null);
        }

        /// <summary>
        /// Mark a message as sent
        /// </summary>
        /// <param name="id">Message identifier (GUID)</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> MarkSentMessageAsync(Guid id, DateTime sentDate)
        {
            var entity = await _messageService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            await _messageService.MarkSentAsync(entity, sentDate);
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
            var entity = await _messageService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            await _messageService.DeleteAsync(entity);
            return Ok();
        }
    }
}