using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    public string redStatus = "offline";
    public string blueStatus = "offline";
    public string greenStatus = "offline";

    public string textOutput;

    public int baseEfficiency = 0;
    public int efficiency = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textOutput = "Terminal Efficiency at " + efficiency + "%";
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);
        efficiency = baseEfficiency;

        Ship ship = GetComponentInParent<Ship>();
        ship.IncreaseEfficiency(efficiencyBonus);
    }

    public void IncreaseEfficiency(int value) {
        efficiency += value;
    }
}
