using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaulichter : MonoBehaviour {

    public List<GameObject> lights;
    public float rotSpeed = 2;
	// Use this for initialization
	void Start ()
    {
        lights.AddRange(GameObject.FindGameObjectsWithTag("Blaulicht"));
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        RotateAllLights();
	}

    void RotateAllLights()
    {
        foreach (GameObject go in lights)
        {
            go.transform.eulerAngles += new Vector3(0, Time.deltaTime * rotSpeed, 0);
        }
    }
}
