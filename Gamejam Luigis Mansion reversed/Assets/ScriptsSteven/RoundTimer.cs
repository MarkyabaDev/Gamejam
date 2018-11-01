using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour {

    public GameObject pauseOverlay;
    public GameObject gameOver;

    public bool pipeline = false;
    public bool vent = false;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    private int nextRound = 0;
    private int timeCounter = 0;
    private int startingTime = 79200;
    private int timeInMinutes { get { return (startingTime / 60)%60; } }
    private int timeInHours { get { return startingTime / 3600; } }

    public Text Timer;
    private int timeLeft = 28800/8;

	void Start () {
        Timer.text = "Time left: " + timeLeft;
    }

	void Update () {
        nextActionTime += Time.deltaTime;
        if (nextActionTime >= 1/12)
        {
            nextActionTime -= 1.0f/12.0f;
            TimeChange();
        }

        if(startingTime == 0)
        {
            return;
        }
    }

    void TimeChange()
    {
        timeLeft -= 1;
        Timer.text = string.Format("{0:0}:{1:00}",timeInHours, timeInMinutes);
        startingTime += 1;
        timeCounter += 60;
        if (startingTime == 86400)
        {
            startingTime = 0;
        }

        if (timeCounter == 3600)
        {
            Debug.Log("test");
            Time.timeScale = 0;
            timeCounter = 0;
            nextRound += 1;
            if (nextRound <= 7)
            {
                pauseOverlay.SetActive(true);
            }
            else
            {
                gameOver.SetActive(true);
            }

            if (nextRound == 3)
            {
                pipeline = true;
            }

            if (nextRound == 6)
            {
                vent = true;
            }
        }    
    }
}
