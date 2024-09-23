using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.External
{
    public class Gateway : IGateway
    {
        public void Send(string userId, string message)
        {
            Console.WriteLine($"Sending message to {userId}: {message}");
        }
    }
}
