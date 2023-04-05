using System.Threading.Tasks;
using FinalSprint.src.Classes;
using Microsoft.AspNetCore.SignalR;

namespace FinalSprint.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MainWindow _mainWindow;

        //Hub used for remote client
        public ChatHub(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public async Task TurnCurrentOn()
        {
            _mainWindow.RemoteTurnCurrentOn();
            bool status = _mainWindow.RemoteGetCurrentStatus() == 1 ? true : false;
            await Clients.All.SendAsync("UpdateCurrentStatus", status);
        }

        public async Task TurnCurrentOff()
        {
            _mainWindow.RemoteTurnCurrentOff();
            bool status = _mainWindow.RemoteGetCurrentStatus() == 1 ? true : false;
            await Clients.All.SendAsync("UpdateCurrentStatus", status);
        }

        public async Task GetExperimentStatus()
        {
            bool status = _mainWindow.RemoteGetExperimentStatus();
            await Clients.All.SendAsync("StatusUpdate", status);
        }

        public async Task StartCapture()
        {
            _mainWindow.RemoteStartCapture();
            bool status = _mainWindow.RemoteGetCaptureStatus();
            await Clients.All.SendAsync("UpdateExperimentStatus", status);
        }

        public async Task StopCapture()
        {
            _mainWindow.RemoteStopCapture();
            bool status = _mainWindow.RemoteGetCaptureStatus();
            await Clients.All.SendAsync("UpdateExperimentStatus", status);
        }

        public async Task GetCaptureStatus(string message)
        {
            bool status = _mainWindow.RemoteGetCaptureStatus();
            await Clients.All.SendAsync("UpdateExperimentStatus", status);
        }

        public async Task GetCurrentStatus()
        {

            bool status = _mainWindow.RemoteGetCurrentStatus() == 1 ? true : false;
            await Clients.All.SendAsync("UpdateCurrentStatus", status);

        }


    }
}
