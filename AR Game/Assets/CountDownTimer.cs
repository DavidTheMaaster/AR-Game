using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float currentTime = 0.0f;
    float startingTime = 5.0f;

    public Text label;
    public Text labelShade;

    AudioSource countdown_audio;

    void Start()
    {
        countdown_audio = GetComponent<AudioSource>();
        currentTime = startingTime;
    }

    void Update()
    {
        if(currentTime == 5.0f)
        {
            countdown_audio.Play();
        }
        currentTime -= 1.0f * Time.deltaTime;

        if (currentTime > 4.0f)
        {
            label.gameObject.SetActive(true);
            labelShade.gameObject.SetActive(true);
            label.text = "3...";
        }
        else if (currentTime > 3.0f)
        {
            label.text = "2...";
        }
        else if (currentTime > 2.0f)
        {
            label.text = "1...";
        }
        else if (currentTime > 1.0f)
        {
            label.text = "GO!";
        }
        else if (currentTime > 0.0f)
        {
            label.gameObject.SetActive(false);
            labelShade.gameObject.SetActive(false);
        }

        labelShade.text = label.text;
    }
}
