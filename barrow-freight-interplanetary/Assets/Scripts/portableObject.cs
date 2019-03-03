using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portableObject : MonoBehaviour
{
    public bool isHeld = false;
    public bool isInstalled = false;
    public int efficiencyBonus = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInstalled) {
            Terminal parentTerminal = GetComponentInParent<Terminal>();
            parentTerminal.IncreaseEfficiency(efficiencyBonus);
        }
    }

    public void PickupObject() {
        isHeld = true;
        isInstalled = false;
        gameObject.layer = 2;
    }

    public void DropObject() {
        isHeld = false;
        isInstalled = false;
        gameObject.layer = 0;
    }

    public void InstallObject() {
        isHeld = false;
        isInstalled = true;
        gameObject.layer = 0;
    }
}
