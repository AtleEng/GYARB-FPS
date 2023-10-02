using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Grappling : MonoBehaviour, IWeaponType
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject firepoint;
    private LineRenderer LineRender;
    Vector3 Gpoint;
    Camera cam;
    [SerializeField] LayerMask WhatG;
    private SpringJoint joint;
    float maxDistance = 500f;

    bool hasStartedGrappling = false;
    void Start()
    {
        cam = Camera.main;

    }

    public void Attack(Vector3 shootingDir)
    {
        if (hasStartedGrappling == false)
        {
            Grapplestart(shootingDir);
            hasStartedGrappling = true;
        }

    }

    void Grapplestart(Vector3 shottingDir)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, shottingDir, out hit, maxDistance, WhatG)) ;
        Gpoint = hit.point;
        joint = player.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = Gpoint;

        float distanceFromPoint = Vector3.Distance(player.transform.position, Gpoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;
    }

    void DrawGrapple()
     { 
       LineRender.SetPosition(0,firepoint.transform.position);

     }
}
