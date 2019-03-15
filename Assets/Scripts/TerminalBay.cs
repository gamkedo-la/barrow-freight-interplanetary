using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBay : MonoBehaviour
{

    private bool isModuleInstalled = false;
    private GameObject installedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachObjectToBay(GameObject go) {
        installedObject = go;
        isModuleInstalled = true;
    }

    public void DetachObjectToBay(GameObject go) {
        installedObject = null;
        isModuleInstalled = false;
    }

    public bool IsModuleInstalled() {
        return isModuleInstalled;
    }

    public GameObject GetInstalledObject() {
        return installedObject;
    }

}
