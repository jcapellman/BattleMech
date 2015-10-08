using BattleMech.PCL.Enums;

namespace BattleMech.PCL.PSI {
    public abstract class BaseSettingsPSI {
        public abstract T Get<T>(SETTING_OPTIONS setting);

        public abstract void Write<T>(SETTING_OPTIONS setting, T value);

        public abstract void Delete(SETTING_OPTIONS setting);
    }
}