using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun stats")]
    [SerializeField] int damage = 10;
    [SerializeField] float firerate = 0.1f;
    float timeBetweenShoots;
    [SerializeField] float burstSpeed;
    [SerializeField] float bulletSpread;
    Vector3 bulletOffset;
    [SerializeField] int amountOfBullets;
    [SerializeField] float accuracyCooldown;
    float timeToCooldown;

    float range = 1000f;

    [Header("Components")]
    [SerializeField] Camera cam;
    [SerializeField] Transform bulletSpawnPos;

    [Header("Effects")]
    [SerializeField] ParticleSystem gunflash;
    [SerializeField] ParticleSystem bulletImpact;
    [SerializeField] GunRecoil gunRecoil;

    [Header("BulletTrail")]
    [SerializeField] float timeActive = 1;
    [SerializeField] TrailRenderer bulletTrail;

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
