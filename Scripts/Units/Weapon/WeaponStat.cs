using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    Attack attack;

    public WeaponType weaponType;
    public WeaponBulletType weaponBulletType;
    public Transform firePoint;
    public float fireRate;

    private void Awake()
    {
        attack = GetComponent<Attack>();
        InitAttack();
    }

    void InitAttack()
    {
        attack.FirePonint = firePoint;
        attack.FireRate = fireRate;
        attack.WeaponBulletType = weaponBulletType;
        attack.FireRate = fireRate;
        
    }
}
