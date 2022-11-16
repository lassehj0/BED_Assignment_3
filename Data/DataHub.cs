using Microsoft.AspNetCore.SignalR;

namespace Assignment3.Data
{
	public class DataHub : Hub
	{
        public async Task Send()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
