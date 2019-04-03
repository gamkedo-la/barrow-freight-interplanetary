using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Jobs : MonoBehaviour {
    int numberOfJobs = 3;
    public List<Job> jobList;

    List<int> jobID; 
    List<string> jobNames;
    List<string> destinations;
    List<string> cargoNames;
    List<string> cargoTypes;
    List<float> cargoValues;
    List<float> targetDeliveryTimes;
    List<int> jobTiers;
    List<bool> alreadyListed;

    public class Job {

        public int jobID;
        public string jobName;
        public string destination;
        public string cargoName;
        public string cargoType;
        public float cargoValue;  //per cubic meter
        public float targetDeliveryTime;
        public int jobTier;

        public Job(int id, string name, string dest, string cargo, string type, float value, float time, int tier) {
            jobID = id;
            jobName = name;
            destination = dest;
            cargoName = cargo;
            cargoType = type;
            cargoValue = value;
            targetDeliveryTime = time;
            jobTier = tier;
        }

    }

    // Start is called before the first frame update
    void Start() {
        jobList = new List<Job>();

        jobID = new List<int>();
        jobNames = new List<string>();
        destinations = new List<string>();
        cargoNames = new List<string>();
        cargoTypes = new List<string>();
        cargoValues = new List<float>();
        targetDeliveryTimes = new List<float>();
        jobTiers = new List<int>();
        alreadyListed = new List<bool>();

        GenerateJobPool();
        GenerateAvailableJobs();
    }

    // Update is called once per frame
    void Update() {

    }

    public void GenerateAvailableJobs() {

        jobList.Clear();
        for (int i = 0; i < alreadyListed.Count; i++) {
            alreadyListed[i] = false;
        }

        for (int i = 0; i < numberOfJobs;) {
            int rand = Random.Range(0, jobID.Count - 1);
            if (!alreadyListed[rand]) {
                jobList.Add(new Job(jobID[rand],
                                    jobNames[rand],
                                    destinations[rand],
                                    cargoNames[rand],
                                    cargoTypes[rand],
                                    cargoValues[rand],
                                    targetDeliveryTimes[rand],
                                    jobTiers[rand]));

                alreadyListed[rand] = true;
                i++;
            }
        }
        Debug.Log(jobList[0].jobName);
    } 

    void GenerateJobPool() {

        int i = 0;

        jobID.Add(i);
        jobNames.Add("Sauce Run");
        destinations.Add("Neptune");
        cargoNames.Add("Dusty \"Bubs\" DeKat\'s Ol\' Fashsioned Bubby-Que Sauce");
        cargoTypes.Add("Food");
        cargoValues.Add(101f);
        targetDeliveryTimes.Add(1001);
        jobTiers.Add(1);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Gimme Odda Pineapples");
        destinations.Add("Odda, Norway, Earth");
        cargoNames.Add("Farmer Helge's Sour Cream & Pineapple Spaghetti Spread");
        cargoTypes.Add("Food");
        cargoValues.Add(102f);
        targetDeliveryTimes.Add(1002);
        jobTiers.Add(2);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Best of Both Worlds");
        destinations.Add("Saturn");
        cargoNames.Add("Pizza Cakes");
        cargoTypes.Add("Food");
        cargoValues.Add(103f);
        targetDeliveryTimes.Add(1003);
        jobTiers.Add(3);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Out of the Bag");
        destinations.Add("Venus");
        cargoNames.Add("Hand Painted Messenger Bags");
        cargoTypes.Add("Luxary Item");
        cargoValues.Add(104f);
        targetDeliveryTimes.Add(1004);
        jobTiers.Add(4);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("We\'ll Get Right On That");
        destinations.Add("The Center of the Sun");
        cargoNames.Add("Barrow Freight Health & Safety Grievences");
        cargoTypes.Add("Mail");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Momo\'s Nom Noms");
        destinations.Add("Mercury");
        cargoNames.Add("Momo\'s Opaque Nutrition Pellets");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Hang In There");
        destinations.Add("Moon Base");
        cargoNames.Add("Motivational Posters");
        cargoTypes.Add("Luxaries");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Roll the Bones");
        destinations.Add("Earth");
        cargoNames.Add("Dinosaur Bones");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("A Slow Connection");
        destinations.Add("Neptune");
        cargoNames.Add("Software Updates");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Plenty of Space");
        destinations.Add("Mars");
        cargoNames.Add("Miniature Space Ships");
        cargoTypes.Add("Machinery");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;
    }
}
