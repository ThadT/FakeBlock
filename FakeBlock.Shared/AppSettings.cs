using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace FakeBlock
{
    class AppSettings
    {
        private ApplicationDataContainer settings;

        private const string WoodSettingKeyName = "WoodSetting";
        private const string WoodDefault = "Grain1.jpg";

        public AppSettings()
        {
            this.settings = ApplicationData.Current.RoamingSettings;
        }

        public bool AddOrUpdateValue(string key, Object value)
        {
            bool valueChanged = false;
            
            // If the key exists
            if (this.settings.Values.ContainsKey(key))
            {
                if (this.settings.Values[key] != value)
                {
                    this.settings.Values[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                this.settings.Values.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value = defaultValue;

            if (this.settings.Values.ContainsKey(key))
            {
                value = (T) settings.Values[key];
            }

            return value;
        }

        public string WoodSetting
        {
            get { return GetValueOrDefault<string>(WoodSettingKeyName, WoodDefault); }
            set { AddOrUpdateValue(WoodSettingKeyName, value); }
        }
    }
}
