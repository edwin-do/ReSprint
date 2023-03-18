using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FinalSprint.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MainWindow _mainWindow;
        public ChatHub(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public async Task SendMessage(string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
           _mainWindow.UpdateLabel(message);
        }

        public async Task StopCapture(string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
            _mainWindow.UpdateLabel(message);
        }
    }
}
