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

    public double BPM = 130f;
    private double eachTick = 0.0f;
    private double sampleRate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        ArrayOfCameraAudioSources = GetComponents<AudioSource>();

        //BoombotSongSource = GetComponents<AudioSource>();
        BoombotSongSource = ArrayOfCameraAudioSources[0];
        //Debug.Log(BoombotSongSource);

        BoombotSongClip = BoombotSongSource.clip;
        BoombotSongTimeLength = BoombotSongClip.length;

        BoombotSongTotalSamples = BoombotSongClip.samples;
        

        BoombotSongSource.Play();

        //

        double startTick = AudioSettings.dspTime/1000;
        sampleRate = AudioSettings.outputSampleRate;
        eachTick = startTick * sampleRate;

        //Debug.Log("startTick = " + startTick);
        //Debug.Log("sampleRate = " + sampleRate);
        //Debug.Log("eachTick = " + eachTick);
    }

    // Update is called once per frame
    void Update()
    {
        //BoombotSongSpeakerPositions = BoombotSongSource.GetOutputData(1024, 0);
        //Debug.Log(BoombotSongSpeakerPositions);
        BoombotSongSample = BoombotSongSource.timeSamples;
        //Debug.Log(BoombotSongSample);
        BoombotSongCurrentTime = BoombotSongSource.time;
        //Debug.Log(BoombotSongCurrentTime);
        

    }
}
