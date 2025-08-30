using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class IOSNotification : MonoBehaviour
{
    [SerializeField] private string notificationId;
    [SerializeField] private int day, hour, minute, second;

#if UNITY_IOS
    public void IOSNotificationManager()
    {
        iOSNotificationCenter.ScheduleNotification(CreateNotification());
    }

    private iOSNotification CreateNotification()
    {
        return new iOSNotification()
        {
            Identifier = notificationId,
            Title = "Example title",
            Subtitle = "",
            Body = "",
            ShowInForeground = false,
            ForegroundPresentationOption = (PresentationOption.None | PresentationOption.Sound),
            CategoryIdentifier = "categoryId",
            ThreadIdentifier = "threadId",
            Trigger = CreateTimeTrigger()
        };
    }

    private iOSNotificationTimeIntervalTrigger CreateTimeTrigger()
    {
        return new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new System.TimeSpan(day, hour, minute, second), // 10 secs
            Repeats = false
        };
    }

    private void OnplicationFocus(bool focus)
    {
        if (!focus)
        {
            IOSNotificationManager();
        }
        else
        {
            iOSNotificationCenter.RemoveAllScheduledNotifications();
            //iOSNotificationCenter.RemoveScheduledNotification(notificationId);
        }        
    }
#endif
}
