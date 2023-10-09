using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(IWeaponType))]
public class WeaponBase : MonoBehaviour
{
    #region Stats
    [Header("Weapon base stats")]
    [SerializeField] GameObject weapon;
    IWeaponType weaponType;
    [SerializeField] bool isAtomatic = true;
    [SerializeField] float firerate = 0.1f;
    float timeBetweenShoots;

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
        weaponType = weapon.GetComponent<IWeaponType>();
    }
    void Update()
    {
        timeBetweenShoots += Time.deltaTime;
        timeToCooldown += Time.deltaTime;

        if (timeBetweenShoots >= firerate)
        {
            if (Input.GetButton("Fire1") && isAtomatic)
            {
                Shoot();
                timeBetweenShoots = 0;
                timeToCooldown = 0;
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                timeBetweenShoots = 0;
                timeToCooldown = 0;
            }
        }
    }
    void Shoot()
    {
        gunRecoil.Recoil();
        gunflash.Play();
        gunSound.Play();
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
            Vector3 shootingDirection = cam.transform.forward + bulletOffset;
            weaponType.Attack(shootingDirection);
        }
    }
}

public interface IWeaponType
{
    void Attack(Vector3 shootingDir);
}
