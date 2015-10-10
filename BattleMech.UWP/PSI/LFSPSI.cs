using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Storage;

using BattleMech.PCL.PSI;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.UWP.PSI {
    public class LFSPSI : BaseLFSPSI {

        public override async Task<bool> WriteFile<T>(T obj) {
            var storageFolder = ApplicationData.Current.LocalFolder;

            var objInBytes = GetBytesFromT(obj);

            using (var stream = await storageFolder.OpenStreamForWriteAsync(typeof(T).Name, CreationCollisionOption.ReplaceExisting)) {
                stream.Write(objInBytes, 0, objInBytes.Length);
            }

            return true;
        }

        public override async Task<CTI<T>> GetFile<T>(Type objType) {
            var appFolder = ApplicationData.Current.LocalFolder;

            var filesinFolder = await appFolder.GetFilesAsync();

            var file = filesinFolder.FirstOrDefault(a => a.Name == typeof(T).Name);

            if (file == null) {
                return new CTI<T>(default(T), $"{typeof(T).Name} was not found");
            }

            var buffer = await FileIO.ReadBufferAsync(file);

            return new CTI<T>(GetObjectFromBytes<T>(buffer.ToArray()));
        }
    }
}