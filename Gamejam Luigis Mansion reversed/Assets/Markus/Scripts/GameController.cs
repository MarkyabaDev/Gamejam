using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> shortcuts;
    public List<string> shortcutNames;

    public static GameController gameControllerInstance;

    private void Awake()
    {
        if (gameControllerInstance == null)

            gameControllerInstance = this;

        else if (gameControllerInstance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        shortcuts.AddRange(GameObject.FindGameObjectsWithTag("Shortcut"));
        foreach(GameObject go in shortcuts)
        {
            shortcutNames.Add(go.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
