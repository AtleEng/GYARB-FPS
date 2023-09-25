using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Stats
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
    [SerializeField] float range = 100f;
    #endregion
    #region varibles
    [Header("Components")]
    Camera cam;
    [SerializeField] Transform bulletSpawnPos;

    [Header("Effects")]
    [SerializeField] ParticleSystem gunflash;
    [SerializeField] ParticleSystem bulletImpact;
    [SerializeField] GunRecoil gunRecoil;

    [Header("BulletTrail")]
    [SerializeField] AudioSource gunSound;

    [Header("BulletTrail")]
    [SerializeField] float timeActive = 1;
    [SerializeField] TrailRenderer bulletTrail;
    #endregion
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
            gunSound.Play();

            if (timeToCooldown >= accuracyCooldown)
            {
                bulletOffset = Vector3.zero;
            }
            else
            {
                bulletOffset = Random.insideUnitCircle * bulletSpread;
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

                StartCoroutine(SpawnTrail(trail, hitinfo.point));

                Target target = hitinfo.transform.GetComponent<Target>();
                //Hit a target
                if (target != null)
                {
                    target.TakeDamage(damage);
                    
                }
            }
            else
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPos.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, bulletSpawnPos.position + raycastDirection * range));
            }
            yield return new WaitForSeconds(burstSpeed);
        }
    }
    //Handle bullet trails
    IEnumerator SpawnTrail(TrailRenderer trail, Vector3 endPos)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < timeActive)
        {
            trail.transform.position = Vector3.Lerp(startPos, endPos, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = endPos;

        Destroy(trail.gameObject, trail.time);
    }
}
