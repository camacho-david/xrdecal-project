using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readAudio : MonoBehaviour
{
    private float[] samples;
    private float threshold = 1f; 
    private float cooldown = 0.5f; // in seconds
    private float lastbeat=0f; // in seconds
    private float herz = 44100f;
    private float currenttime; // in seconds
    private float lastenergy=0; // between -1 and 1
    private float lastlastenergy=0;
    private int detectinterval = 100; //in hertz
    static public  List<float> beatarray = new List<float>(); //array of timestamps of beats
    // Start is called before the first frame update
    void Start()
    {   // put song data into this samples array
        AudioSource audioSource = GetComponent<AudioSource>();
        int samples_len = audioSource.clip.samples * audioSource.clip.channels;
        samples = new float[samples_len];
        audioSource.clip.GetData(samples, 0);

        // looping through sample
        for (int index = 0; index < samples_len; index+=detectinterval)
        {
            currenttime = (float)index/herz;
            // if the cooldown time has pass, 
            if (currenttime >= cooldown + lastbeat){
                // then check whether this sample exceed previous sample more than thresholds
                if (samples[index]>=threshold+lastenergy || samples[index]<=-threshold+lastenergy
                // ||samples[index]>=threshold+lastlastenergy || samples[index]<=-threshold+lastlastenergy
                ){
                
                    // take note of this beat
                    // print( samples[index] );
                    lastbeat = (float)index/herz; // update this to become the last beat
                    beatarray.Add(lastbeat);
                    // print(lastbeat);
                    }
            }
        lastenergy = samples[index];
        if (index>detectinterval){
        lastlastenergy = samples[index-detectinterval]; }
        }
    // print(beatarray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
