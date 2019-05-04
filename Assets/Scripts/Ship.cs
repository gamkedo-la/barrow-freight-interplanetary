using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float baseShipPowerCapacity = 150;
    public float currentShipPowerCapacity;
    public float currentShipPowerConsumption;
    public float baseShipTemp = 23.0f;
    public float currentShipTemp;
    public float finalShipTemp;
    public float totalShipHeatGeneration;
    public float baseShipCoolingRate = 1000;
    public float currentShipCoolingRate;
    public float maxShipSpeed;
    public float previousFrameShipSpeed = 0.01f; //in LSPS
    public float currentShipSpeed = 0.01f; //in LSPS
    public float totalShipCargoCapacity = 100;
    public float consumedShipCargoCapacity;
    public float maxShipComRange = 50;
    public int currency;

    public string textOutput;

    public Vector3 initialShipPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialShipPosition = this.transform.position;
        currentShipTemp = baseShipTemp;
        finalShipTemp = baseShipTemp;
        //Debug.Log(currentShipTemp);

        currency = 5000;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInternalShipTemp();

        //Sets text output and sends it to the ship's main monitor.
        textOutput = "Barrow Freight Interplanetary";
        textOutput += "\nShip Power usage: " + currentShipPowerConsumption + "/" + currentShipPowerCapacity + "Gw";
        textOutput += "\nCurrent  Ship Speed " + previousFrameShipSpeed + "LSPS";
        textOutput += "\nCurrent Ship Temp: " + currentShipTemp.ToString("F1") + "C";
        textOutput += "\nCurrent  Ship Cooling Rate: " + currentShipCoolingRate + "";
        textOutput += "\nShip Com Range: " + maxShipComRange + "KM";
        textOutput += "\nHeat Generation: " + totalShipHeatGeneration;
        textOutput += "\nCurrency: " + currency;

        TerminalMonitor monitor = GameObject.Find("Mission Monitor").GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        

        //Reset current power usage, so that it does not continue to increase every frame.
        currentShipPowerConsumption = 0;
        currentShipPowerCapacity = baseShipPowerCapacity;
        previousFrameShipSpeed = currentShipSpeed;
        currentShipSpeed = 10;
        currentShipCoolingRate = baseShipCoolingRate;
        maxShipComRange = 0;
        consumedShipCargoCapacity = 0;
        //currentShipTemp = baseShipTemp;
        totalShipHeatGeneration = 0;
    }

    public void UpdateShipPowerConsumption(float consumedFromTerminal) {
        currentShipPowerConsumption += consumedFromTerminal;
    }

    public void UpdateShipPowerCapacity(float capacityFromTerminal) {
        currentShipPowerCapacity += capacityFromTerminal;
    }

    public void UpdateTotalShipHeatGeneration(float heatGenerationFromTerminal) {
        totalShipHeatGeneration += heatGenerationFromTerminal;
    }

    public void UpdateInternalShipTemp() {
        finalShipTemp = Mathf.Clamp(baseShipTemp * (totalShipHeatGeneration / currentShipCoolingRate), baseShipTemp , 1000.0f );
        //Debug.Log(finalShipTemp);

        float tempDelta = finalShipTemp - currentShipTemp;
        currentShipTemp += tempDelta * (Time.deltaTime * 0.001f);
        //Debug.Log(currentShipTemp);

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

    public void MoveShipToTerminalView(Vector3 shipTerminalPosition) { 
        this.transform.position = shipTerminalPosition;
    }

    public void ReturnShipToInitialPosition() {
        this.transform.position = initialShipPosition;
    }

}
