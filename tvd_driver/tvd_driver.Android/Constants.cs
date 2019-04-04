namespace tvd_driver.Droid
{
    public static class Constants
    {
        public static readonly int NOTIFICATION_ID = 1000;
        public static readonly string CHANNEL_ID = "tvd_notification";
        public const string SenderID = "91160039920";
        public const string ListenConnectionString = "Endpoint=sb://tvdnotifications.servicebus.windows.net/;" +
            "SharedAccessKeyName=DefaultListenSharedAccessSignature;" +
            "SharedAccessKey=gn3s8cdnqTOjiJOlmIJk34HTvS92V2uF+mEQ9pvfdvM=";
        public const string NotificationHubName = "tvdNotifications";
    }
}