using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    public string redStatus = "offline";
    public string blueStatus = "offline";
    public string greenStatus = "offline";

    public string textOutput;

    public int basePowerOutput = 0;
    public int powerOutput = 0;


    // Start is called before the first frame update
    void Start()
    {
        powerOutput = basePowerOutput;
    }

    // Update is called once per frame
    void Update()
    {
        textOutput = "Terminal Power Output at " + powerOutput + "Gw";
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        Ship ship = GetComponentInParent<Ship>();
        ship.IncreaseShipPowerOutput(powerOutput);

        powerOutput = basePowerOutput;
    }

    public void IncreasePowerOutput(int value) {
        powerOutput += value;
    }
}
