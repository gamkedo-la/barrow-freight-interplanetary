using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
    public class PlayAudio : MonoBehaviour
    {
    AudioSource[] ArrayOfBoomBotSounds;
    AudioSource BassDrumSound;
    AudioSource SnareDrumSound;


    private void Start()
    {
        ArrayOfBoomBotSounds = GetComponents<AudioSource>();
        BassDrumSound = ArrayOfBoomBotSounds[0];
        SnareDrumSound = ArrayOfBoomBotSounds[1];
    }
    public void PlayBassDrum()
        {
            
            //BassDrumSound.Play(0);
        Debug.Log("Bass Drum Played");
        }

    public void PlaySnareDrum()
    {
        //SnareDrumSound.Play(0);
        Debug.Log("Snare Drum Played");
    }

    public void PlayBassAndSnare()
    {
        BassDrumSound.Play(0);
        SnareDrumSound.Play(0);
    }

    private void OnEnable()
    {
        PlayBassDrum();
        PlaySnareDrum();
    }

    /*void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Pause"))
        {
            audioData.Pause();
            Debug.Log("Pause: " + audioData.time);
        }

        if (GUI.Button(new Rect(10, 170, 150, 30), "Continue"))
        {
            audioData.UnPause();
        }
    }*/
}

