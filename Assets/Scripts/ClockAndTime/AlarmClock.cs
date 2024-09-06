using System;
using UnityEngine;

namespace ClockAndTime
{
    public class AlarmClock : MonoBehaviour
    {
        public Action<DateTime> OnAlarmTriggered;

        
        private GlobalClock _clock;

        private DateTime _alarmTime;
        private bool _isAlarmSet = false;

        private void Start()
        {
            _clock = GlobalClock.instance;
        }
        private void CheckAlarmTrigger()
        {
            if (_isAlarmSet && _alarmTime.TimeOfDay <= _clock.currentTime.TimeOfDay)
            {
                _isAlarmSet = false;
                TriggerAlarm();
            }
        }
        private void Update()
        {
            if (_isAlarmSet && _alarmTime.TimeOfDay <= _clock.currentTime.TimeOfDay)
            {
                _isAlarmSet = false;
                TriggerAlarm();
            }
        }
        public bool SetAlarm(DateTime time)
        {
            DateTime currentTime = DateTime.Now;

            if (time.TimeOfDay <= currentTime.TimeOfDay || time.Date < currentTime.Date)
            {
                return false;
            }

            _alarmTime = time;
            _isAlarmSet = true;

            InvokeRepeating(nameof(CheckAlarmTrigger), 1f, 36000f);
            return true;
        }
        private void TriggerAlarm()
        {
            OnAlarmTriggered?.Invoke(_alarmTime);
        }
    }
}
