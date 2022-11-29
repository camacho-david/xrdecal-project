using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCreation : MonoBehaviour
{
    public GameObject prefabl;
    public GameObject prefabr;
    public float height;
    //stores time that a note appear
    public GameObject music;
    public List<float> nums = new List<float>(){};
    private int index =0;
    // stores position of a time
    List<Vector3> listOfPosition = new List<Vector3>() {};
    // Start is called before the first frame update
    private float nextSpawn = 0.0f;
    public float mindistance=5f;
    public float maxdistance=15f;
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
            var position = new Vector3(Random.Range(-10.0f, 10.0f), height, Random.Range(mindistance, maxdistance));
            listOfPosition.Add(position);
            //wait for this many seconds to create new note
            GameObject go;
            //("instantiating");
            
            //destroy note after 2 seconds
            int choice = Random.Range(0, 2);
            print("choice"+choice.ToString());
            if (choice==0)
            {
                go = Instantiate(prefabl, position, Quaternion.identity);
                go.tag = "Left";
            }
            else
            {
                go = Instantiate(prefabr, position, Quaternion.identity);
                go.tag = "Right";
            }

            Object.Destroy(go, 2f);


            index++;
            if (index+1<nums.Count){
            nextSpawn = nums[index];}
        
    }
    }
}
