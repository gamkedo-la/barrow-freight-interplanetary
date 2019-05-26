using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool startMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startpos = this.transform.position;
        Vector3 endpos = startpos += (Vector3.forward + Vector3.up) * Time.deltaTime;
        this.transform.position = endpos;
    }

    public void StartMovement()
    {
        startMoving = true;
    }
}
