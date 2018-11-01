using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClamp : MonoBehaviour {

    public float m_maxDegrees = 90;

    private float m_currentDegreesY;

    private Vector3 doorRotation;

    // Use this for initialization
    private void Awake()
    {

    }

    void Start ()
    {
        m_currentDegreesY = transform.eulerAngles.y;
        doorRotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        doorRotation.y = Mathf.Clamp(doorRotation.y, -m_maxDegrees, m_maxDegrees);
        Debug.Log(m_currentDegreesY + " | " + doorRotation.y);
        transform.eulerAngles = doorRotation;

    }
}
