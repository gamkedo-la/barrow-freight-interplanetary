using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardButtonLightScript : MonoBehaviour
{
    public Vector3 standardButtonLightPosition;
    // Start is called before the first frame update
    void Start()
    {
        standardButtonLightPosition = transform.position;
        Debug.Log(standardButtonLightPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
