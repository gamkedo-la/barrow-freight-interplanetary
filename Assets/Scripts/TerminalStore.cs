using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalStore : MonoBehaviour {

    int numberOfTerminals = 3;
    public List<Terminal> terminalList;

    List<int> terminalID;
    List<string> terminalNames;
    List<string> destinations;
    List<string> cargoNames;
    List<string> cargoTypes;
    List<float> cargoValues;
    List<float> targetDeliveryTimes;
    List<int> terminalTiers;
    List<bool> alreadyListed;

    public class Terminal {

        public int terminalID;
        public string terminalName;
        public string destination;
        public string cargoName;
        public string cargoType;
        public float cargoValue;  //per cubic meter
        public float targetDeliveryTime;
        public int terminalTier;

        public Terminal(int id, string name, string dest, string cargo, string type, float value, float time, int tier) {
            terminalID = id;
            terminalName = name;
            destination = dest;
            cargoName = cargo;
            cargoType = type;
            cargoValue = value;
            targetDeliveryTime = time;
            terminalTier = tier;
        }

    }

    // Start is called before the first frame update
    void Start() {
        terminalList = new List<Terminal>();

        terminalID = new List<int>();
        terminalNames = new List<string>();
        destinations = new List<string>();
        cargoNames = new List<string>();
        cargoTypes = new List<string>();
        cargoValues = new List<float>();
        targetDeliveryTimes = new List<float>();
        terminalTiers = new List<int>();
        alreadyListed = new List<bool>();

        GenerateTerminalPool();
        GenerateAvailableTerminals();
    }


    // Update is called once per frame
    void Update() {

    }

    public void GenerateAvailableTerminals() {

        terminalList.Clear();
        for (int i = 0; i < alreadyListed.Count; i++) {
            alreadyListed[i] = false;
        }

        for (int i = 0; i < numberOfTerminals;) {
            int rand = Random.Range(0, terminalID.Count - 1);
            if (!alreadyListed[rand]) {
                terminalList.Add(new Terminal(terminalID[rand],
                                    terminalNames[rand],
                                    destinations[rand],
                                    cargoNames[rand],
                                    cargoTypes[rand],
                                    cargoValues[rand],
                                    targetDeliveryTimes[rand],
                                    terminalTiers[rand]));

                alreadyListed[rand] = true;
                i++;
            }
        }
        Debug.Log(terminalList[0].terminalName);
    }

    void GenerateTerminalPool() {

        int i = 0;

        terminalID.Add(i);
        terminalNames.Add("Sauce Run");
        destinations.Add("Neptune");
        cargoNames.Add("Dusty \"Bubs\" DeKat\'s Ol\' Fashsioned Bubby-Que Sauce");
        cargoTypes.Add("Food");
        cargoValues.Add(101f);
        targetDeliveryTimes.Add(1001);
        terminalTiers.Add(1);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Gimme Odda Pineapples");
        destinations.Add("Odda, Norway, Earth");
        cargoNames.Add("Farmer Helge's Sour Cream & Pineapple Spaghetti Spread");
        cargoTypes.Add("Food");
        cargoValues.Add(102f);
        targetDeliveryTimes.Add(1002);
        terminalTiers.Add(2);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Best of Both Worlds");
        destinations.Add("Saturn");
        cargoNames.Add("Pizza Cakes");
        cargoTypes.Add("Food");
        cargoValues.Add(103f);
        targetDeliveryTimes.Add(1003);
        terminalTiers.Add(3);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Out of the Bag");
        destinations.Add("Venus");
        cargoNames.Add("Hand Painted Messenger Bags");
        cargoTypes.Add("Luxary Item");
        cargoValues.Add(104f);
        targetDeliveryTimes.Add(1004);
        terminalTiers.Add(4);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("We\'ll Get Right On That");
        destinations.Add("The Center of the Sun");
        cargoNames.Add("Barrow Freight Health & Safety Grievences");
        cargoTypes.Add("Mail");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        terminalTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Momo\'s Nom Noms");
        destinations.Add("Mercury");
        cargoNames.Add("Momo\'s Opaque Nutrition Pellets");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        terminalTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Hang In There");
        destinations.Add("Moon Base");
        cargoNames.Add("Motivational Posters");
        cargoTypes.Add("Luxaries");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        terminalTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Roll the Bones");
        destinations.Add("Earth");
        cargoNames.Add("Dinosaur Bones");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        terminalTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("A Slow Connection");
        destinations.Add("Neptune");
        cargoNames.Add("Software Updates");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        terminalTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        terminalID.Add(i);
        terminalNames.Add("Plenty of Space");
        destinations.Add("Mars");
        cargoNames.Add("Miniature Space Ships");
        cargoTypes.Add("Machinery");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        terminalTiers.Add(5);
        alreadyListed.Add(false);
        i++;
    }

}
