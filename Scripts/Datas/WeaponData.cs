using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/WeaponDatas", fileName = "Weapon_")]

public class WeaponData : ScriptableObject
{
    [SerializeField]
    public string weaponName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public string iconPath;
    [SerializeField]
    public WeaponType weaponType;
    [SerializeField]
    public WeaponBulletData weaponBulletData;
    [SerializeField]
    public float fireRate;

}
