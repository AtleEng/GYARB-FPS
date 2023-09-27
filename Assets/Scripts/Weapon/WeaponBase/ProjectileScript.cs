using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileScript : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float radius;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float force;

    [SerializeField] GameObject kaboomEffect;

    Rigidbody rb;

    public void StartProjectile()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnExplode();

        Instantiate(kaboomEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnExplode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.drag = 0;
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

    }
}
