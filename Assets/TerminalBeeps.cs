using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBeeps : MonoBehaviour
{

    public AudioSource[] terminalBeepsArray;
    public AudioSource A440;
    public AudioSource B494;
    public AudioSource CSharp554;
    public AudioSource DSharp622;
    public AudioSource F699;
    public AudioSource GSharp415;
    public AudioSource G392;

    private float beepDelay;
    private int IndexForBeepArray;
    private float beepDelayMin;
    private float beepDelayMax;

    // Start is called before the first frame update
    void Start()
    {

        terminalBeepsArray = GetComponents<AudioSource>();
        //Debug.Log(buttonSFX);
        A440 = terminalBeepsArray[0];
        B494 = terminalBeepsArray[1];
        CSharp554 = terminalBeepsArray[2];
        DSharp622 = terminalBeepsArray[3];
        F699 = terminalBeepsArray[4];
        GSharp415 = terminalBeepsArray[5];
        G392 = terminalBeepsArray[6];

        beepDelayMin = 0f;
        beepDelayMax = 0.2f;
    }

    private void calculateBeepDelay()
    {
        beepDelay = Random.Range(beepDelayMin, beepDelayMax);
    }

    public void playInitialBeep()
    {
        IndexForBeepArray = Random.Range(0, 6);
        terminalBeepsArray[IndexForBeepArray].Play();
    }

    public void playDelayedBeeps()
    {
        for (int i = 0; i < 5; i++)
        {
            calculateBeepDelay();
            IndexForBeepArray = Random.Range(0, 6);
            terminalBeepsArray[IndexForBeepArray].PlayDelayed(beepDelay);
            beepDelayMin+= 0.1f;
            beepDelayMax+= 0.1f;
        }
        beepDelayMin = 0f;
        beepDelayMax = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
