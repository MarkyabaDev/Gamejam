using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WinningCondition : MonoBehaviour
{
    public List<Enemy> m_enemies;

    void Start()
    {
        m_enemies.AddRange(GameObject.FindObjectsOfType<Enemy>());

    }

    void Update()
    {
        CheckWinning();
    }

    void CheckWinning()
    {
        int walkers = 0;
        foreach (Enemy count in m_enemies)
        {
            if (count.GetComponent<AIMovement>().IsRunning == true)
            {
                walkers++;
            }  
        }

        if (m_enemies.Count == walkers)
        {
            Debug.Log("Winner");
        }
    }
}
