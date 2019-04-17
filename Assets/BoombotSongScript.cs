using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombotSongScript : MonoBehaviour
{
    private AudioSource BoombotSongSource;
    private AudioSource[] ArrayOfCameraAudioSources;
    
    private AudioClip BoombotSongClip;

    public float[] BoombotSongSpeakerPositions;
    public int BoombotSongSample;
    public float BoombotSongCurrentTime;
    public float BoombotSongTimeLength;
    public float BoombotSongTotalSamples;
    
    private int Channel;
    private int secondsToWait;

    // Start is called before the first frame update
    void Start()
    {
        ArrayOfCameraAudioSources = GetComponents<AudioSource>();        ;

        BoombotSongSource = ArrayOfCameraAudioSources[1];
        Debug.Log(BoombotSongSource);

        BoombotSongClip = BoombotSongSource.clip;
        BoombotSongTimeLength = BoombotSongClip.length;

        BoombotSongTotalSamples = BoombotSongClip.samples;
        

        BoombotSongSource.Play();

        //secondsToWait = 3;
        //yield return new WaitForSeconds(secondsToWait);
        
    }

    // Update is called once per frame
    void Update()
    {
        //BoombotSongSpeakerPositions = BoombotSongSource.GetOutputData(1024, 0);
        //Debug.Log(BoombotSongSpeakerPositions);
        BoombotSongSample = BoombotSongSource.timeSamples;
        Debug.Log(BoombotSongSample);
        BoombotSongCurrentTime = BoombotSongSource.time;
        Debug.Log(BoombotSongCurrentTime);
        

    }
}
