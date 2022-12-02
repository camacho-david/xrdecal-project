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
    int instantSamples = 512;
    /*TODO: Find a perfect value for C. Too low = too many beats, too high = too few (or none) beats
     * Good value for 60 bpm metronome is 20
     * Good value for 155 bpm metronome is 10
     */
    public float c = 1.7f;
    public Queue<Beat> beats;
    
    void BufferTrackSampleData()
    {

    }
    
    public class Beat
    {
        public float sample;
        public float time;
        public float instantEnergy;
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
                    Beat beat = new Beat(sampleIndex, (float) sampleIndex / sampleRate, instantEnergy);
                    beats.Enqueue(beat);
                    Debug.Log("Beat detected at time: (" + beat.time.ToString("F6") + ")");
                }
            }
        }
        Debug.Log("Total number of beats: " + beats.Count);
        Debug.Log("Average BPM: " + (float) beats.Count / (clip.length / 60f));
    }

    void Start()
    {
        // Grab the game objects with our audio data
        track = GameObject.FindGameObjectWithTag("Track").GetComponent<AudioSource>();
        clip = track.clip;
        sampleRate = clip.frequency;
        channelCount = clip.channels;

        // Get the raw sample data
        samples = new float[clip.samples * clip.channels];
        float endTime = 0;
        float startTime = Time.time;   
        clip.GetData(samples, 0);
        endTime = Time.time;

        SimpleSoundEnergyBeatDetection();

        track.Play();
        
        /*
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
        */
        
    }

    void Update()
    {
        
    }
}
