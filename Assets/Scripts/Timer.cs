using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   float timer = 0f;
   [SerializeField] TextMeshProUGUI text;
   float timeElapsed;
    void Update()
    {
        timeElapsed += Time.deltaTime;
        text.text = timeElapsed.ToString();
        
        
    }
}
