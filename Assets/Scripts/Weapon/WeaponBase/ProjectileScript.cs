using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public float speed;
    public float radius;

    public float timeToDestroy;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float force;

    [SerializeField] GameObject kaboomEffect;

    Rigidbody rb;

    public void StartProjectile()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, timeToDestroy);
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
            if (collider.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.drag = 0;
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
