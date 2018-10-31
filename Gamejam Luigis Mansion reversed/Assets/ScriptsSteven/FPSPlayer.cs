using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{

    public GameObject CubePrefab;
    public float m_Speed = 1;
    public float m_rotationSpeed = 120;
    public Camera Cam;
    public LayerMask m_cubeLayers;
    CharacterController m_char;
    private float m_ySpeed;
    public float jumpSpeed = 100;

    //CAM
    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    private float _rotationX = 0;
    // Use this for initialization
    void Start()
    {
        m_char = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ProcessPlanting();

    }

    void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Ipnut könnte länger als 1 sein, somit würden wir diagonal schneller sein.w
        move = move.normalized;
        if (!m_char.isGrounded)
        {
            m_ySpeed -= 9.81f * Time.deltaTime;

        }
        else
        {
            if (Input.GetButton("Fire3"))
            {
                move *= 2;
            }

            if (Input.GetButton("Jump"))
            {
                m_ySpeed = jumpSpeed;
            }
            else
            {
                m_ySpeed = -0.1f;
            }

        }
        move.y = m_ySpeed;
        //WAndelt eine Characterbezogene Richtung in Weltrichtung
        move = transform.TransformDirection(move);
        move *= Time.deltaTime * m_Speed;
        //Führt eine Physikalische Bewegung durch (kann mit allem möglichen Kollidieren, Verhalten wie im Charactercontroller eingestellt)
        m_char.Move(move);

        m_char.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * m_rotationSpeed);

        _rotationX -= Input.GetAxis("Mouse Y") * Time.deltaTime * m_rotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
        float rotationY = transform.rotation.y;
        Cam.transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

    }

    void ProcessPlanting()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, m_cubeLayers))
                {
                    //Wenn wir collider treffen, dann platzieren
                    //Quaternion, ist keine rotation
                    GameObject tree = Instantiate(CubePrefab, hit.point, Quaternion.identity);
                    tree.transform.up = hit.normal;
                }
            }

        }
    }
}
