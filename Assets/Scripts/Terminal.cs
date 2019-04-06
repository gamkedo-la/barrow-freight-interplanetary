using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    private float powerConsumption = 50;
    private float powerCapacity = 0;
    private float heatGeneration = 50;
    private float coolingRate = 0;
    private float efficiencyRating = 0;
    private float engineSpeed = 0;
    private float navSpeedBonus = 0;
    private float comRangeBoost = 0;
    private float efficiencyBonus = 1;
    private float baseFailureRate = 0.03f;
    private float currentFailureRate;
    private float failureChanceInterval = 30.0f; //in seconds
    private bool terminalFailure = false;

    public string textOutput;
    private string label;
    private string positiveAttributeLabel;
    private float positiveAttribute;
    private string positiveAttributeUnit;
    private float basePositiveAttribute;

    public enum ShipAttributeTypes { PowerCapacity, Temperature, Speed, CargoCapacity, ComRange };
    public ShipAttributeTypes shipAttributeType;

    public enum TerminalTypes { PowerGenerator, CoolingUnit, EngineControl, NAVCOMComputer, JobSelection, TerminalStore, Other };
    public TerminalTypes terminalType;

    private Jobs jobsManager;
    private TerminalStore terminalStore;
    private TerminalMonitor monitor;
    public AudioSource terminalStartupAndRunningSound;

    // Start is called before the first frame update
    void Start() {

        currentFailureRate = baseFailureRate;

        jobsManager = GameObject.Find("Game Managers").GetComponent<Jobs>();
        terminalStore = GameObject.Find("Game Managers").GetComponent<TerminalStore>();
        monitor = GetComponentInChildren<TerminalMonitor>();

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

        if (ship.currentShipPowerConsumption > ship.currentShipPowerCapacity && terminalType != TerminalTypes.PowerGenerator) {
            positiveAttribute = 0;
        }

        if (terminalFailure) {
            positiveAttribute = 0;
        }

        if (ship.currentShipTemp > 35.0f){
            currentFailureRate = baseFailureRate + 0.3f;
        }

        switch (terminalType) {
            case TerminalTypes.PowerGenerator:
                //sends power capacity value to the Ship script.
                ship.UpdateShipPowerCapacity(positiveAttribute);
                DisplayTerminalInfo();
                break;
            case TerminalTypes.CoolingUnit:
                //sends cooling rate value to the Ship script.
                ship.UpdateShipCoolingRate(positiveAttribute);
                DisplayTerminalInfo();
                break;
            case TerminalTypes.EngineControl:
                //sends engine speed value to the Ship script.
                ship.UpdateCurrentShipSpeed(positiveAttribute);
                DisplayTerminalInfo();
                break;
            case TerminalTypes.NAVCOMComputer:
                //sends com range value to the Ship script.
                ship.UpdateShipMaxComRange(positiveAttribute);
                DisplayTerminalInfo();
                break;
            case TerminalTypes.JobSelection:
                DisplayJobSelection();
                break;
            case TerminalTypes.TerminalStore:
                DisplayTerminalSelection();
                break;
            default:
                break;
        }

        failureChanceInterval -= Time.deltaTime;
        if (failureChanceInterval < 0) {
            if (Random.Range(0.0f,1.0f) < currentFailureRate) {
                terminalFailure = true;
            }
            failureChanceInterval = 30;
        }

        //sends power consumption value to the Ship script.
        ship.UpdateShipPowerConsumption(powerConsumption);

        //sends heat generation value to the Ship script.
        ship.UpdateTotalShipHeatGeneration(heatGeneration);

        //Reset current positiveAttribute and efficiency values, so they does not continue to increase every frame.
        positiveAttribute = basePositiveAttribute;
        efficiencyBonus = 1;
    }

    public void IncreaseEfficiency(float value) {
        efficiencyBonus += value - 1;
    }

    public void DisplayJobSelection() {
        textOutput = "Available Jobs\n\n" +
                        "Job 1: " + jobsManager.jobList[0].jobName + "\n" +
                        "Deliver " + jobsManager.jobList[0].cargoName + " to " + jobsManager.jobList[0].destination +
                        " within " + jobsManager.jobList[0].targetDeliveryTime + " days to collect " + jobsManager.jobList[0].cargoValue + " copper coins per cubic meter of cargo." +
                        "\n\n" +
                        "Job 2: " + jobsManager.jobList[1].jobName + "\n" +
                        "Deliver " + jobsManager.jobList[1].cargoName + " to " + jobsManager.jobList[1].destination +
                        " within " + jobsManager.jobList[1].targetDeliveryTime + " days to collect " + jobsManager.jobList[1].cargoValue + " copper coins per cubic meter of cargo." +
                        "\n\n" +
                        "Job 3: " + jobsManager.jobList[2].jobName + "\n" +
                        "Deliver " + jobsManager.jobList[2].cargoName + " to " + jobsManager.jobList[2].destination +
                        " within " + jobsManager.jobList[2].targetDeliveryTime + " days to collect " + jobsManager.jobList[2].cargoValue + " copper coins per cubic meter of cargo." +
                        "\n\n" +
                        "(Press job number to accept a job)";

        monitor.ChangeFontSize(28);
        monitor.ChangeAlignmentToMiddleLeft();
        monitor.WriteToMonitor(textOutput);
    }

    public void DisplayTerminalSelection() {
        textOutput = "Available Terminals\n\n" + 
                        "Terminal 1: " + terminalStore.terminalList[0].terminalType + "(Tier " + terminalStore.terminalList[0].terminalTier + ")" +
                        "\n\n" +
                        "Terminal 2: " + terminalStore.terminalList[1].terminalType + "(Tier " + terminalStore.terminalList[1].terminalTier + ")" +
                        "\n\n" +
                        "Terminal 3: " + terminalStore.terminalList[2].terminalType + "(Tier " + terminalStore.terminalList[2].terminalTier + ")";

        monitor.ChangeFontSize(28);
        monitor.ChangeAlignmentToMiddleLeft();
        monitor.WriteToMonitor(textOutput);
    }

    //Sets text output and sends it to the terminal's monitor.
    public void DisplayTerminalInfo() {
        textOutput = label + "\n" + positiveAttributeLabel + positiveAttribute + positiveAttributeUnit +
                        "\nTerminal Failure: " + terminalFailure;
        monitor.WriteToMonitor(textOutput);
    }
}
