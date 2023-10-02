using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour, IWeaponType
{
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 10;
    [SerializeField] Transform bulletSpawnPos;
    Camera cam;

    [Header("BulletTrail")]
    [SerializeField] float trailSpeed = 1;
    [SerializeField] TrailRenderer bulletTrail;
    void Start()
    {
        cam = Camera.main;
    }

    public void Attack(Vector3 shootingDir)
    {
        if (Physics.Raycast(cam.transform.position, shootingDir, out RaycastHit hitinfo, range))
        {
            Debug.Log(hitinfo.transform.name);

            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hitinfo.point, trailSpeed));

            HitPoint hitpoint = hitinfo.transform.GetComponent<HitPoint>();
            //Hit a target
            if (hitpoint != null)
            {
                hitpoint.OnHit(damage);
            }
        }
        else
        {
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, bulletSpawnPos.position + shootingDir * range, trailSpeed));
        }
    }

    //Handle bullet trails
    IEnumerator SpawnTrail(TrailRenderer trail, Vector3 endPos, float constantSpeed)
    {
        Vector3 startPos = trail.transform.position;
        Vector3 direction = (endPos - startPos).normalized; // Calculate the direction vector.

        while (Vector3.Distance(trail.transform.position, endPos) > 0.01f)
        {
            float distanceThisFrame = constantSpeed * Time.deltaTime;
            trail.transform.position += direction * distanceThisFrame;

            yield return null;
        }

        trail.transform.position = endPos;

        Destroy(trail.gameObject, trail.time);
    }
}
