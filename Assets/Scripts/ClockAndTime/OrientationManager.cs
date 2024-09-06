using UnityEngine;

namespace ClockAndTime
{



    public class OrientationManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private RectTransform clock;
        [SerializeField] private RectTransform alarmStatusText;
        [SerializeField] private RectTransform setAlarmButton;
        [SerializeField] private RectTransform window;
        [SerializeField] private RectTransform windowClock;
        [SerializeField] private RectTransform buttons;
        [SerializeField] private RectTransform time;

        [Header("Portrait Configuration")]
        [SerializeField] private RectTransform clockPortrait;
        [SerializeField] private RectTransform alarmStatusTextPortrait;
        [SerializeField] private RectTransform setAlarmButtonPortrait;
        [SerializeField] private RectTransform windowPortrait;
        [SerializeField] private RectTransform windowClockPortrait;
        [SerializeField] private RectTransform buttonsPortrait;
        [SerializeField] private RectTransform timePortrait;

        [Header("Landscape Configuration")]
        [SerializeField] private RectTransform clockLandscape;
        [SerializeField] private RectTransform alarmStatusTextLandscape;
        [SerializeField] private RectTransform setAlarmButtonLandscape;
        [SerializeField] private RectTransform windowLandscape;
        [SerializeField] private RectTransform windowClockLandscape;
        [SerializeField] private RectTransform buttonsLandscape;
        [SerializeField] private RectTransform timeLandscape;

        private void Start()
        {
            AdjustUIForOrientation(Screen.orientation);
        }

        private void Update()
        {
            if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
                Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                AdjustUIForOrientation(ScreenOrientation.LandscapeLeft);
            }
            else if (Screen.orientation == ScreenOrientation.Portrait ||
                Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                AdjustUIForOrientation(ScreenOrientation.Portrait);
            }
        }

        private void AdjustUIForOrientation(ScreenOrientation orientation)
        {
            RectTransform sourceRectTransformClock = null;
            RectTransform sourceRectTransformAlarmStatusText = null;
            RectTransform sourceRectTransformSetAlarmButton = null;
            RectTransform sourceRectTransformWindow = null;
            RectTransform sourceRectTransformWindowClock = null;
            RectTransform sourceRectTransformButtons = null;
            RectTransform sourceRectTransformTime = null;

            if (orientation == ScreenOrientation.Portrait)
            {
                sourceRectTransformClock = clockPortrait;
                sourceRectTransformAlarmStatusText = alarmStatusTextPortrait;
                sourceRectTransformSetAlarmButton = setAlarmButtonPortrait;
                sourceRectTransformWindow = windowPortrait;
                sourceRectTransformWindowClock = windowClockPortrait;
                sourceRectTransformButtons = buttonsPortrait;
                sourceRectTransformTime = timePortrait;
            }
            else if (orientation == ScreenOrientation.LandscapeLeft)
            {
                sourceRectTransformClock = clockLandscape;
                sourceRectTransformAlarmStatusText = alarmStatusTextLandscape;
                sourceRectTransformSetAlarmButton = setAlarmButtonLandscape;
                sourceRectTransformWindow = windowLandscape;
                sourceRectTransformWindowClock = windowClockLandscape;
                sourceRectTransformButtons = buttonsLandscape;
                sourceRectTransformTime = timeLandscape;
            }

            CopyRectTransformValues(clock, sourceRectTransformClock);
            CopyRectTransformValues(alarmStatusText, sourceRectTransformAlarmStatusText);
            CopyRectTransformValues(setAlarmButton, sourceRectTransformSetAlarmButton);
            CopyRectTransformValues(window, sourceRectTransformWindow);
            CopyRectTransformValues(windowClock, sourceRectTransformWindowClock);
            CopyRectTransformValues(buttons, sourceRectTransformButtons);
            CopyRectTransformValues(time, sourceRectTransformTime);
        }

        private void CopyRectTransformValues(RectTransform target, RectTransform source)
        {
            target.anchorMin = source.anchorMin;
            target.anchorMax = source.anchorMax;
            target.anchoredPosition = source.anchoredPosition;
            target.sizeDelta = source.sizeDelta;
            target.pivot = source.pivot;
            target.localScale = source.localScale;
        }
    }
}
