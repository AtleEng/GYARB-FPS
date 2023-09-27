using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour, IWeaponType
{
    [SerializeField] Transform bulletSpawnPos;

    [Header("Projectile varibles")]
    [SerializeField] GameObject projectile;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float radius;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    [System.Obsolete]
    public void Attack(Vector3 shootingDir)
    {
        GameObject g = Instantiate(projectile, bulletSpawnPos.position, Quaternion.identity);
        g.transform.forward = shootingDir;

        ProjectileScript pS = g.GetComponent<ProjectileScript>();

        pS.damage = damage;
        pS.speed = speed;
        pS.radius = radius;

        pS.StartProjectile();
    }

}
