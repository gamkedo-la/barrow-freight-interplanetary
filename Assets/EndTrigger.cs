using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{

    public BlackScreen blackScreen;
    public Text endScreen;

    void Start()
    {
        blackScreen = GameObject.Find("Black Screen").GetComponent<BlackScreen>();
        endScreen = GameObject.Find("End Screen").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {

        blackScreen.SetAlpha(1);
        endScreen.enabled = true;

    }
}
