using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{

    public string textOutput;

    public int basePowerOutput = 0;
    public int powerOutput = 0;

    public enum TerminalTypes { PowerGenerator, CoolingUnit, EngineControl, NAVCOMComputer };
    public TerminalTypes terminalType;


    // Start is called before the first frame update
    void Start() {

        Renderer rend = GetComponent<Renderer>();

        switch (terminalType) {
            case TerminalTypes.PowerGenerator:
                rend.material.color = Color.yellow;
                break;
            case TerminalTypes.CoolingUnit:
                rend.material.color = Color.blue;
                break;
            case TerminalTypes.EngineControl:
                rend.material.color = Color.red;
                break;
            case TerminalTypes.NAVCOMComputer:
                rend.material.color = Color.green;
                break;
            default:
            break;
        }
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
