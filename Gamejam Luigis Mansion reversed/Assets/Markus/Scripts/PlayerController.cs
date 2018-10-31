using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController m_playerController;
    public float speed;

    void Awake()
    {
        m_playerController = GetComponent<CharacterController>();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3();

        movement.x += Input.GetAxis("Vertical") * speed* Time.deltaTime;
        movement.z += Input.GetAxis("Horizontal") * speed * Time.deltaTime;


        if(!m_playerController.isGrounded)
        {
            movement.y -= 9.81f * Time.deltaTime;
        }

        m_playerController.Move(movement.normalized);
    }
}
