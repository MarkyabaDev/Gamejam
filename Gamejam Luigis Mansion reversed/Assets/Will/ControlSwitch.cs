using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSwitch : MonoBehaviour
{

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
}
