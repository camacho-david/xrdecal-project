using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCreation : MonoBehaviour
{
    public GameObject prefab;
    //stores time that a note appear
    List<float> nums = new List<float>() { 60/82f, 60/82f, 60/82f, 60/82f, 60/82f };
    // stores position of a time
    List<Vector3> listOfPosition = new List<Vector3>() {};
    // Start is called before the first frame update
    private float nextSpawn = 0.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextSpawn){

        
            // //randomizing position
            var position = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
            listOfPosition.Add(position);
            //wait for this many seconds to create new note
            GameObject go;
            print("instantiating");
            go = Instantiate(prefab, position, Quaternion.identity);
            //destroy note after 2 seconds
            Object.Destroy(go, 600/82f);
            nextSpawn = Time.time+30/82f;
        }
    }

}
