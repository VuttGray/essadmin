using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESS.Admin.WebHost.Models;
using ESS.Admin.Core.Abstractions.Services;
using AutoMapper;
using ESS.Admin.Core.Domain.Administration;

namespace ESS.Admin.WebHost.Controllers
{
    /// <summary>
    /// Messages
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessagesController(IMapper mapper, IMessageService messageService)
        {
            _mapper = mapper;
            _messageService = messageService;
        }

        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<MessageResponse>>> GetAsync()
        {
            var values = await _messageService.GetAllAsync();
            var models = _mapper.Map<List<Message>, List<MessageResponse>>(values.ToList());
            return Ok(models);
        }

        /// <summary>
        /// Get messages to sent
        /// </summary>
        /// <returns></returns>
        [HttpGet("/toSent")]
        public async Task<ActionResult<List<MessageResponse>>> GetMessagesToSendAsync()
        {
            var values = await _messageService.GetMessagesToSendAsync();
            var models = _mapper.Map<List<Message>, List<MessageResponse>>(values.ToList());
            return Ok(models);
        }

        /// <summary>
        /// Get message by Id
        /// </summary>
        /// <param name="id">Message identifier (GUID)</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MessageResponse>> GetByIdAsync(Guid id)
        {
            var value = await _messageService.GetByIdAsync(id);
            if (value == null) return NotFound();
            var model = _mapper.Map<MessageResponse>(value);
            return Ok(model);
        }

        /// <summary>
        /// Create a new message
        /// </summary>
        /// <param name="request">Message request to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MessageResponse>> CreateAsync(CreateMessageRequest request)
        {
            var message = _mapper.Map<Message>(request);
            await _messageService.AddAsync(message);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = message.RecordId }, null);
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
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _messageService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            await _messageService.DeleteAsync(entity);
            return Ok();
        }
    }
}