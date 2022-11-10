using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetection : MonoBehaviour
{
    AudioSource track;
    AudioClip clip;
    float[] samples; // array that will be populated with audio samples
    int sampleRate; // number of samples per second 
    int channelCount;
    int instantEnergySampleCount = 1024;
    
    void BufferTrackSampleData()
    {

    }

    void SimpleSoundEnergyBeatDetection()
    {
         // if (instant energy) > C * (local energy average) then register a beat.
        for (int pos = sampleRate; pos < samples.Length; pos += instantEnergySampleCount * channelCount)
        {
            // Calculate instant energy 
            // E_left + E_right
            // Sum from pos -> pos + instantEnergySampleCount (1024) { left_signal^2 + right_signal^2 }
            float instantEnergy = 0;
            for (int l = pos; l < pos + instantEnergySampleCount * channelCount; l += channelCount)
            {
                for (int i = 0; i < channelCount; i++)
                {
                    instantEnergy += Mathf.Pow(samples[l + i], 2);
                }
            }

            // TODO: Add caching here! (Instant Energy calculations can be used for local average energy)
            // Calculate local average energy
            // Sum from pos -> pos + instantEnergySampleCount (1024) { left_signal^2 + right_signal_^2 } 
            float localEnergyAverage = 0;
            // TODO : write this part

        }
    }

    void Start()
    {
        track = GameObject.FindGameObjectWithTag("Track").GetComponent<AudioSource>();
        clip = track.clip;
        sampleRate = clip.frequency;
        channelCount = clip.channels;

        // Code for using AudioSource.GetSpectrumData():
        /*
        int samplesSize = Mathf.ClosestPowerOfTwo(clip.samples);
        samples = new float[samplesSize];

        //track.Play();
        
        // Incomplete code:
        // Grab the sample data as the song plays and store it in array. (Slow)
        //track.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        */

        // Code for using AudioClip.GetData():
        samples = new float[clip.samples * clip.channels];
        float endTime = 0;
        float startTime = Time.time;   
        clip.GetData(samples, 0);
        endTime = Time.time;

        // test: print the number of channels in the clip 
        Debug.Log("GetData() took " + (endTime - startTime) + " seconds.");
        Debug.Log("Channel count: " + clip.channels);
        Debug.Log("Samples count: " + clip.samples);
        Debug.Log("Sample rate: " + sampleRate);
        Debug.Log("Sample array length: " + samples.Length);
        Debug.Log("Samples data: " + samples);
        Debug.Log("Element 0: " + samples[0]);
        Debug.Log("Element middle: " + samples[samples.Length / 2]);
        Debug.Log("Element last: " + samples[samples.Length - 1]);

        
    }

    void Update()
    {
        
    }
}
