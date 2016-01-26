using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace MirrorCompanion.Notifications
{
    public static class NotificationManager
    {
        
        /// <summary>
        /// Notify immediately using the type of notification
        /// </summary>
        /// <param name="noticeText"></param>
        /// <param name="notificationType"></param>
        public static void NotifyNow(string noticeText, NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.Toast:
                    //TODO: Make this hit and deserialize a real XML file
                    string toast = string.Format("<toast>"
                        + "<visual>"
                        + "<binding template = \"ToastGeneric\" >"
                        + "<image placement=\"appLogoOverride\" src=\"Assets\\CompanionAppIcon44x44.png\" />"
                        + "<text>{0}</text>"
                        + "</binding>"
                        + "</visual>"
                        + "</toast>", noticeText);

                    XmlDocument toastDOM = new XmlDocument();
                    toastDOM.LoadXml(toast);
                    ToastNotificationManager.CreateToastNotifier().Show(
                        new ToastNotification(toastDOM));
                    break;
                case NotificationType.Tile:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Schedule a notification of the specified date and type.
        /// </summary>
        /// <param name="noticeText"></param>
        /// <param name="dueTime"></param>
        /// <param name="notificationType"></param>
        public static void ScheduleNotification(string noticeText, DateTime dueTime, NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.Toast:
                    string toast = string.Format("<toast>"
                        + "<visual>"
                        + "<binding template = \"ToastGeneric\" >"
                        + "<image placement=\"appLogoOverride\" src=\"Assets\\CompanionAppIcon44x44.png\" />"
                        + "<text>{0}</text>"
                        + "</binding>"
                        + "</visual>"
                        + "</toast>", noticeText);

                    XmlDocument toastDOM = new XmlDocument();
                    toastDOM.LoadXml(toast);
                    ScheduledToastNotification toastNotification = new ScheduledToastNotification(toastDOM, dueTime) { Id = "Note_Reminder" };
                    ToastNotificationManager.CreateToastNotifier().AddToSchedule(toastNotification);
                    break;
                case NotificationType.Tile:
                    //TODO: Tile updates
                    throw new NotImplementedException();
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Notifcation Types to choose from
    /// </summary>
    public enum NotificationType
    {
        Toast,
        Tile
    }
}