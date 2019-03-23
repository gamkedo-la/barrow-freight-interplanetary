using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButtonLightScript : MonoBehaviour
{
    public Vector3 holdButtonLightPosition;
    // Start is called before the first frame update
    void Start()
    {
        holdButtonLightPosition = transform.position;
        Debug.Log(holdButtonLightPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
