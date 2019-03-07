using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    public string textOutput;

    public int basePowerOutput = 0;
    public int powerOutput = 0;

    private string label;

    private float powerConsumption = 0;
    private float heatGeneration = 0;
    private float efficiencyRating = 0;
    private float engineSpeed = 0;
    private float navSpeedBonus = 0;
    private float comRangeBoost = 0;
    private float efficiencyBonus = 1;

    private string positiveAttributeLabel;
    private float positiveAttribute;
    private string positiveAttributeUnit;
    private float basePositiveAttribute;

    public enum ShipAttributeTypes { PowerCapacity, Temperature, Speed, CargoCapacity, ComRange };
    public ShipAttributeTypes shipAttributeType;

    public enum TerminalTypes { PowerGenerator, CoolingUnit, EngineControl, NAVCOMComputer };
    public TerminalTypes terminalType;


    // Start is called before the first frame update
    void Start() {

        Renderer rend = GetComponent<Renderer>();

        switch (terminalType) {
            case TerminalTypes.PowerGenerator:
                rend.material.color = Color.yellow;
                label = "Power Generator";
                powerConsumption = -1000;
                heatGeneration = 500;
                positiveAttributeLabel = "Power Generated:\n";
                positiveAttribute = -powerConsumption;
                positiveAttributeUnit = "Gw";
                break;
            case TerminalTypes.CoolingUnit:
                rend.material.color = Color.blue;
                label = "Cooling Unit";
                powerConsumption = 100;
                heatGeneration = -300;
                positiveAttributeLabel = "Heat Dissipated:\n";
                positiveAttribute = -heatGeneration;
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
        basePositiveAttribute = positiveAttribute;
        positiveAttribute *= efficiencyBonus;

        if (terminalType == TerminalTypes.PowerGenerator) {
            powerConsumption = -positiveAttribute;
        }

        //Sets text output and sends it to the terminal's monitor.
        textOutput = label + "\n"+ positiveAttributeLabel + positiveAttribute + positiveAttributeUnit;
        TerminalMonitor monitor = GetComponentInChildren<TerminalMonitor>();
        monitor.WriteToMonitor(textOutput);

        //sends power consumption value to the Ship script.
        Ship ship = GetComponentInParent<Ship>();
        ship.UpdateShipPowerConsumption(-powerConsumption);

        //Reset current positiveAttribute and efficiency values, so they does not continue to increase every frame.
        positiveAttribute = basePositiveAttribute;
        efficiencyBonus = 1;
    }

    public void IncreaseEfficiency(float value) {
        efficiencyBonus += value - 1;
    }
}
