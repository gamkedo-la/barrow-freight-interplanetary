using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalStore : MonoBehaviour {

    Ship ship;

    int numberOfTerminals = 3;
    public List<Terminal> terminalList;

    public List<Terminal> purchasedTerminals;
    public Terminal activePurchasedTerminal;

    List<string> terminalTypes;
    List<int> terminalTiers;

    public class Terminal {

        public string terminalType;
        public int terminalTier;
        public int terminalCost;

        public Terminal(string type, int tier, int cost) {
            terminalType = type;
            terminalTier = tier;
            terminalCost = cost;
        }

    }

    // Start is called before the first frame update
    void Start() {
        terminalList = new List<Terminal>();
        purchasedTerminals = new List<Terminal>();
        ship = GameObject.Find("Ship").GetComponent<Ship>();

        terminalTypes = new List<string>() {
            "PowerGenerator", "CoolingUnit", "EngineControl", "NAVCOMComputer", "JobSelection", "TerminalStore"
        };

        terminalTiers = new List<int>() {1, 2, 3};

        GenerateAvailableTerminals();
    }

    // Update is called once per frame
    void Update() {

        if (terminalList.Count == numberOfTerminals) {

            if (Input.GetKeyUp(KeyCode.Alpha1)) {
                purchasedTerminals.Add(terminalList[0]);
                ship.currency -= terminalList[0].terminalCost;
                activePurchasedTerminal = purchasedTerminals[purchasedTerminals.Count - 1];
            }

            if (Input.GetKeyUp(KeyCode.Alpha2)) {
                purchasedTerminals.Add(terminalList[1]);
                ship.currency -= terminalList[1].terminalCost;
                activePurchasedTerminal = purchasedTerminals[purchasedTerminals.Count - 1];
            }

            if (Input.GetKeyUp(KeyCode.Alpha3)) {
                purchasedTerminals.Add(terminalList[2]);
                ship.currency -= terminalList[2].terminalCost;
                activePurchasedTerminal = purchasedTerminals[purchasedTerminals.Count - 1];
            }
        }

    }

    public void GenerateAvailableTerminals() {

        terminalList.Clear();

        for (int i = 0; i < numberOfTerminals; i++) {
            terminalList.Add(new Terminal(
                                terminalTypes[Random.Range(0, terminalTypes.Count)],
                                terminalTiers[Random.Range(0, terminalTiers.Count)],
                                Random.Range(100,500))); //terminal cost
        }

    }

}
