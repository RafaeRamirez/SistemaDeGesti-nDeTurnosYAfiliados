using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using systemdeeps.WebApplication.Services;

namespace systemdeeps.WebApplication.Hubs
{
    // SignalR hub: real-time events for TV and operator UI
    public class TurnHub : Hub
    {
        private readonly TurnService _turnService;

        public TurnHub(TurnService turnService)
        {
            _turnService = turnService;
        }

        // Hot Reload safe: keep async overrides as-is
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("connected");
            
            // Enviar el turno actual al nuevo cliente
            var currentTurn = _turnService.GetCurrentTurn();
            if (currentTurn != null)
            {
                await Clients.Caller.SendAsync("currentTurnChanged", currentTurn.Number, currentTurn.Status);
            }
            
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        // Client -> Server
        public async Task Ping() => await Clients.Caller.SendAsync("pong");

        public async Task JoinRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Caller.SendAsync("joinedRoom", room);
        }

        public async Task LeaveRoom(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.Caller.SendAsync("leftRoom", room);
        }

        // Server -> Client helpers (not required, but handy if you ever call from inside the hub)
        public Task BroadcastNewTurnCreated(int number, string status) =>
            Clients.All.SendAsync("newTurnCreated", number, status);

        public Task BroadcastCurrentTurnChanged(int number, string status) =>
            Clients.All.SendAsync("currentTurnChanged", number, status);

        public Task BroadcastQueueUpdated() =>
            Clients.All.SendAsync("queueUpdated");
    }
}