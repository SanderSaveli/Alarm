using System;
using UnityEngine;

namespace ClockAndTime
{

    public class TimeInputWindiow : MonoBehaviour
    {
        [SerializeField] private IntInputField _inthourInput;
        [SerializeField] private IntInputField _intminuteInput;

        [SerializeField] private InputHand _minuteInputHand;
        [SerializeField] private InputHand _hourInputHand;

        [SerializeField] private GameObject _timeSetWindow;

        private Action<DateTime> _callback;

        private void OnEnable()
        {
            SubscribeToEvents();
            SetMinuteInputFieldValue(0);
            SetHourInputFieldValue(0);
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        public void ChoseTime(Action<DateTime> callback)
        {
            this._callback = callback;
            _timeSetWindow.SetActive(true);
        }
        public void CloseWindow()
        {
            this._callback = null;
            _timeSetWindow.SetActive(false);
        }
        public void SetTime()
        {
            int hour = _inthourInput.value;
            int minute = _intminuteInput.value;

            DateTime inputDateTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour, minute, 0
                );

            _callback?.Invoke(inputDateTime);
            _timeSetWindow.SetActive(false);
        }
        private void SubscribeToEvents()
        {
            _hourInputHand.OnValueChange += SetHourInputFieldValue;
            _minuteInputHand.OnValueChange += SetMinuteInputFieldValue;

            _inthourInput.OnValueChange += SetHandHourValue;
            _intminuteInput.OnValueChange += SetHandMinuteValue;
        }
        private void UnsubscribeFromEvents()
        {
            _hourInputHand.OnValueChange -= SetHourInputFieldValue;
            _minuteInputHand.OnValueChange -= SetMinuteInputFieldValue;

            _inthourInput.OnValueChange -= SetHandHourValue;
            _intminuteInput.OnValueChange -= SetHandMinuteValue;
        }
        private void SetHourInputFieldValue(float value)
        {
            _inthourInput.SetValue(Mathf.RoundToInt(value));
        }
        private void SetMinuteInputFieldValue(float value)
        {
            _intminuteInput.SetValue(Mathf.RoundToInt(value));
        }
        private void SetHandMinuteValue(int value)
        {
            _minuteInputHand.SetValue(value);
        }
        private void SetHandHourValue(int value)
        {
            _hourInputHand.SetValue(value);
        }
    }
}
