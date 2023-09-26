using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public float speed;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity += transform.forward * speed;
    }
}
