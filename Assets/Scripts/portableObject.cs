using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portableObject : MonoBehaviour
{
    public bool isHeld = false;
    public bool isInstalled = false;
    public bool isActivated = false;
    public float efficiencyBonus;
    //public bool isTool = false;
    public Vector3 initialPosition;
    public Vector3 activatedPosition;
    public GameObject[] indicators;
    public GameObject terminalPrefab;

    private AudioSource audioData;

    public enum objectTypes { Module, PowerCell, FireExtinguisher, TerminalPlacer};
    public objectTypes objectType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If this module is installed in a terminal, it sends its stat values to the terminal
        if (isInstalled) {
            Terminal parentTerminal = GetComponentInParent<Terminal>();
            parentTerminal.IncreaseEfficiency(efficiencyBonus);
        }

        
    }

    public void PickupObject() {
        isHeld = true;
        isInstalled = false;
        gameObject.layer = 2;  //prevents held objects from blocking raycasts.

        if (objectType == objectTypes.TerminalPlacer)
        {
            indicators = GameObject.FindGameObjectsWithTag("TerminalPlacementIndicator");
            foreach (GameObject indicator in indicators)
            {
                MeshRenderer highlight = indicator.GetComponent<MeshRenderer>();
                highlight.enabled = true;

                BoxCollider collider = indicator.GetComponent<BoxCollider>();
                collider.enabled = true;
            }
        }

        audioData = GetComponent<AudioSource>();
        if (audioData != null)
        {
            audioData.Play();
        }
        else
        {
            Debug.Log("No SFX found");
        }

    }

    public void DropObject() {
        isHeld = false;
        isInstalled = false;
        gameObject.layer = 0;

        if (objectType == objectTypes.TerminalPlacer)
        {
            indicators = GameObject.FindGameObjectsWithTag("TerminalPlacementIndicator");
            foreach (GameObject indicator in indicators)
            {
                MeshRenderer highlight = indicator.GetComponent<MeshRenderer>();
                highlight.enabled = false;

                BoxCollider collider = indicator.GetComponent<BoxCollider>();
                collider.enabled = false;
            }
        }

    }

    public void InstallObject() {
        isHeld = false;
        isInstalled = true;
        gameObject.layer = 0;
        initialPosition = transform.position;
    }

    public void ActivateObject() {
        activatedPosition = initialPosition + new Vector3(0,0,-10);
        transform.Translate(new Vector3(-0.7f,0,0));
    }

    public void DeactivateObject() {
        activatedPosition = initialPosition + new Vector3(0, 0, 10);
        transform.Translate(new Vector3(0.7f, 0, 0));
    }

    public bool IsObjectInstalled() {
        return isInstalled;
    }

    public bool IsObjectActivated() {
        return isActivated;
    }

    public void PlaceTerminal(Vector3 pos) {
        Instantiate(terminalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
