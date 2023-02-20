using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    WeaponBullet_Pooler weaponBullet_Pooler;
    public GameObject target { get; set; }

    private Transform firePoint;
    public Transform FirePonint
    {
        get { return firePoint; }
        set { firePoint = value; }
    }

    private float fireRate;
    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    private WeaponBulletType weaponBulletType;
    public WeaponBulletType WeaponBulletType
    {
        get { return weaponBulletType; }
        set { weaponBulletType= value; }
    }
    private float fireCountdown = 0f;

    private void Awake()
    {
        weaponBullet_Pooler=FindObjectOfType<WeaponBullet_Pooler>(); 
    }

    void Update()
    {
        if (target == null)
            return;

        if(fireCountdown <=0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Debug.Log("shoot");
        GameObject bulletGO = weaponBullet_Pooler.GetWeaponBullet(weaponBulletType);
        bulletGO.SetActive(true);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }
}
