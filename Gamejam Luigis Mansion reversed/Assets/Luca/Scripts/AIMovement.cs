using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {

    public float m_Speed = 5;
    public Transform[] m_Waypoints;
    public GameObject enemy;
    public bool IsRunning = true;
    public bool m_IsSleeping = true;
    public int m_currentIndex = 0;
    public float m_AmountStunTime = 5;

    private float m_timer = 0;

    NavMeshAgent m_Runner;
    

    // Use this for initialization
    void Start () {
   

        m_Runner = GetComponent<NavMeshAgent>();
        //m_Runner.SetDestination(m_Waypoints[0].position);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_IsSleeping)
        {
            
        }
        else
        {
            EnemyRun();
        }
       

        
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

            m_Runner.isStopped = true;
           if(m_timer <= m_AmountStunTime)
            {
                m_timer += Time.deltaTime;
            }
           else
            {
                m_Runner.isStopped = false;
                IsRunning = true;
                m_timer = 0;
            }   
            // 6e78a0a52fc111b547144c7e881f30b217478f69

        }
    }
}
