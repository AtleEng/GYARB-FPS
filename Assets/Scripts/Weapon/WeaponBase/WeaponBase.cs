using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(IWeaponType))]
public class WeaponBase : MonoBehaviour
{
    #region Stats
    [Header("Weapon base stats")]
    IWeaponType weaponType;
    [SerializeField] float firerate = 0.1f;
    float timeBetweenShoots;
    [SerializeField] float burstSpeed;
    [SerializeField] float bulletSpread;
    Vector3 bulletOffset;
    [SerializeField] int amountOfBullets;
    [SerializeField] float accuracyCooldown;
    float timeToCooldown;

    #endregion
    #region varibles
    [Header("Components")]
    Camera cam;

    [Header("Effects")]
    [SerializeField] ParticleSystem gunflash;
    [SerializeField] AudioSource gunSound;
    [SerializeField] ParticleSystem bulletImpact;
    [SerializeField] GunRecoil gunRecoil;

    #endregion
    void Start()
    {
        cam = Camera.main;
        weaponType = gameObject.GetComponent<IWeaponType>();
    }
    void Update()
    {
        timeBetweenShoots += Time.deltaTime;
        timeToCooldown += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeBetweenShoots >= firerate)
        {
            gunRecoil.Recoil();

            StartCoroutine(Shoot());
            timeBetweenShoots = 0;
            timeToCooldown = 0;
        }
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < amountOfBullets; i++)
        {
            if (timeToCooldown >= accuracyCooldown)
            {
                bulletOffset = Vector3.zero;
                timeToCooldown = 0;
            }
            else
            {
                bulletOffset = Random.insideUnitCircle * bulletSpread;
            }

            if (burstSpeed > 0 || i == 1)
            {
                gunflash.Play();
                gunSound.Play();
            }

            Vector3 shootingDirection = cam.transform.forward + bulletOffset;
            weaponType.Attack(shootingDirection);

            yield return new WaitForSeconds(burstSpeed);
        }
    }
}

public interface IWeaponType
{
    void Attack(Vector3 shootingDir);
}
