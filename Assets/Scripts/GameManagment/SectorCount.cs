using TMPro;
using UnityEngine;

public class SectorCount : MonoBehaviour
{
    [SerializeField] string prefixText = "Current Sector: ";

    private TMP_Text selfTMP;

    private void Awake()
    {
        selfTMP = GetComponent<TMP_Text>();
    }
    public void SetCount(int newScore)
    {
        selfTMP.text = prefixText + (newScore + 1).ToString();
    }
}