using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class IntInputField : MonoBehaviour
{
    public Action<int> OnValueChange;

    [SerializeField] private int _minValue = 0;
    [SerializeField] private int _maxValue = 100;

    private TMP_InputField _inputField;

    public int value { get; private set; }

    private void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onValueChanged.AddListener(ValidateInput);
    }
    public void SetValue(int value)
    {
        ClampValue(value);
    }
    public void Increase(int value)
    {
        ClampValue(this.value + value);
    }
    public void Decrease(int value)
    {
        ClampValue(this.value - value);
    }
    private void ValidateInput(string text)
    {
        string validatedText = "";
        foreach (char c in text)
        {
            if (char.IsDigit(c)) validatedText += c;
        }

        if (validatedText != text)
        {
            _inputField.text = validatedText;
        }
        ClampValue(validatedText);
    }
    private void ClampValue(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            _inputField.text = _minValue.ToString();
            return;
        }

        ClampValue(int.Parse(text));
    }
    private void ClampValue(int value)
    {
        int clampedValue = Mathf.Clamp(value, _minValue, _maxValue);
        if (clampedValue == this.value)
        {
            return;
        }
        this.value = clampedValue;
        OnValueChange?.Invoke(value);
        _inputField.text = this.value.ToString();
    }
}
