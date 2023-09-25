using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScopeScript : MonoBehaviour
{

    [SerializeField] float scope;
    float normalFov;
    [SerializeField] GameObject sniperScope;
    Camera mycamera;
    void Start()
    {
        mycamera = Camera.main;
        normalFov = mycamera.fieldOfView;
    }
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            mycamera.fieldOfView = scope;
            sniperScope.SetActive(true);
        }
        else
        {
            mycamera.fieldOfView = normalFov;
            sniperScope.SetActive(false);
        }

    }
}
