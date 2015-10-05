using System.Web.Http;

using BattleMech.WebAPI.Transports.Internal;

namespace BattleMech.WebAPI.Controllers {
    public class BaseController : ApiController {
        private AuthorizedUser _authorizedUser;

        public AuthorizedUser AUTH_USER => _authorizedUser ?? (_authorizedUser = Request.Properties.ContainsKey("AUTH_USER") ? (AuthorizedUser)Request.Properties["AUTH_USER"] : null);
    }
}