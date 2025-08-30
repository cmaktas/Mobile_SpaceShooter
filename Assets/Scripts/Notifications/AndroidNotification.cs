using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNotification : MonoBehaviour
{
    [SerializeField] private int notificationId;
    [SerializeField] private string channelId;
#if UNITY_ANDROID
    public void AndroidNotificationManager(DateTime fireTime)
    {
        AndroidNotificationCenter.RegisterNotificationChannel(CreateChannel());
        AndroidNotificationCenter.SendNotification(CreateNotification(fireTime), channelId);
        // AndroidNotificationCenter.SendNotificationWithExplicitID(CreateNotification(fireTime), channelId, notificationId);
    }

    private void OnAplicationFocus(bool focus)
    {
        if (!focus)
        {
            DateTime fireTime = DateTime.Now.AddHours(3);
            AndroidNotificationManager(fireTime);
        }
        else
        {
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            // AndroidNotificationCenter.CancelScheduledNotification(notificationId);
        }
    }
    
    private AndroidNotificationChannel CreateChannel()
    {
        return new AndroidNotificationChannel()
        {
            Id = channelId,
            Name = "Example Channel Name",
            Description = "Example Channle desc.",
            Importance = Importance.Default
        };
    }

    private Unity.Notifications.Android.AndroidNotification CreateNotification(DateTime fireTime)
    {
        return new Unity.Notifications.Android.AndroidNotification()
        {
            Title = "Example title",
            Text = "Example text",
            SmallIcon = "small_icon",
            LargeIcon = "large_icon",
            FireTime = fireTime
        };
    }
#endif
}
