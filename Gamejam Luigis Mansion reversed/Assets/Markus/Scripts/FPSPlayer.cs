using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSPlayer : MonoBehaviour
{
    public float m_Speed = 1;
    public float m_rotationSpeed = 120;
    public Camera m_Cam;

    public Transform m_thirdPersonCameraSpot, m_firstPersonCameraSpot;

    public Material m_playerMaterial;

    CharacterController m_char;
    public float m_jumpSpeed = 100;

    //CAM
    public float m_minVert = -45.0f;
    public float m_maxVert = 45.0f;
    public float m_dissolveAmount  = -1;
    public float m_dissolvingSpeed = 1;
    public float m_cameraChangeSpeed = 1;
    public float m_pushPower = 2.0f;

    private float m_ySpeed;
    private bool m_dissolving = false;
    private bool m_thirdPerson = false;

    public bool m_dead = false;
    public bool m_appearing = false;

    private float m_rotationX = 0;

    private Dropdown m_shortcutDropdown;
    private Renderer m_renderer;
    private Vector3 m_cameraPosition = new Vector3();

    void Awake()
    {
        m_char = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {   
        m_shortcutDropdown = GameObject.Find("ShortcutDropdown").GetComponent<Dropdown>();
        m_renderer = GetComponentInChildren<MeshRenderer>();
        

        m_shortcutDropdown.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_thirdPerson)
        {
            Move();
        }
        DissolveAndTeleport();
        Appear();
        
        
    }

    private void LateUpdate()
    {
        if (!m_thirdPerson)
        {
            CameraMove();
            CameraToFirstPerson();
        }
        else
        {
            m_Cam.transform.LookAt(transform.position + new Vector3(0,1.5f,0));
            CameraToThirdPerson();
        }
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
                m_ySpeed = m_jumpSpeed;
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

    }
    
    void CameraMove()
    {
        m_rotationX -= Input.GetAxis("Mouse Y") * Time.deltaTime * m_rotationSpeed;
        m_rotationX = Mathf.Clamp(m_rotationX, m_minVert, m_maxVert);
        float rotationY = transform.rotation.y;
        m_Cam.transform.localEulerAngles = new Vector3(m_rotationX, rotationY, 0);
    }

    void CameraToThirdPerson()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(m_firstPersonCameraSpot.position, m_thirdPersonCameraSpot.position - m_firstPersonCameraSpot.position, out hit, 3))
        {
            m_cameraPosition = hit.point;
        }
        else
        {
            m_cameraPosition = m_thirdPersonCameraSpot.position;
        }
        m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, m_cameraPosition, Time.deltaTime * m_cameraChangeSpeed);
    }

    void CameraToFirstPerson()
    {
        m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, m_firstPersonCameraSpot.position, Time.deltaTime * m_cameraChangeSpeed * 2);
    }


    void DissolveAndTeleport()
    {
        if (m_dissolving || m_dead)
        {
            if (m_dead)
            {
                m_playerMaterial.SetColor("Color_AA70D6CC", Color.red);
            }
            else
            {
                m_playerMaterial.SetColor("Color_AA70D6CC", Color.cyan);
            }

            m_thirdPerson = true;
            m_renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            m_char.enabled = false;
            m_dissolveAmount = Mathf.Lerp(m_dissolveAmount, 1.5f, Time.deltaTime * m_dissolvingSpeed);
            m_playerMaterial.SetFloat("Vector1_841C2CC7", m_dissolveAmount);

            

            if(m_dissolveAmount >= 1)
            {
                m_dissolving = false;
                if (!m_dead)
                {
                    
                    m_appearing = true;
                    
                    transform.position = GameObject.Find(m_shortcutDropdown.options[m_shortcutDropdown.value].text).transform.GetChild(0).position;
                }
            }
        }
    }

    void Appear()
    {
        if(m_appearing)
        {
            m_dead = false;
            m_dissolveAmount = Mathf.Lerp(m_dissolveAmount, -1.5f, Time.deltaTime * m_dissolvingSpeed);
            m_playerMaterial.SetFloat("Vector1_841C2CC7", m_dissolveAmount);
            if (m_dissolveAmount <= -1)
            {
                m_renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                m_char.enabled = true;
                m_appearing = false;
                m_thirdPerson = false;
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
                GameController.gameControllerInstance.isOnShortcut = true;
                transform.LookAt(other.transform.position);
                m_Cam.transform.LookAt(other.transform.position);
                m_shortcutDropdown.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButtonDown(1) && m_shortcutDropdown.gameObject.activeSelf)
            {
                GameController.gameControllerInstance.isOnShortcut = false;
                m_shortcutDropdown.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shortcut")
        {
            GameController.gameControllerInstance.isOnShortcut = false;
            m_shortcutDropdown.gameObject.SetActive(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if(body == null || body.isKinematic) { return; }

        if (hit.moveDirection.y < -0.3) { return; }
        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.
        // Apply the push
        body.velocity = pushDir * m_pushPower;

    }

    public void UseShortcut()
    {
        m_dissolving = true;
    }
}
