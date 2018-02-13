using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TP.SignalR))]
namespace TP
{
    public class SignalR
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}