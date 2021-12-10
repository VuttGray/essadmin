using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ESS.Admin.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Message> _repository;
        private readonly IEmailService _emailService;

        public Worker(ILogger<Worker> logger, 
            IServiceProvider serviceProvider,
            IRepository<Message> repository,
            IEmailService emailService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _repository = repository;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await SendMessages();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task SendMessages()
        {
            using var scope = _serviceProvider.CreateScope();

            while(true)
            {
                var message = await _repository.GetFirstOrDefaultAsync();
                if (message == null) break;
                await _emailService.SendAsync(message);
            }
        }
    }
}
