using System;
using UnityEngine;

namespace ClockAndTime
{
    public class TimeSetter : MonoBehaviour
    {
        [SerializeField] private TimeInputWindiow _timeSetWindow;

        public void ChoseTime()
        {
            _timeSetWindow.ChoseTime(SetPlayerTime);
        }
        public void SetPlayerTime(DateTime chosenTime)
        {
            GlobalClock.instance.SetCurrentTime(chosenTime);
        }
    }
}
