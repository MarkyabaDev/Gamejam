using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSpawnPoint : MonoBehaviour {

    public GameObject SpawnMain;
    public GameObject SpawnGarage;
    public GameObject SpawnBack;
    public GameObject ChangeSpawnTo;
    public GameObject SpawnPoint;

    public List<GameObject> SelectSpawn;
    private string spawnName;

    private void Start()
    {
        
    }

    public void UseSpawnPoint()
    {
        SelectSpawn.Clear();
        SelectSpawn.AddRange(GameObject.FindGameObjectsWithTag("SpawnPointSelect"));
        foreach (GameObject child in SelectSpawn)
        {
            if (child.transform.GetComponent<Toggle>().isOn)
            {
                spawnName = child.transform.GetChild(1).GetComponent<Text>().text;
                ChangeSpawnTo = GameObject.Find("Spawnpoints/"+spawnName);
                Debug.Log("Spawnpoints/"+spawnName);
                SpawnPoint.transform.position = new Vector3(ChangeSpawnTo.transform.position.x, ChangeSpawnTo.transform.position.y, ChangeSpawnTo.transform.position.z);

            }
        }
    }
}
