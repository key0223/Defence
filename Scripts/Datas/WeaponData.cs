using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/WeaponDatas", fileName = "Weapon_")]

public class WeaponData : ScriptableObject
{
    [Header("General")]
    [SerializeField]
    public string weaponName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public string iconPath;

    [Header("Turret Info")]
    [SerializeField]
    public WeaponType weaponType;
    [SerializeField]
    public float range;
    [SerializeField]
    public float turnSpeed;

    [Header("BulletInfo")]
    [SerializeField]
    public WeaponBulletType weaponBulletType;
    [SerializeField]
    public EffectType effectType;
    [SerializeField]
    public float fireRate;
    [SerializeField]
    public bool useLaser;
}
