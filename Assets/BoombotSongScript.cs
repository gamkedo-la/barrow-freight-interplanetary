using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombotSongScript : MonoBehaviour
{
    private AudioSource BoombotSong;
    private AudioSource[] ArrayOfCameraAudioSources;
    private float[] NumberOfBoombotSongSamples;
    private int Channel;
    private int secondsToWait;

    // Start is called before the first frame update
    void Start()
    {
        ArrayOfCameraAudioSources = GetComponents<AudioSource>();
        BoombotSong = ArrayOfCameraAudioSources[1];
        Debug.Log(BoombotSong);
        BoombotSong.Play();
        BoombotSong.GetOutputData(NumberOfBoombotSongSamples,Channel);
        //secondsToWait = 3;
        //yield return new WaitForSeconds(secondsToWait);
        Debug.Log(NumberOfBoombotSongSamples);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
