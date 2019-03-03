using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int efficiency = 0;
    public int baseEfficiency = 0;

    public string textOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textOutput = "Ship Efficiency at " + efficiency + "%";
        MainlMonitor monitor = GetComponentInChildren<MainMonitor>();
        monitor.WriteToMonitor(textOutput);
        efficiency = baseEfficiency;
    }

    public void IncreaseEfficiency(int value) {
        efficiency += value;
    }
}
