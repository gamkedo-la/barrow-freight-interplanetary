using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisCapsule : MonoBehaviour
{

    private Jobs jobsManager;

    // Start is called before the first frame update
    void Start()
    {
        jobsManager = GameObject.Find("Game Managers").GetComponent<Jobs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterStatis()
    {
        jobsManager.ToggleStasis();
    }
}
