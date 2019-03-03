using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int powerOutput = 0;
    public int basePowerOutput = 0;

    public string textOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textOutput = "Barrow Freight Interplanetary \n\n\nShip Power Output at " + powerOutput + "Gw";
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);
        powerOutput = basePowerOutput;
    }

    public void IncreaseShipPowerOutput(int value) {
        powerOutput += value;
    }
}
