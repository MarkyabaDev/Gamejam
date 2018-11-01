using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {

    public float m_Speed = 5;
    public Transform[] m_Waypoints;
    public bool IsRunning = true;
    public GameObject enemy;
    public int m_currentIndex = 0;

    NavMeshAgent m_Runner;
    

    // Use this for initialization
    void Start () {
   

        m_Runner = GetComponent<NavMeshAgent>();
        m_Runner.SetDestination(m_Waypoints[1].position);
    }
	
	// Update is called once per frame
	void Update () {

        EnemyRun();

        
    }

    void EnemyRun()
    {
        if (IsRunning)
        {
            Vector3 dir = m_Waypoints[m_currentIndex].position - transform.position;
            dir = dir.normalized;
            dir = dir * Time.deltaTime * m_Speed;
            if (m_Runner.remainingDistance <= 0.5f) 
            {
                m_currentIndex = Random.Range(0, m_Waypoints.Length);
                m_Runner.SetDestination(m_Waypoints[m_currentIndex].position);
            }
        }
        else
        {
            // System.Threading.Thread.Sleep(5000);
           
            IsRunning = true;

        }
    }

}
