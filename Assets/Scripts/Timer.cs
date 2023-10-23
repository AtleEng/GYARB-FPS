using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] TextMeshProUGUI text;
    float timeElapsed;

    public bool isPaused;
    void Update()
    {
        if (!isPaused)
        {
            timeElapsed += Time.deltaTime;
            string s = timeElapsed.ToString().Substring(0, 4);

            text.text = "Time: " + s;
        }

    }
}