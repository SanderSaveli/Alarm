using System;
using TMPro;
using UnityEngine;

namespace ClockAndTime
{
    public class DigitalClockView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _digitalClockTime;
        
        private GlobalClock _clock;

        private void Awake()
        {
            _clock = GlobalClock.instance;
        }
        private void OnEnable()
        {
            _clock.OnClockTimeChange += UpdateTiveView;
        }
        private void OnDisable()
        {
            _clock.OnClockTimeChange -= UpdateTiveView;
        }
        public void UpdateTiveView(DateTime time)
        {
            _digitalClockTime.text = time.ToString("HH:mm:ss");
        }
    }
}
