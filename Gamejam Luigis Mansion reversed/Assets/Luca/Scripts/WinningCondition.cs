using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinningCondition : MonoBehaviour
{
    public List<Enemy> m_enemies;
    public GameObject WinUI;

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
            if (count.GetComponent<AIMovement>().m_IsSleeping == false)
            {
                walkers++;
            }  
        }

        if (m_enemies.Count == walkers)
        {
            Debug.Log("Winner");
            //SceneManager.LoadScene("WinningScreen", LoadSceneMode.Additive);
            //return;
            Time.timeScale = 0;
            WinUI.SetActive(true);
        }
    }
}
