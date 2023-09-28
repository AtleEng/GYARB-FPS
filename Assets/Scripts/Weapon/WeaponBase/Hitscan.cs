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
    [SerializeField] float timeActive = 1;
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

            StartCoroutine(SpawnTrail(trail, bulletSpawnPos.position + shootingDir * range));
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
