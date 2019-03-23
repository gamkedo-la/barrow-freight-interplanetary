using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonLightScript : MonoBehaviour
{
    public Vector3 toggleButtonLightPosition;
    // Start is called before the first frame update
    void Start()
    {
        toggleButtonLightPosition = transform.position;
        Debug.Log(toggleButtonLightPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
