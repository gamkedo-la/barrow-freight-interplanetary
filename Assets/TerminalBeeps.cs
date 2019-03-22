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
        G392 = terminalBeepsArray[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
