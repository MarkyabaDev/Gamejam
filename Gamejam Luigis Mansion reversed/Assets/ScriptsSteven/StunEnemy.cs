using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour {

    public GameObject Enemy;
    public GameObject stun;
    AIMovement AiScript;

    void Start () {
        AiScript = Enemy.GetComponent<AIMovement>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (collider.gameObject == stun)
        {
            AiScript.IsRunning = true;
            Debug.Log("Collide");
        }
    }
}
