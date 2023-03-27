using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

/*        public async Task SendMessage(string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
           _mainWindow.UpdateLabel(message);
        }
*/
        public async Task TurnCurrentOn()
        {
/*            await Clients.All.SendAsync("ReceiveMessage", "");*/
            _mainWindow.TurnCurrentOn();
            bool status = _mainWindow.getCurrentStatus() == 1 ? true : false;
            await Clients.All.SendAsync("UpdateCurrentStatus", status);
        }

        public async Task TurnCurrentOff()
        {
            //await Clients.All.SendAsync("ReceiveMessage", "");
            _mainWindow.TurnCurrentOff();
            bool status = _mainWindow.getCurrentStatus() == 1 ? true : false;
            await Clients.All.SendAsync("UpdateCurrentStatus", status);
        }

        public async Task GetExperimentStatus()
        {
            bool status = _mainWindow.getExperimentStatus();
            await Clients.All.SendAsync("StatusUpdate", status);
        }

        public async Task StartCapture(string message)
        {
            _mainWindow.StartCapture();
            bool status = _mainWindow.GetCaptureStatus();
            await Clients.All.SendAsync("UpdateExperimentStatus", status);
        }

        public async Task StopCapture(string message)
        {
            _mainWindow.StopCapture();
            bool status = _mainWindow.GetCaptureStatus();
            await Clients.All.SendAsync("UpdateExperimentStatus", status);
        }

        public async Task GetCaptureStatus(string message)
        {
            bool status = _mainWindow.GetCaptureStatus();
            await Clients.All.SendAsync("UpdateExperimentStatus", status);
        }

        public async Task GetCurrentStatus()
        {

            bool status = _mainWindow.getCurrentStatus() == 1 ? true : false;
            await Clients.All.SendAsync("UpdateCurrentStatus", status);

        }


    }
}
