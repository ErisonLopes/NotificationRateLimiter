using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.External
{
    public class Gateway : IGateway
    {
        private readonly ILogger<Gateway> _logger;
        public Gateway(ILogger<Gateway> logger) 
        {
            _logger = logger;
        }

        public void Send(string userId, string message)
        {
            _logger.LogInformation($"Sending message to {userId}: {message}");
        }
    }
}
