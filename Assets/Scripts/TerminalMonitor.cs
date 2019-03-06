using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TerminalMonitor : MonoBehaviour
{
    public string monitorOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text text = GetComponentInChildren<Text>();
        text.text = monitorOutput;
    }

    public void WriteToMonitor(string output) {
        monitorOutput = output;
    }
}
