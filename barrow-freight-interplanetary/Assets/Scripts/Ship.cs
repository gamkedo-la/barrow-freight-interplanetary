using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//It might be possible to combine this script with the Terminal script since they share similar functions.

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
        //Sets text output and sends it to the ship's main monitor.
        textOutput = "Barrow Freight Interplanetary \n\n\nShip Power Output at " + powerOutput + "Gw";
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        //Reset current power output, so that it does not continue to increase every frame.
        powerOutput = basePowerOutput;
    }

    public void IncreaseShipPowerOutput(int value) {
        powerOutput += value;
    }
}
