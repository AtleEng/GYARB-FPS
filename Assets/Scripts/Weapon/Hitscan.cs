using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour, IWeaponType
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 1;
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
        if (!gameManager.isPaused) { gameManager.shootsFired++; }

        if (Physics.Raycast(cam.transform.position, shootingDir, out RaycastHit hitinfo, range))
        {
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hitinfo.point, trailSpeed));

            Target target = hitinfo.transform.GetComponent<Target>();
            //Hit a target
            if (target != null)
            {
                if (!gameManager.isPaused) { gameManager.hitShoots++; }

                target.TakeDamage(damage);
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
        Vector3 direction = (endPos - startPos).normalized;

        while ((endPos - trail.transform.position).magnitude > 0.01f)
        {
            trail.transform.position = Vector3.MoveTowards(trail.transform.position, endPos, Time.deltaTime * constantSpeed);
            yield return null;
        }

        trail.transform.position = endPos;

        Destroy(trail.gameObject, trail.time);
    }
}
