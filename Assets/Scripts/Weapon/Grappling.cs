using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Grappling : MonoBehaviour, IWeaponType
{
    [SerializeField] GameObject player;

    [SerializeField] Transform firepoint;
    LineRenderer lineRender;
    Vector3 hitPoint;
    Camera cam;
    [SerializeField] LayerMask WhatG;
    private SpringJoint joint;
    float maxDistance = 500f;
    bool hasStartedGrappling = false;
    void Start()
    {
        cam = Camera.main;
        lineRender = GetComponent<LineRenderer>();

    }


    public void Attack(Vector3 shootingDir)
    {
        Grapplestart(shootingDir);
        hasStartedGrappling = true;
    }
    void Update()
    {
        Drawrope();
    }

    void Grapplestart(Vector3 shottingDir)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, shottingDir, out hit, maxDistance, WhatG)) ;
        hitPoint = hit.point;
        joint = player.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = hitPoint;

        float distanceFromPoint = Vector3.Distance(player.transform.position, hitPoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lineRender.positionCount = 2;
    }


    void Drawrope()
    {
        if (!joint) return;
        lineRender.SetPosition(0, firepoint.position);
        lineRender.SetPosition(1, hitPoint);

    }

    void GrappleStop()
    {
        Destroy(joint);
    }
<<<<<<< HEAD
}
=======

    }
>>>>>>> e63dbd93afd2c82094539a57cde94ee3da100af8
