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
    public AudioSource terminalStartupAndRunningSound;
    public float loopStart, loopEnd;

    private float beepDelay;
    private int IndexForBeepArray;
    private float beepDelayMin;
    private float beepDelayMax;
    private float beepPan;

    

    // Start is called before the first frame update
    void Start()
    {

        terminalBeepsArray = GetComponents<AudioSource>();
        
        A440 = terminalBeepsArray[0];
        B494 = terminalBeepsArray[1];
        CSharp554 = terminalBeepsArray[2];
        DSharp622 = terminalBeepsArray[3];
        F699 = terminalBeepsArray[4];
        GSharp415 = terminalBeepsArray[5];
        G392 = terminalBeepsArray[6];
        terminalStartupAndRunningSound = terminalBeepsArray[7];
        loopStart = 3.0f;
        loopEnd = 3.9f;

        beepDelayMin = 0f;
        beepDelayMax = 0.3f;
        beepPan = 1.0f;
    }

    private void calculateBeepDelay()
    {
        beepDelay = Random.Range(beepDelayMin, beepDelayMax);
    }

    public void playInitialBeep()
    {
        IndexForBeepArray = Random.Range(0, 6);
        terminalBeepsArray[IndexForBeepArray].volume = Random.Range(0.7f,1.0f);
        terminalBeepsArray[IndexForBeepArray].pitch = Random.Range(0.88f,1.12f);
        Debug.Log(terminalBeepsArray[IndexForBeepArray].panStereo);
        terminalBeepsArray[IndexForBeepArray].Play();
    }

    public void playDelayedBeeps()
    {
        for (int i = 0; i < 5; i++)
        {
            calculateBeepDelay();
            IndexForBeepArray = Random.Range(0, 6);
            terminalBeepsArray[IndexForBeepArray].volume = Random.Range(0.7f, 1.0f);
            terminalBeepsArray[IndexForBeepArray].pitch = Random.Range(0.88f, 1.12f);

            if (beepPan == 1.0f)
            {
                beepPan = -1.0f;
            } else
            {
                beepPan = 1.0f;
            }

            terminalBeepsArray[IndexForBeepArray].panStereo = beepPan;

            terminalBeepsArray[IndexForBeepArray].PlayDelayed(beepDelay);
            Debug.Log(terminalBeepsArray[IndexForBeepArray].panStereo);
            beepDelayMin += 0.1f;
            beepDelayMax+= 0.1f;


            //Debug.Log(beepDelayMin);
            //Debug.Log(beepDelayMax);
            
        }
        beepDelayMin = 0f;
        beepDelayMax = 0.3f;
    }

    public void playTerminalStartupAndRunningSound()
    {
        terminalStartupAndRunningSound.Play();
    }

    public void resetTerminalStartupAndRunningSound()
    {
        terminalStartupAndRunningSound.Pause();
        terminalStartupAndRunningSound.time = 0f;

    }
    // Update is called once per frame
    void Update()
    {
        if (terminalStartupAndRunningSound.isPlaying && terminalStartupAndRunningSound.time > loopEnd)
        {
            terminalStartupAndRunningSound.time = loopStart;
        }
    }
}
