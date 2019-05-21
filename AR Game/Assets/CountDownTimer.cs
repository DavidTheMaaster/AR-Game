using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0.0f;
    float startingTime = 5.0f;

    public Text label;
    public Text goLabel;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 0.5f * Time.deltaTime;

        if(currentTime > 4.0f)
        {
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
            label.gameObject.SetActive(false);
            goLabel.gameObject.SetActive(true);
        }
        else if (currentTime > 0.0f)
        {
            goLabel.gameObject.SetActive(false);
        }
    }
}
