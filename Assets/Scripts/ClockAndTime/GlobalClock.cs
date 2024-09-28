using System;
using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

namespace ClockAndTime
{
    public class GlobalClock : Singletone<GlobalClock>
    {
        public Action<DateTime> OnClockTimeChange;
        public DateTime currentTime { get => _globalTime + _userOffsetTime; private set {
            if(value != _globalTime)
                {
                    _globalTime = value;
                    OnClockTimeChange?.Invoke(currentTime);
                }
            } 
        }

        private DateTime _globalTime;
        private TimeSpan _userOffsetTime = TimeSpan.Zero;
        private TimeSynchronization _timeSynchronization;

        private void Start()
        {
            currentTime = DateTime.Now;
            _timeSynchronization = new TimeSynchronization();


            InvokeRepeating(nameof(SynchronizeTime), 3600f, 3600f);
            StartCoroutine(UpdateTime());
        }

        public void SetCurrentTime(DateTime time)
        {
            _userOffsetTime = TimeSpan.Zero;
            _userOffsetTime = time - currentTime;
        }
        private IEnumerator UpdateTime()
        {
            while (true)
            {
                currentTime = _globalTime.AddSeconds(Time.deltaTime);
                yield return null;
            }
        }
        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                currentTime = DateTime.Now;
                _timeSynchronization = new TimeSynchronization();
            }
        }
        private async Task SynchronizeTime()
        {
            DateTime synchronizedTime = await _timeSynchronization.GetTimeFromAPI();
            if (synchronizedTime != DateTime.MinValue)
            {
                currentTime = synchronizedTime;
            }
        }
    }
}

