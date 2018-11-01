using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rerun : MonoBehaviour {

    public GameObject pauseOverlay;
    public GameObject player;
    public GameObject SpawnPoint;

    private void Awake()
    {
        Debug.Log("<b>" + FindObjectsOfType<Rerun>().Length + "</b>");
    }

    public void RunAgain()
    {
        pauseOverlay.SetActive(false);
        
        Time.timeScale = 1;
        Debug.Log(player.name);
        Debug.Log(SpawnPoint.name);
        player.GetComponent<FPSPlayer>().m_appearing = true;

        player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
    }
}
