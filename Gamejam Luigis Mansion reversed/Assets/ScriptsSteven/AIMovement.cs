using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    public float m_Speed = 5;

    public Transform[] m_Waypoints;
    public bool m_GoingForward = true;
    public bool IsRunning = false;
    private int m_currentIndex = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 dir = m_Waypoints[m_currentIndex].position - transform.position;
        dir = dir.normalized;
        dir = dir * Time.deltaTime * m_Speed;

        if (dir.magnitude > Vector3.Distance(m_Waypoints[m_currentIndex].position, transform.position))
        {
            m_currentIndex = Random.Range(0, m_Waypoints.Length);
           

        }

        transform.Translate(dir, Space.World);

    }
}
