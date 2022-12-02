using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readAudio : MonoBehaviour
{
    private float[] samples;
    public float threshold = 1f; 
    private float cooldown = 0.5f; // in seconds
    private float lastbeat=0f; // in seconds
    private float herz;
    private float currenttime; // in seconds
    private float lastenergy=0; // between -1 and 1
    private float lastlastenergy=0;
    private float detectinterval = 0.05f; //in seconds
    static public  List<float> beatarray = new List<float>(); //array of timestamps of beats
    // Start is called before the first frame update
    void Awake()

    {
        beatarray = new List<float>();
        // put song data into this samples array
        AudioSource audioSource = GetComponent<AudioSource>();
        int samples_len = audioSource.clip.samples * audioSource.clip.channels;
        samples = new float[samples_len];
        audioSource.clip.GetData(samples, 0);

        herz = audioSource.clip.frequency;
        detectinterval *= herz;
        
        // looping through sample
        for (int index = 0; index < samples_len; index+=(int)detectinterval)
        {
            currenttime = (float)index/(herz* audioSource.clip.channels);
            // if the cooldown time has pass, 
            if (currenttime >= cooldown + lastbeat){
                // then check whether this sample exceed previous sample more than thresholds
                if (samples[index]>=threshold+lastenergy || samples[index]<=-threshold+lastenergy
                // ||samples[index]>=threshold+lastlastenergy || samples[index]<=-threshold+lastlastenergy
                ){
                
                    // take note of this beat
                    // print( samples[index] );
                    lastbeat = (float)index/(herz* audioSource.clip.channels); // update this to become the last beat
                    if (lastbeat > 1)
                    {
                        beatarray.Add(lastbeat-1);
                    }
                    
                    // print(lastbeat);
                }
            }
        lastenergy = samples[index];
        if (index>detectinterval){
        lastlastenergy = samples[index- (int)detectinterval]; }
        }
    // print(beatarray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
