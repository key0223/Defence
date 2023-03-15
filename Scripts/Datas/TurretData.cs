using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/TurretDatas", fileName = "Turret_")]

public class TurretData : ScriptableObject
{
    [Header("General")]
    [SerializeField]
    public string turretName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public string iconPath;

    [Header("Turret Info")]
    [SerializeField]
    public TurretType turretType;
    [SerializeField]
    public float range;
    [SerializeField]
    public float turnSpeed;

    [Header("BulletInfo")]
    [SerializeField]
    public TurretBulletType turretBulletType;
    [SerializeField]
    public TurretEffectType effectType;
    [SerializeField]
    public float fireRate;
    [SerializeField]
    public bool useLaser;
}
