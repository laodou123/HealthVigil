using Microsoft.AspNetCore.SignalR;
namespace HealthcareMonitoring.Server.Hubs
{
	public class Chathub : Hub
	{
		public async Task SendMessage(int CurrentUserId, int Value,String NewMessage)

		{
			await Clients.All.SendAsync("ReceiveMessage", CurrentUserId, Value, NewMessage);
		}
	}
}
