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

    public Vector3 buttonSoundManagerPosition;
    public GameObject StandardButtonTest;
    public GameObject StandardButtonTestLight;

    public StandardButtonLightScript standardButtonLightScript;
    public Vector3 StandardButtonLightPosition;

    public ToggleButtonLightScript toggleButtonLightScript;
    public Vector3 ToggleButtonLightPosition;

    public HoldButtonLightScript holdButtonLightScript;
    public Vector3 HoldButtonLightPosition;

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

        standardButtonLightScript = GameObject.Find("Standard Button Test").GetComponent<StandardButtonLightScript>();
        StandardButtonLightPosition = standardButtonLightScript.standardButtonLightPosition;
        Debug.Log(StandardButtonLightPosition);

        toggleButtonLightScript = GameObject.Find("Toggle Button Test").GetComponent<ToggleButtonLightScript>();
        ToggleButtonLightPosition = toggleButtonLightScript.toggleButtonLightPosition;

        holdButtonLightScript = GameObject.Find("Hold Button Test").GetComponent<HoldButtonLightScript>();
        HoldButtonLightPosition = holdButtonLightScript.holdButtonLightPosition;

        buttonSoundManagerPosition = transform.position;

    }

    public void updateButtonSoundManagerPosition(Vector3 buttonPosition) {
        transform.position = buttonPosition;
    }
}
