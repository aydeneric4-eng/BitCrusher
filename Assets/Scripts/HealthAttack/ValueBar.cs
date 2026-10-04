using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    [SerializeField] Image barBase;
    [SerializeField] Image barDisplay;

    [SerializeField] float currentValue, maxValue, minValue;

    public void Setup(float newCurrent, float newMax, float newMin = 0)
    {
        currentValue = newCurrent;
        maxValue = newMax;
        minValue = newMin;
    }

    public void UpdateValue(float newValue)
    {
        currentValue = Mathf.Clamp(newValue, minValue, maxValue);

        barDisplay.rectTransform.sizeDelta = new Vector2(barBase.rectTransform.sizeDelta.x * (currentValue - minValue)/(maxValue - minValue), 0);
    }

}
