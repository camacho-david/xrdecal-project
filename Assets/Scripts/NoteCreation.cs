using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteCreation : MonoBehaviour
{
    public GameObject prefabl;
    public GameObject prefabr;
    public float height;
    //stores time that a note appear
    public GameObject music;
    public static List<float> nums = new List<float>(){};
    private int index =0;
    // stores position of a time
    List<Vector3> listOfPosition = new List<Vector3>() {};
    // Start is called before the first frame update
    private float nextSpawn = 0.0f;
    public float mindistance=5f;
    public float maxdistance=15f;

    public Score scoreScript;

    float endTime = 999999999999999999;

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
        if (index==nums.Count)
        {
            scoreScript.GameOver(nums.Count);
            endTime = Time.timeSinceLevelLoad;
            index++;
            nums = new List<float>() { };
            print("COUNT " + nums.Count.ToString());
        }
        else if (Time.timeSinceLevelLoad >= endTime + 5f)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);


        }
        else if (Time.timeSinceLevelLoad >= nextSpawn && index<nums.Count){
            //print("the beat timestamp");
            //print(nums[index]);
        
            // //randomizing position
            var position = new Vector3(Random.Range(-7.0f, 7.0f), height, Random.Range(mindistance, maxdistance));
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

            Object.Destroy(go, 1.5f);


            index++;
            print(index);
            if (index<nums.Count){
            nextSpawn = nums[index];}
        
        }
    }
}
