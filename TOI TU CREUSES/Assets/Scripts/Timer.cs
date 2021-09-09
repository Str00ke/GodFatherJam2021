using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 180f;
    public Text timerText;
    public GameObject timer;
    private new Vector3 scaleChange;

    private float minutes;
    private float seconds;

    void Start()
    {
        scaleChange = new Vector3(0.0041666666666667f, 0.0041666666666667f, 0.0041666666666667f);
    }

    void Update()
    {
        timeRemaining += Time.deltaTime;

        DisplayTime(timeRemaining);

        if (seconds == 30 || seconds == 00)
        {
            timer.transform.localScale += scaleChange;

            if (timer.transform.localScale.y < 1.0f || timer.transform.localScale.y > 1.25f)
            {
                scaleChange = -scaleChange;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
