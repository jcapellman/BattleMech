using System;
using System.Threading.Tasks;
using BattleMech.WebAPI.PCL.Transports.Common;
using Newtonsoft.Json;

namespace BattleMech.PCL.PSI {
    public abstract class BaseLFSPSI {
        public abstract Task<bool> WriteFile<T>(T obj);

        public abstract Task<CTI<T>> GetFile<T>(Type objType);

        protected static byte[] GetBytesFromT<T>(T obj) {
            var jsonStr = GetJSONStringFromT(obj);

            var bytes = new byte[jsonStr.Length * sizeof(char)];

            System.Buffer.BlockCopy(jsonStr.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        protected static T GetObjectFromBytes<T>(byte[] bytes) {
            var chars = new char[bytes.Length / sizeof(char)];

            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            
            return JsonConvert.DeserializeObject<T>(new string(chars));
        }

        protected static string GetJSONStringFromT<T>(T obj) { return JsonConvert.SerializeObject(obj); }
    }
}