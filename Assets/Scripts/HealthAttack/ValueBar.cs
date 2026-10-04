using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    [SerializeField] Image barBase;
    [SerializeField] Image barDisplay;

    [SerializeField] float currentValue, maxValue, minValue;
    [SerializeField] bool hideIfMax = true;
    private void HideBar()
    {
        barBase.enabled = false;
        barDisplay.enabled = false;
    }
    private void ShowBar()
    {
        barBase.enabled = true;
        barDisplay.enabled = true;
    }
    public void Setup(float newCurrent, float newMax, float newMin = 0)
    {
        currentValue = newCurrent;
        maxValue = newMax;
        minValue = newMin;

        if (barBase == null || barDisplay == null)
        {
            Debug.LogError("VALUE BAR INCORRECTLY SET UP");
            return;
        }

        barDisplay.rectTransform.sizeDelta = new Vector2(barBase.rectTransform.sizeDelta.x * (currentValue - minValue) / (maxValue - minValue), 0);
        if (hideIfMax && currentValue == maxValue)
            HideBar();
        else
            ShowBar();
    }

    public void UpdateValue(float newValue)
    {
        currentValue = Mathf.Clamp(newValue, minValue, maxValue);

        if (barBase == null || barDisplay == null)
        {
            Debug.LogError("VALUE BAR INCORRECTLY SET UP");
            return;
        }

        barDisplay.rectTransform.sizeDelta = new Vector2(barBase.rectTransform.sizeDelta.x * (currentValue - minValue)/(maxValue - minValue), 0);
        if (hideIfMax && currentValue == maxValue)
            HideBar();
        else
            ShowBar();
    }

}
