using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jobs : MonoBehaviour
{
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


    public class Job {

        public string jobName;
        public string destination;
        public string cargoName;
        public string cargoType;
        public float cargoValue;  //per cubic meter
        public float targetDeliveryTime;
        public int jobTier;

        public Job(string name, string dest, string cargo, string type, float value, float time, int tier) {
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
    void Start()
    {
        jobList = new List<Job>();

        jobID = new List<int>();
        jobNames = new List<string>();
        destinations = new List<string>();
        cargoNames = new List<string>();
        cargoTypes = new List<string>();
        cargoValues = new List<float>();
        targetDeliveryTimes = new List<float>();
        jobTiers = new List<int>();

        GenerateJobPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateAvailableJobs() 
    {
        for(int i = 0;i < numberOfJobs; i++) {
            int rand = Random.Range(0, jobID.Count - 1);
            jobList.Add(new Job(jobNames[rand],
                                    destinations[rand],
                                    cargoNames[rand],
                                    cargoTypes[rand],
                                    cargoValues[rand],
                                    targetDeliveryTimes[rand],
                                    jobTiers[rand]));
        }
        Debug.Log(jobList[0].jobName);
    }

    void GenerateJobPool() {

        int i = 0;

        jobID.Add(i);
        jobNames.Add("Job Name A");
        destinations.Add("Destination A");
        cargoNames.Add("Cargo Name A");
        cargoTypes.Add("Cargo Type A");
        cargoValues.Add(101f);
        targetDeliveryTimes.Add(1001);
        jobTiers.Add(1);
        i++;

        jobID.Add(i);
        jobNames.Add("Job Name B");
        destinations.Add("Destination B");
        cargoNames.Add("Cargo Name B");
        cargoTypes.Add("Cargo Type B");
        cargoValues.Add(102f);
        targetDeliveryTimes.Add(1002);
        jobTiers.Add(2);
        i++;

        jobID.Add(i);
        jobNames.Add("Job Name C");
        destinations.Add("Destination C");
        cargoNames.Add("Cargo Name C");
        cargoTypes.Add("Cargo Type C");
        cargoValues.Add(103f);
        targetDeliveryTimes.Add(1003);
        jobTiers.Add(3);
        i++;

        jobID.Add(i);
        jobNames.Add("Job Name D");
        destinations.Add("Destination D");
        cargoNames.Add("Cargo Name D");
        cargoTypes.Add("Cargo Type D");
        cargoValues.Add(104f);
        targetDeliveryTimes.Add(1004);
        jobTiers.Add(4);
        i++;

        jobID.Add(i);
        jobNames.Add("Job Name E");
        destinations.Add("Destination E");
        cargoNames.Add("Cargo Name E");
        cargoTypes.Add("Cargo Type E");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(1005);
        jobTiers.Add(5);
        i++;
    }
}
