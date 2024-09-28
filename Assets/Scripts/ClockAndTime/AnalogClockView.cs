using System;
using UnityEngine;
using DG.Tweening;

namespace ClockAndTime
{

    public class AnalogClockView : MonoBehaviour
    {
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;
        [SerializeField] private Transform _secondHand;
        
        
        private GlobalClock _clock;

        private const float _degreeRotationOfOneMinute = 360 / 60;
        private const float _degreeRotationOfOneHour = 360 / 12;
        private const float _degreeRotationOfOneSecond = 360 / 60;

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
        public void UpdateTiveView(DateTime synchronizedTime)
        {
            float hourRotation = (synchronizedTime.Hour % 12) * _degreeRotationOfOneHour + synchronizedTime.Minute * 0.5f;
            float minuteRotation = synchronizedTime.Minute * _degreeRotationOfOneMinute + synchronizedTime.Second * 0.1f;
            float secondRotation = synchronizedTime.Second * _degreeRotationOfOneSecond;
            _hourHand.DORotate(new Vector3(0, 0, -hourRotation), 1f); 
            _minuteHand.DORotate(new Vector3(0, 0, -minuteRotation), 1f);
            _secondHand.DORotate(new Vector3(0, 0, -secondRotation), 1f); 
        }
    }
}
