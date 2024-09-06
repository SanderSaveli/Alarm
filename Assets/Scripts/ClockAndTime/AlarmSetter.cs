using System;
using TMPro;
using UnityEngine;
#if UNITY_ANDROID
    using Unity.Notifications.Android;
#endif

namespace ClockAndTime
{
    public class AlarmSetter : MonoBehaviour
    {
        [SerializeField] private AlarmClock _alarmClock;
        [SerializeField] private TimeInputWindiow _timeSetWindow;
        [SerializeField] private TMP_Text _alarmStatusText;

        private void OnEnable()
        {
            _alarmClock.OnAlarmTriggered += ShowAlarmTriggerMessage;
        }
        private void OnDisable()
        {
            _alarmClock.OnAlarmTriggered -= ShowAlarmTriggerMessage;
        }
        public void ChoseTime()
        {
            _timeSetWindow.ChoseTime(ActivateAlarm);
        }
        public void ActivateAlarm(DateTime chosenTime)
        {
            if (_alarmClock.SetAlarm(chosenTime))
            {
                _alarmStatusText.text = "Будильник установлен на " + chosenTime.Hour + " : " + chosenTime.Minute;
            }
            else
            {
                _alarmStatusText.text = "Невозможно установить будильник на прошедшее время!";
            }
        }
        private void ShowAlarmTriggerMessage(DateTime time)
        {
            _alarmStatusText.text = "Будильник сработал!";
            SendAndroidNotification("Будильник", "Будильник сработал!");
        }
        private void SendAndroidNotification(string title, string text)
        {
#if UNITY_ANDROID
            const string channelId = "alarm_channel";

            var notificationChannel = new AndroidNotificationChannel()
            {
                Id = channelId,
                Name = "Будильник",
                Importance = Importance.High,
                Description = "Оповещения о будильнике",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

            var notification = new AndroidNotification()
            {
                Title = title,
                Text = text,
                SmallIcon = "default",
                LargeIcon = "default",
                FireTime = DateTime.Now.AddSeconds(1)
            };

            AndroidNotificationCenter.SendNotification(notification, channelId);
#endif
        }
    }
}
