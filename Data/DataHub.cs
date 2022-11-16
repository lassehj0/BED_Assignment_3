using Microsoft.AspNetCore.SignalR;

namespace Assignment3.Data
{
    public interface IData
    {
        Task SendData(string user, string data);
    }
	public class DataHub : Hub<IData>
	{
        public async Task Send(string user, string data)
        {
            await Clients.All.SendData(user, data);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync(); 
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
