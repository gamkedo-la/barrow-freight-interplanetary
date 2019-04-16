using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TerminalMonitor : MonoBehaviour
{
    private Text text;
    private Image barFillImage;
    private string monitorOutput;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        barFillImage = this.transform.Find("BarFilled").GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = monitorOutput;
    }

    public void WriteToMonitor(string output) {
        monitorOutput = output;
    }

    public void ChangeFontSize(int size) {
        text.fontSize = size;
    }

    public void ChangeAlignmentToMiddleLeft() {
        text.alignment = TextAnchor.MiddleLeft;
    }

    public void SetBarFillAmount(float fillAmount, float minValue = 0, float maxValue = 1000) {
        float fillAmountPercent = fillAmount / maxValue;
        barFillImage.fillAmount = fillAmountPercent;
    }

}
