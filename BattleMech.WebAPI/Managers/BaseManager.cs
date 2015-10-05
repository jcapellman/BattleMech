using BattleMech.WebAPI.Transports.Internal;

namespace BattleMech.WebAPI.Managers {
    public class BaseManager {
        internal AuthorizedUser _authorizedUser;

        public BaseManager(AuthorizedUser authorizedUser = null) { _authorizedUser = authorizedUser; }
    }
}