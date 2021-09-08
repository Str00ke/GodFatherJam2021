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

    void Start()
    {
        scaleChange = new Vector3(0.0055f, 0.0055f, 0.0055f);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
        }

        DisplayTime(timeRemaining);

        if (timeRemaining < 11f && timeRemaining != 0)
        {
            timer.transform.localScale -= scaleChange;

            if (timer.transform.localScale.y < 0.85f || timer.transform.localScale.y > 1.0f)
            {
                scaleChange = -scaleChange;
            }
        }

        //Clignotement non implémenté pour cause d'explosion
        if (timeRemaining < 4f && timeRemaining != 0)
        {
            StartCoroutine(TimerBlink());
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Clignotement non implémenté pour cause d'explosion
    IEnumerator TimerBlink()
    {
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.black;

    }
}
