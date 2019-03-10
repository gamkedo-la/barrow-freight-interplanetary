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
    public float currentShipCoolingRate;
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
        textOutput = "Barrow Freight Interplanetary \n\n";
        textOutput += "\nShip Power usage: " + currentShipPowerConsumption + "/" + currentShipPowerCapacity + "Gw";
        textOutput += "\nCurrent  Ship Speed " + currentShipSpeed + "KM/H";
        textOutput += "\nInternal Ship Temp: " + internalShipTemp + "C";
        textOutput += "\nCurrent  Ship Cooling Rate: " + currentShipCoolingRate + "";
        textOutput += "\nShip Com Range: " + maxShipComRange + "KM";


        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        //Reset current power usage, so that it does not continue to increase every frame.
        currentShipPowerConsumption = 0;
        currentShipPowerCapacity = baseShipPowerCapacity;
        currentShipSpeed = 10;
        currentShipCoolingRate = 0;
        maxShipComRange = 0;
        consumedShipCargoCapacity = 0;
        internalShipTemp = 0;
    }

    public void UpdateShipPowerConsumption(float consumedFromTerminal) {
        currentShipPowerConsumption += consumedFromTerminal;
    }

    public void UpdateShipPowerCapacity(float capacityFromTerminal) {
        currentShipPowerCapacity += capacityFromTerminal;
    }

    public void UpdateInternalShipTemp(float heatGenerationFromTerminal) {
        internalShipTemp += heatGenerationFromTerminal;
    }

    public void UpdateShipCoolingRate(float coolingRateFromTerminal) {
        currentShipCoolingRate += coolingRateFromTerminal;
    }

    public void UpdateShipMaxComRange(float comRangeFromTerminal) {
        maxShipComRange += comRangeFromTerminal;
    }

    public void UpdateCurrentShipSpeed(float engineSpeedFromTerminal ) {
        currentShipSpeed += engineSpeedFromTerminal;
    }
}
