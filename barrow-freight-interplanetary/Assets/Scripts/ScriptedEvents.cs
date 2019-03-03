using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvents : MonoBehaviour {

    public GameObject[] goList;
    public bool setActiveOnTrigger;

    void OnTriggerEnter(Collider other) {
        foreach (GameObject go in goList) {
            go.SetActive(setActiveOnTrigger);
        }
    }
}
