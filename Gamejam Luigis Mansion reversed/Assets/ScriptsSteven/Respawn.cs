using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject Player;

    void OnTriggerEnter(Collider Player)
    {
        Debug.Log("Test");
        PlayerStats playerScript = Player.GetComponent<PlayerStats>();
        playerScript.lives -= 1;
        if (playerScript.lives <= 0)
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
        Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
    }  
}
