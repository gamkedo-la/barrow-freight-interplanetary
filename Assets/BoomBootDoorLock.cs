using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBootDoorLock : MonoBehaviour
{
    Door door;
    Jobs jobManager;
    public TerminalBay powerBay;

    void Start() {
        door = GameObject.Find("Door (3)").GetComponent<Door>();
        jobManager = GameObject.Find("Game Managers").GetComponent<Jobs>();
        powerBay = GameObject.Find("Power Bay").GetComponent<TerminalBay>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && jobManager.jobsCompleted == 1 && !powerBay.IsModuleInstalled())
        {
            door.LockDoor();
        }
    }

    //public void A()
    //{
    //    this.SetActive(true);
    //}
//
    //public void SetInactive()
    //{
    //    this.SetActive(false);
    //}
}
