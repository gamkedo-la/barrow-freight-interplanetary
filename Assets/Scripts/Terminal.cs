﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    private float powerConsumption = 0;
    private float powerCapacity = 0;
    private float heatGeneration = 0;
    private float coolingRate = 0;
    private float efficiencyRating = 0;
    private float engineSpeed = 0;
    private float navSpeedBonus = 0;
    private float comRangeBoost = 0;
    private float efficiencyBonus = 1;

    public string textOutput;
    private string label;
    private string positiveAttributeLabel;
    private float positiveAttribute;
    private string positiveAttributeUnit;
    private float basePositiveAttribute;

    public enum ShipAttributeTypes { PowerCapacity, Temperature, Speed, CargoCapacity, ComRange };
    public ShipAttributeTypes shipAttributeType;

    public enum TerminalTypes { PowerGenerator, CoolingUnit, EngineControl, NAVCOMComputer, Other };
    public TerminalTypes terminalType;

    

    // Start is called before the first frame update
    void Start() {

        Renderer rend = GetComponent<Renderer>();

        switch (terminalType) {
            case TerminalTypes.PowerGenerator:
                rend.material.color = Color.yellow;
                label = "Power Generator";
                powerCapacity = 1000;
                heatGeneration = 500;
                positiveAttributeLabel = "Power Generated:\n";
                positiveAttribute = powerCapacity;
                positiveAttributeUnit = "Gw";
                break;
            case TerminalTypes.CoolingUnit:
                rend.material.color = Color.blue;
                label = "Cooling Unit";
                powerConsumption = 100;
                coolingRate = 300;
                positiveAttributeLabel = "Heat Dissipated:\n"; 
                positiveAttribute = coolingRate;
                positiveAttributeUnit = "BTUs";
                break;
            case TerminalTypes.EngineControl:
                rend.material.color = Color.red;
                label = "Engine Control";
                powerConsumption = 200;
                heatGeneration = 500;
                engineSpeed = 500;
                positiveAttributeLabel = "Engine Speed:\n";
                positiveAttribute = engineSpeed;
                positiveAttributeUnit = "KPH";
                break;
            case TerminalTypes.NAVCOMComputer:
                label = "NAV/COM Computer";
                rend.material.color = Color.green;
                powerConsumption = 50;
                heatGeneration = 50;
                comRangeBoost = 100;
                positiveAttributeLabel = "Com Range:\n";
                positiveAttribute = comRangeBoost;
                positiveAttributeUnit = "KM";
                break;
            default:
            break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Ship ship = GetComponentInParent<Ship>();

        basePositiveAttribute = positiveAttribute;
        positiveAttribute *= efficiencyBonus;

        if (terminalType == TerminalTypes.PowerGenerator) {
            //sends power capacity value to the Ship script.
            ship.UpdateShipPowerCapacity(positiveAttribute);
        }

        if (terminalType == TerminalTypes.CoolingUnit) {
            //sends cooling rate value to the Ship script.
            ship.UpdateShipCoolingRate(positiveAttribute);
        }

        if (terminalType == TerminalTypes.EngineControl) { 
            //sends engine speed value to the Ship script.
            ship.UpdateCurrentShipSpeed(positiveAttribute);
        }

        if (terminalType == TerminalTypes.NAVCOMComputer) {
            //sends com range value to the Ship script.
            ship.UpdateShipMaxComRange(positiveAttribute);
        }

        //Sets text output and sends it to the terminal's monitor.
        if (terminalType != TerminalTypes.Other) {
            textOutput = label + "\n" + positiveAttributeLabel + positiveAttribute + positiveAttributeUnit;
            TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
            monitor.WriteToMonitor(textOutput);  
        }
        //sends power consumption value to the Ship script.
        ship.UpdateShipPowerConsumption(powerConsumption);

        //sends heat generation value to the Ship script.
        ship.UpdateInternalShipTemp(heatGeneration);

        //Reset current positiveAttribute and efficiency values, so they does not continue to increase every frame.
        positiveAttribute = basePositiveAttribute;
        efficiencyBonus = 1;
    }

    public void IncreaseEfficiency(float value) {
        efficiencyBonus += value - 1;
    }
}
