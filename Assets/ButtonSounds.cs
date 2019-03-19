using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{

    public AudioSource[] buttonSFX;
    public AudioSource ButtonPressSound;
    public AudioSource ButtonPressFirstHalf;
    public AudioSource ButtonPressSecondHalf;
    public AudioSource LightStartingSound;
    public AudioSource LightLoopingSound;
    

    // Start is called before the first frame update
    void Start()
    {
        buttonSFX = GetComponents<AudioSource>();
        Debug.Log(buttonSFX);
        ButtonPressSound = buttonSFX[0];
        ButtonPressFirstHalf = buttonSFX[1];
        ButtonPressSecondHalf = buttonSFX[2];
        LightStartingSound = buttonSFX[3];
        LightLoopingSound = buttonSFX[4];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
