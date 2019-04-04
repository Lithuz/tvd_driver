// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace tvd_driver.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";

        private const string userId = "userId";
        private const string userPass = "userPass";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(userId, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userId, value);
            }
        }

        public static string UserPass
        {
            get
            {
                return AppSettings.GetValueOrDefault(userPass, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userPass, value);
            }
        }

    }
}