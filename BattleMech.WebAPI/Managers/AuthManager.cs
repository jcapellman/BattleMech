using System.Security.Cryptography;
using System.Text;
using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.Transports.Internal;
using BattleMech.DataLayer.PCL.Models.Users;

namespace BattleMech.WebAPI.Managers {
    public class AuthManager : BaseManager {
        public AuthResponseItem GenerateToken(AuthRequestItem requestItem) {
            using (var uFactory = new UserContext()) {
                using (var assetFactory = new AssetContext()) {
                    using (var md5Hash = MD5.Create()) {
                        var hash = getMd5Hash(md5Hash, requestItem.Username + requestItem.Password);

                        var match = uFactory.UsersDS.FirstOrDefault(a => a.EmailAddress == requestItem.Username && a.Password == requestItem.Password);

                        if (match == null) {
                            return null;
                        }

                        var characterAsset = assetFactory.CharacterAssetsVIEWDS.FirstOrDefault(a => a.UserID == match.ID);

                        var tokenExist = uFactory.TokensDS.Any(a => a.HASH == hash);

                        if (!tokenExist) {
                            var token = new Tokens { UserID = match.ID, HASH = hash, CharacterID = characterAsset.ID };

                            uFactory.TokensDS.Add(token);

                            uFactory.SaveChanges();
                        }
                        
                        return match == null ? null : new AuthResponseItem {Token = hash, PlayerAssetID = characterAsset.AssetID };
                    }
                }
            }
        }

        static string getMd5Hash(MD5 md5Hash, string input) {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            
            var sBuilder = new StringBuilder();
            
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public AuthorizedUser CheckToken(string token) {
            if (string.IsNullOrEmpty(token)) {
                return null;
            }

            using (var uFactory = new UserContext()) {
                var result = uFactory.TokensDS.FirstOrDefault(a => a.HASH == token);

                if (result == null) {
                    return null;
                }

                return new AuthorizedUser { ID = result.UserID, CharacterID = result.CharacterID };
            }
        }
    }
}