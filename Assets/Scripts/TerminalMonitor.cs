using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TerminalMonitor : MonoBehaviour
{
    Text text;
    public string monitorOutput;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
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
}
