﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Jobs : MonoBehaviour {

    Ship ship;
    TerminalMonitor etaClock;

    public int numberOfJobs = 3;
    public List<Job> jobList;
    public Job activeJob;
    public int jobsCompleted = 0;

    public bool navcomFailure = false;

    public float currentDist = 0; //in light-seconds
    public float eta = 0; //in seconds
    public bool isInStasis = false;

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
        public float distanceToDestination; //in light-seconds
        public string cargoName;
        public string cargoType;
        public float cargoValue;  //per cubic meter
        public float targetDeliveryTime;
        public int jobTier;
        public bool jobComplete;

        public Job(int id, string name, string dest, float dist, string cargo, string type, float value, float time, int tier) {
            jobID = id;
            jobName = name;
            destination = dest;
            distanceToDestination = dist; //in light-seconds
            cargoName = cargo;
            cargoType = type;
            cargoValue = value;
            targetDeliveryTime = time; // in seconds
            jobTier = tier;
            jobComplete = false;
        }

    }

    // Start is called before the first frame update
    void Start() {
        jobList = new List<Job>();
        etaClock = GameObject.Find("ETA Clock").GetComponentInChildren<TerminalMonitor>();
        ship = GameObject.Find("Ship").GetComponent<Ship>();

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

        if (navcomFailure) {
            numberOfJobs = 1;
        } else {
            numberOfJobs = 3;
        }

        if (jobList.Count == numberOfJobs) {

            if (Input.GetKeyUp(KeyCode.Alpha1)) {
                activeJob = jobList[0];
                currentDist = activeJob.distanceToDestination;
            }

            if (Input.GetKeyUp(KeyCode.Alpha2)) {
                activeJob = jobList[1];
                currentDist = activeJob.distanceToDestination;
            }

            if (Input.GetKeyUp(KeyCode.Alpha3)) {
                activeJob = jobList[2];
                currentDist = activeJob.distanceToDestination;
            }
        }

        UpdateETAClock();
    }

    public void GenerateAvailableJobs() {

        jobList.Clear();
        for (int i = 0; i < alreadyListed.Count; i++) {
            alreadyListed[i] = false;
        }

        for (int i = 0; i < numberOfJobs;) {
            int rand = UnityEngine.Random.Range(0, jobID.Count - 1);
            if (!alreadyListed[rand]) {
                jobList.Add(new Job(jobID[rand],
                                    jobNames[rand],
                                    destinations[rand],
                                    UnityEngine.Random.Range(1, 3000), //random distance in light-seconds
                                    cargoNames[rand],
                                    cargoTypes[rand],
                                    cargoValues[rand],
                                    targetDeliveryTimes[rand],
                                    jobTiers[rand]));

                alreadyListed[rand] = true;
                i++;
            }
        }
        //Debug.Log(jobList[0].jobName);
    }

    void GenerateJobPool() {

        int i = 0;

        jobID.Add(i);
        jobNames.Add("Sauce Run");
        destinations.Add("Neptune");
        cargoNames.Add("Dusty \"Bubs\" DeKat\'s Ol\' Fashsioned Bubby-Que Sauce");
        cargoTypes.Add("Food");
        cargoValues.Add(101f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(1);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Gimme Odda Pineapples");
        destinations.Add("Odda, Norway, Earth");
        cargoNames.Add("Farmer Helge's Sour Cream & Pineapple Spaghetti Spread");
        cargoTypes.Add("Food");
        cargoValues.Add(102f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(2);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Best of Both Worlds");
        destinations.Add("Saturn");
        cargoNames.Add("Pizza Cakes");
        cargoTypes.Add("Food");
        cargoValues.Add(103f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(3);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Out of the Bag");
        destinations.Add("Venus");
        cargoNames.Add("Hand Painted Messenger Bags");
        cargoTypes.Add("Luxary Item");
        cargoValues.Add(104f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(4);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("We\'ll Get Right On That");
        destinations.Add("The Center of the Sun");
        cargoNames.Add("Barrow Freight Health & Safety Grievences");
        cargoTypes.Add("Mail");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Momo\'s Nom Noms");
        destinations.Add("Mercury");
        cargoNames.Add("Momo\'s Opaque Nutrition Pellets");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Hang In There");
        destinations.Add("Moon Base");
        cargoNames.Add("Motivational Posters");
        cargoTypes.Add("Luxaries");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Roll the Bones");
        destinations.Add("Earth");
        cargoNames.Add("Dinosaur Bones");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("A Slow Connection");
        destinations.Add("Neptune");
        cargoNames.Add("Software Updates");
        cargoTypes.Add("Cat Food");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;

        jobID.Add(i);
        jobNames.Add("Plenty of Space");
        destinations.Add("Mars");
        cargoNames.Add("Miniature Space Ships");
        cargoTypes.Add("Machinery");
        cargoValues.Add(105f);
        targetDeliveryTimes.Add(101001);
        jobTiers.Add(5);
        alreadyListed.Add(false);
        i++;
    }

    public void UpdateNAVCOMStatus(bool failed) {
        if (failed) {
            navcomFailure = true;
        }
    }

    public void ToggleStasis()
    {
        isInStasis = !isInStasis;
    }

    public void UpdateETAClock() {

        string timeText;
        float stasisTimeMultiplier;

        if (activeJob != null) {

            if (isInStasis) {
                stasisTimeMultiplier = 1000;
            } else {
                stasisTimeMultiplier = 1;
            }
            currentDist = currentDist/*LS*/ - (ship.previousFrameShipSpeed/*LSPS*/ * (Time.deltaTime/*S*/ * stasisTimeMultiplier));
            eta/*S*/ = currentDist/*LS*/ / ship.previousFrameShipSpeed/*LSPS*/;

            TimeSpan timeSpan = TimeSpan.FromSeconds(eta);
            timeText = string.Format("{0:D3}:{1:D2}:{2:D2}:{3:D2}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            if (eta < 0)
            {
                activeJob.jobComplete = true;
                ship.UpdateCurrency(activeJob.cargoValue);
                timeText = "Job Complete";
                jobsCompleted++;
                activeJob = null;
                isInStasis = false;
            }

        } else {
            timeText = "No Destination Selected";
        }

        etaClock.WriteToMonitor(timeText);
    }
}
