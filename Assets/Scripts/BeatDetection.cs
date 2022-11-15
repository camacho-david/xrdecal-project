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
    int instantSamples = 1024;
    float c = 1.3f;
    Queue<Beat> beats;
    
    void BufferTrackSampleData()
    {

    }
    
    public class Beat
    {
        float sample;
        float time;
        float instantEnergy;
        public Beat(int sample, float time, float energy)
        {
            this.sample = sample;
            this.time = time;
            this.instantEnergy = energy;
        }
    }
    void SimpleSoundEnergyBeatDetection()
    {
        beats = new Queue<Beat>();
        float[] sampleEnergies = new float[samples.Length / channelCount];
        for (int pos = 0; pos < samples.Length; pos += channelCount)
        {
            int sampleIndex = pos / channelCount;
            sampleEnergies[sampleIndex] = 0;
            // The energy for one sample is the sum of the energies for each channel.
            for (int chan = 0; chan < channelCount; chan += 1)
            {
                sampleEnergies[sampleIndex] += Mathf.Pow(samples[pos + chan], 2);
            }
            if (sampleIndex % instantSamples == 0 && sampleIndex >= sampleRate) // if sampleIndex is divisible by 1024
            {
                // Calculate instant energy for the last instantSamples many samples.
                float instantEnergy = 0;
                for (int i = sampleIndex - instantSamples; i < sampleIndex; i++)
                {
                    instantEnergy += sampleEnergies[i];
                }
                // Calculate total energy for the last sampleRate many samples.
                float totalEnergy = 0; 
                for (int i = sampleIndex - sampleRate; i < sampleIndex; i++)
                {
                    totalEnergy += sampleEnergies[i];
                }
                // Calculate average local energy by multiplying total energy by (instantSamples / sampleRate).
                float localAverageEnergy = totalEnergy * instantSamples / sampleRate;
                // If instant energy > C * average local energy then register a beat. 
                if (instantEnergy > c * localAverageEnergy)
                {
                    // Register a beat by adding it to a queue.
                    beats.Enqueue(new Beat(sampleIndex, sampleIndex / sampleRate, instantEnergy));
                }
            }
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
