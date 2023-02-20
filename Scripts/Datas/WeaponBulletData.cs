using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/WeaponBulletDatas", fileName = "WeaponBullet_")]
public class WeaponBulletData : ScriptableObject
{
    [SerializeField]
    public string weaponBulletName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public WeaponBulletType weaponBulletType;
    [SerializeField]
    public float speed;
    [SerializeField]
    public float explosionRadius;
}
