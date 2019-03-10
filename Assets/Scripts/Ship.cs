using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//It might be possible to combine this script with the Terminal script since they share similar functions.

public class Ship : MonoBehaviour
{
    public float baseShipPowerCapacity = 150;
    public float currentShipPowerCapacity;
    public float currentShipPowerConsumption;
    public float internalShipTemp;
    public float maxShipSpeed;
    public float currentShipSpeed = 10;
    public float totalShipCargoCapacity = 100;
    public float consumedShipCargoCapacity;
    public float maxShipComRange = 50;

    public string textOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Sets text output and sends it to the ship's main monitor.
        textOutput = "Barrow Freight Interplanetary \n\n\nPower Consumed " + currentShipPowerConsumption + "Gw";
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        //Reset current power usage, so that it does not continue to increase every frame.
        currentShipPowerConsumption = 0;
        currentShipPowerCapacity = baseShipPowerCapacity;
    }

    public void UpdateShipPowerConsumption(float consumedFromTerminal) {
        currentShipPowerConsumption += consumedFromTerminal;
    }

    public void UpdateShipPowerCapacity(float capacityFromTerminal) {
        currentShipPowerCapacity += capacityFromTerminal;
    }
}
