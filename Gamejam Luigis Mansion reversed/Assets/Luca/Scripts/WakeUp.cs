using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}


    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Wake")
        {
            if (collider.transform.parent.GetComponent<AIMovement>().m_IsSleeping)
            {
                collider.transform.parent.GetComponent<AIMovement>().m_IsSleeping = false;
            }

        }
    }
}
