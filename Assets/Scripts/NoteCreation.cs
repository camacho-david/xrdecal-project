using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCreation : MonoBehaviour
{
    public GameObject prefab;
    //stores time that a note appear
    List<float> nums = new List<float>() { 1f, 2f, 3f, 4f, 5f };
    // stores position of a time
    List<Vector3> listOfPosition = new List<Vector3>();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine("CreateNote");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CreateNote(){
        print("function start");
        //create each note
        for (var i = 0; i < nums.Count; i++) {
            print("creating note "+ i);
            //randomizing position
            var position = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
            listOfPosition.Add(position);
            //wait for this many seconds to create new note
            yield return new WaitForSeconds(i);
            GameObject go;
            print("instantiating");
            go = Instantiate(prefab, listOfPosition[i], Quaternion.identity);
            //destroy note after 2 seconds
            Object.Destroy(go, 2.0f);
        }
    }
}
