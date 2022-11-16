using Microsoft.AspNetCore.SignalR;

namespace Assignment3.Data
{
	public class DataHub : Hub
	{
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("RecieveMessage");
        }
        
    }
}
