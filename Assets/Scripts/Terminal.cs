using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    public string textOutput;

    public int basePowerOutput = 0;
    public int powerOutput = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Sets text output and sends it to the terminal's monitor.
        textOutput = "Terminal Power Output at " + powerOutput + "Gw";
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        //sends terminal total values to the Ship script.
        Ship ship = GetComponentInParent<Ship>();
        ship.IncreaseShipPowerOutput(powerOutput);

        //Reset current power output, so that it does not continue to increase every frame.
        powerOutput = basePowerOutput;
    }

    public void IncreasePowerOutput(int value) {
        powerOutput += value;
    }
}
