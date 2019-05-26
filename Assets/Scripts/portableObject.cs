using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portableObject : MonoBehaviour
{
    public bool isHeld = false;
    public bool isInstalled = false;
    public bool isActivated = false;
    public float efficiencyBonus;
    public Vector3 initialPosition;
    public Vector3 activatedPosition;
    public GameObject[] firecolliders;
    public GameObject terminalPrefab;
    public GameObject foam;
    public bool isSpraying = true;
    private ParticleSystem ps;

    private AudioSource audioData;

    public enum objectTypes { Module, PowerCell, FireExtinguisher, TerminalPlacer};
    public objectTypes objectType;

    // Start is called before the first frame update
    void Start()
    {
        if (objectType == objectTypes.FireExtinguisher)
        {
            foam = GameObject.Find("Foam Particle System");
            ps = foam.GetComponent<ParticleSystem>();
            var emission = ps.emission;
            emission.enabled = isSpraying;
        }
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

        if (objectType == objectTypes.FireExtinguisher)
        {
            firecolliders = GameObject.FindGameObjectsWithTag("FireCollider");
            foreach (GameObject firecollider in firecolliders)
            {
                //MeshRenderer highlight = indicator.GetComponent<MeshRenderer>();
                //highlight.enabled = true;

                BoxCollider collider = firecollider.GetComponent<BoxCollider>();
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

        if (objectType == objectTypes.FireExtinguisher)
        {
            firecolliders = GameObject.FindGameObjectsWithTag("FireCollider");
            foreach (GameObject firecollider in firecolliders)
            {
                //MeshRenderer highlight = indicator.GetComponent<MeshRenderer>();
                //highlight.enabled = false;

                BoxCollider collider = firecollider.GetComponent<BoxCollider>();
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

    public bool IsFireExtinguisher()
    {
        if(objectType == objectTypes.FireExtinguisher)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void ToggleIsSpraying()
    {
        isSpraying = !isSpraying;
        var emission = ps.emission;
        emission.enabled = isSpraying;
    }

}
