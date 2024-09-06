using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHand : MonoBehaviour, IDragHandler
{
    public float value { get; private set; }
    public Action<float> OnValueChange;

    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 100;
    [SerializeField] private float _minStep = 1.0f;

    private float _currentAngle = 0f;
    private float _lastValue = 0f;
    private float _startAngle;
    private const float _maxEngle = 360;

    private void Start()
    {
        _startAngle = transform.rotation.y + 90;
        _currentAngle = NormalizeAngle(transform.eulerAngles.z);
        _lastValue = GetValueFromAngle(_currentAngle);
    }
    public void SetNewSettings(float minValue, float maxValue, float minStep)
    {
        this._minValue = minValue;
        this._maxValue = maxValue;
        this._minStep = minStep;
    }
    public void SetValue(float value)
    {
        float clampedValue = Math.Clamp(value, _minValue, _maxValue);
        if (this.value == clampedValue)
        {
            return;
        }
        float angle = GetAnsleFromValue(value);
        SetRorarion(angle);
        _lastValue = this.value;
        this.value = clampedValue;
        OnValueChange?.Invoke(this.value);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = eventData.position;
        Vector3 handPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - handPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = NormalizeAngle(angle);

        UpdateValueIfMoreThenMinStep(angle);
    }
    private float NormalizeAngle(float angle)
    {
        if (angle < 0) angle += _maxEngle;
        if (angle >= _maxEngle) angle -= _maxEngle;
        return angle;
    }
    private float GetValueFromAngle(float angle)
    {
        float stepSize = _maxEngle / (_maxValue - _minValue);
        float value;
        value = Mathf.RoundToInt(angle / stepSize);
        value = _maxValue - value;
        return Mathf.Clamp(value, _minValue, _maxValue);
    }
    private float GetAnsleFromValue(float value)
    {
        return _maxEngle - (_maxEngle / _maxValue - _minValue) * value;
    }
    private void UpdateValueIfMoreThenMinStep(float newAngle)
    {
        float relativeAngle = NormalizeAngle(newAngle - _startAngle);

        float value = GetValueFromAngle(relativeAngle);
        if (Mathf.Abs(value - _lastValue) >= _minStep)
        {
            SetValue(value);
        }
    }
    private void SetRorarion(float angle)
    {
        _currentAngle = angle;
        transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
    }
}
