using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models;
using BattleMech.WebAPI.PCL.Transports.Common;

using Newtonsoft.Json;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class BaseHandler {
        private string _baseURL { get; set; }

        internal string _baseArgURL { get; set; }

        public BaseHandler(string baseURL, string baseArgURL) {
            _baseURL = baseURL;
            _baseArgURL = baseArgURL;
        }

        private static HttpClient HC => new HttpClient();

        private string BASEURL => _baseURL + _baseArgURL;

        private static HttpContent convertObj<T>(T objValue) {
            return new StringContent(JsonConvert.SerializeObject(objValue), Encoding.UTF8, "application/json");
        }

        public async Task<CTI<TK>> PUT<T, TK>(T obj) where T : BaseModel { return await PUT<T, TK>(_baseArgURL, obj); }

        public async Task<CTI<TK>> PUT<T, TK>(string urlArgs, T obj) where T : BaseModel {
            var result = await HC.PutAsync(BASEURL, convertObj(obj));
            
            var data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<CTI<TK>>(data);
        }
    }
}