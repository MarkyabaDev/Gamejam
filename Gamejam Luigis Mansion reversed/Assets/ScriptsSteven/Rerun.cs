using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rerun : MonoBehaviour {

    public GameObject pauseOverlay;
    public GameObject player;
    public GameObject SpawnPoint;

    public void RunAgain()
    {
        pauseOverlay.SetActive(false);
        Time.timeScale = 1;
        player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
    }
}
