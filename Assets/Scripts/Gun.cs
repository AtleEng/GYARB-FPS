using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Stats
    [Header("Gun stats")]

    [Tooltip("Damage of each bullet")]
    [SerializeField] int damage = 10;

    [Tooltip("The time between attacks")]
    [SerializeField] float firerate = 0.1f;
    float timeBetweenShoots;

    [Tooltip("If you have more then 1 bullet per attack, this add timedelay between them")]
    [SerializeField] float burstSpeed;

    [Tooltip("Spread in degrees, first shoot is fully accuret")]
    [SerializeField] float bulletSpread;
    Vector3 bulletOffset;

    [Tooltip("Spread in degrees, first shoot is fully accurate")]
    [SerializeField] int amountOfBullets;

    [Tooltip("Time until the first shoot is accuret")]
    [SerializeField] float accuracyCooldown;
    float timeToCooldown;

    float range = 1000f;

    #endregion

    [Header("Components")]
    Camera cam;
    [SerializeField] Transform bulletSpawnPos;

    [Header("Effects")]
    [SerializeField] ParticleSystem gunflash;
    [SerializeField] ParticleSystem bulletImpact;
    [SerializeField] GunRecoil gunRecoil;

    [Header("BulletTrail")]
    [SerializeField] float timeActive = 1;
    [SerializeField] TrailRenderer bulletTrail;

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        timeBetweenShoots += Time.deltaTime;
        timeToCooldown += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeBetweenShoots >= firerate)
        {
            gunflash.Play();

            if (timeToCooldown >= accuracyCooldown)
            {
                bulletOffset = Vector3.zero;
                Debug.Log("No recoil");
            }
            else
            {
                bulletOffset = Random.insideUnitCircle * bulletSpread;
                Debug.Log("recoil");
            }
            timeToCooldown = 0;
            gunRecoil.Recoil();

            StartCoroutine(Shoot());
            timeBetweenShoots = 0;
        }
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < amountOfBullets; i++)
        {
            if (i >= 1) { bulletOffset = Random.insideUnitCircle * bulletSpread; }

            Vector3 raycastDirection = cam.transform.forward + bulletOffset;

            if (Physics.Raycast(cam.transform.position, raycastDirection, out RaycastHit hitinfo, range))
            {
                Debug.Log(hitinfo.transform.name);

                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPos.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hitinfo));

                Target target = hitinfo.transform.GetComponent<Target>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            yield return new WaitForSeconds(burstSpeed);
        }
    }
    //Handle bullet trails
    IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < timeActive)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;
        //Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.point));

        Destroy(trail.gameObject, trail.time);
    }
}
