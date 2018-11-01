using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySwitch : MonoBehaviour
{

    public void PlayScene()
    {
        SceneManager.LoadScene("Game");
    }
}
