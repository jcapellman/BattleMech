using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using BattleMech.WebAPI.Managers;

namespace BattleMech.WebAPI.Handlers {
    public class Authandler : DelegatingHandler {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            if (request.RequestUri.PathAndQuery == "/api/Auth" || request.RequestUri.PathAndQuery == "/api/Users") {
                return await base.SendAsync(request, cancellationToken);
            }

            var token = request.Headers.GetValues("AUTH_TOKEN").First();

            var result = new AuthManager().CheckToken(token);

            if (result == null) {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();

                tsc.SetResult(response);
                return await tsc.Task;
            }

            request.Properties.Add("AUTH_USER", result);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}