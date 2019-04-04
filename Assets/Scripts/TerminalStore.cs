using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalStore : MonoBehaviour {

    int numberOfTerminals = 3;
    public List<Terminal> terminalList;

    List<string> terminalTypes;
    List<int> terminalTiers;

    public class Terminal {

        public string terminalType;
        public int terminalTier;

        public Terminal(string type, int tier) {
            terminalType = type;
            terminalTier = tier;
        }

    }

    // Start is called before the first frame update
    void Start() {
        terminalList = new List<Terminal>();

        terminalTypes = new List<string>() {
            "PowerGenerator", "CoolingUnit", "EngineControl", "NAVCOMComputer", "JobSelection", "TerminalStore"
        };

        terminalTiers = new List<int>() {1, 2, 3};

        GenerateAvailableTerminals();
    }

    // Update is called once per frame
    void Update() {

    }

    public void GenerateAvailableTerminals() {

        terminalList.Clear();

        for (int i = 0; i < numberOfTerminals; i++) {
                terminalList.Add(new Terminal(
                                    terminalTypes[Random.Range(0, terminalTypes.Count)],
                                    terminalTiers[Random.Range(0, terminalTiers.Count)]));
        }

    }

}
