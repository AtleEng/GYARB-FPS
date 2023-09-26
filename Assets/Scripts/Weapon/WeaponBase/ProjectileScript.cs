using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileScript : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public float speed;

    [SerializeField] GameObject kaboomEffect;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity += transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(kaboomEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
