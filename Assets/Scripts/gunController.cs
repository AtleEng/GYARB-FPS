using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float firerate = 0.1f;
    float timeBetweenShoots;
    float range = 1000f;

    [SerializeField] Camera cam;
    [SerializeField] Transform bulletSpawnPos;

    [SerializeField] ParticleSystem gunflash;
    [SerializeField] ParticleSystem bulletImpact;
    [SerializeField] Animator animator;

    [SerializeField] TrailRenderer bulletTrail;
    void Update()
    {
        timeBetweenShoots += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timeBetweenShoots >= firerate)
        {
            animator.SetTrigger("ShootsFired");
            gunflash.Play();

            ShootLogic();
            timeBetweenShoots = 0;
        }
    }

    void ShootLogic()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitinfo, range))
        {
            Debug.Log(hitinfo.transform.name);

            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hitinfo));

            TargetComponent target = hitinfo.transform.GetComponent<TargetComponent>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
            
        }
    }
    //Handle bullet trails
    IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;
        Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.point));

        Destroy(trail.gameObject, trail.time);
    }
}
