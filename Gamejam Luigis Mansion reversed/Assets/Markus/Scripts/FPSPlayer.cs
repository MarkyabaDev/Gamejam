using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSPlayer : MonoBehaviour
{

    public GameObject CubePrefab;
    public float m_Speed = 1;
    public float m_rotationSpeed = 120;
    public Camera Cam;
    public LayerMask m_cubeLayers;

    public Material _playerMaterial;

    CharacterController m_char;
    private float m_ySpeed;
    public float jumpSpeed = 100;

    //CAM
    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    public float dissolveAmount  = -1;
    public bool dissolving = false;
    public bool appearing = false;
    public float dissolvingSpeed = 1;

    private float _rotationX = 0;

    private Dropdown m_shortcutDropdown;

    // Use this for initialization
    void Start()
    {
        m_char = GetComponent<CharacterController>();
        m_shortcutDropdown = GameObject.Find("ShortcutDropdown").GetComponent<Dropdown>();

        m_shortcutDropdown.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DissolveAndTeleport();
        Appear();
        
    }

    void Move()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
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

    void DissolveAndTeleport()
    {
        if (dissolving)
        {
            dissolveAmount = Mathf.Lerp(dissolveAmount, 1.5f, Time.deltaTime * dissolvingSpeed);
            _playerMaterial.SetFloat("Vector1_841C2CC7", dissolveAmount);

            if(dissolveAmount >= 1)
            {
                dissolving = false;
                appearing = true;
                transform.position = GameObject.Find(m_shortcutDropdown.options[m_shortcutDropdown.value].text).transform.position + new Vector3(1, 0, 0);
            }
        }
    }

    void Appear()
    {
        if(appearing)
        {
            dissolveAmount = Mathf.Lerp(dissolveAmount, -1.5f, Time.deltaTime * dissolvingSpeed);
            _playerMaterial.SetFloat("Vector1_841C2CC7", dissolveAmount);
            if (dissolveAmount <= -1)
            {
                appearing = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shortcut")
        {
            m_shortcutDropdown.ClearOptions();
            
            m_shortcutDropdown.AddOptions(GameController.gameControllerInstance.shortcutNames);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shortcut")
        {
            if (Input.GetMouseButtonDown(1) && !m_shortcutDropdown.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                transform.LookAt(other.transform.position);
                Cam.transform.LookAt(other.transform.position);
                m_shortcutDropdown.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButtonDown(1) && m_shortcutDropdown.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                m_shortcutDropdown.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shortcut")
        {
            Cursor.lockState = CursorLockMode.Locked;
            m_shortcutDropdown.gameObject.SetActive(false);
        }
    }

    public void UseShortcut()
    {
        dissolving = true;              
    }
}
