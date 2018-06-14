using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace TP.PL.Hubs
{
    [HubName("HubIO")]
    public class IOConnectionHub : Hub
    {
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string Id = Context.ConnectionId;

            return base.OnDisconnected(stopCalled);
        }
    }
}