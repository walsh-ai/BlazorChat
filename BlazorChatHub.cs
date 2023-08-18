using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR; 

namespace BlazorChat 
{
    /**
    *   Signal IR Hubs API allows for connected clients to call methods on the server 
    *   This class inherits from the Hub class 
    *   Public methods are callable by the client 
    */
    public class BlazorChatSampleHub : Hub 
    {
        public const string HubURL = "/chat"; 

        /**
        *   Broadcast method depends on the Hub being alive 
        */
        public async Task Broadcast(string username, string message) {
            // Use await when calling an async method that depends on the Hub alive
            // Clients.All.SendAsync() can fail if called without await and the hub method
            //      completes before SendAsync finishes
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        /**
        *   OnConnectedAsync override 
        *   Print the connection Id when a new client connects 
        */
        public override Task OnConnectedAsync() {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync(); 
        }

        /**
        * Print to the console when client disconnects with exception message
        *   if Exception e is not null 
        *   *This method requires 'await' not 'return' because it is async 
        */
        public override async Task OnDisconnectedAsync(Exception e) {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}"); 
            await base.OnDisconnectedAsync(e); 
        }
    }
}