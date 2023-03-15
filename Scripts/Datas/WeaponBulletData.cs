using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletLevel
{
    [SerializeField]
    public float damage;
    [SerializeField]
    public float speed;
    [SerializeField]
    public float explosionRadius;
}

[CreateAssetMenu(menuName = "Datas/WeaponBulletDatas", fileName = "WeaponBullet_")]
public class WeaponBulletData : ScriptableObject
{
    [SerializeField]
    public string weaponBulletName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public TurretBulletType turretBulletType;
    [SerializeField]
    public TurretEffectType effectType;
    [SerializeField]
    public BulletLevel[] bulletLevels;
}
