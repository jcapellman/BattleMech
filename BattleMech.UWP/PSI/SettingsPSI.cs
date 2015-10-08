using BattleMech.PCL.Enums;
using BattleMech.PCL.PSI;

namespace BattleMech.UWP.PSI {
    public class SettingsPSI : BaseSettingsPSI {
        private readonly Windows.Storage.ApplicationDataContainer _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public override T Get<T>(SETTING_OPTIONS setting) {
            if (!_localSettings.Values.ContainsKey(setting.ToString())) {
                return default(T);
            }

            return (T)_localSettings.Values[setting.ToString()];
        }

        public override void Write<T>(SETTING_OPTIONS setting, T value) {
            _localSettings.Values[setting.ToString()] = value;
        }

        public override void Delete(SETTING_OPTIONS setting) {
            _localSettings.Values.Remove(setting.ToString());
        }
    }
}