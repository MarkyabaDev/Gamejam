using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject Player;

    private FPSPlayer fpsPlayer;

    private void Start()
    {
        fpsPlayer = Player.GetComponent<FPSPlayer>();
    }

    private void Update()
    {
        if(fpsPlayer.m_dead && fpsPlayer.m_dissolveAmount >= 0.5f)
        {
            this.Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
            fpsPlayer.m_appearing = true;
        }
    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            if (!fpsPlayer.m_dead)
            {
                Debug.Log("Test");
                PlayerStats playerScript = this.Player.GetComponent<PlayerStats>();
                playerScript.lives -= 1;
                if (playerScript.lives <= 0)
                {
                    SceneManager.LoadScene("Level1", LoadSceneMode.Single);
                }
                fpsPlayer.m_dead = true;
            }
        }
    }  
}
