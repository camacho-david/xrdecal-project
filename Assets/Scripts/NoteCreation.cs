using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCreation : MonoBehaviour
{
    public GameObject prefab;
    public float height;
    //stores time that a note appear
    public GameObject music;
    public List<float> nums = new List<float>(){};
    private int index =0;
    // stores position of a time
    List<Vector3> listOfPosition = new List<Vector3>() {};
    // Start is called before the first frame update
    private float nextSpawn = 0.0f;
    void Start()
    {
        nums = readAudio.beatarray;
        foreach (float beat in nums){
        print(beat);
        }
        nextSpawn = nums[index];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            
        if (Time.time >= nextSpawn && index<nums.Count){
            //print("the beat timestamp");
            //print(nums[index]);
        
            // //randomizing position
            var position = new Vector3(Random.Range(-10.0f, 10.0f), height, Random.Range(0.0f, 20.0f));
            listOfPosition.Add(position);
            //wait for this many seconds to create new note
            GameObject go;
            //("instantiating");
            go = Instantiate(prefab, position, Quaternion.identity);
            //destroy note after 2 seconds
            go.tag = "Target";


            //Object.Destroy(go, 2f);


            index++;
            if (index+1<nums.Count){
            nextSpawn = nums[index];}
        
    }
    }
}
