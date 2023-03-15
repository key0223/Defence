using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/EnemyDatas", fileName ="Enemy_")]
public class EnemyData : ScriptableObject
{
    [Header("General")]
    [SerializeField]
    public string enemyName;
    [SerializeField]
    public string prefabPath;

    [Header("Enemy Info")]
    [SerializeField]
    public EnemyType enemyType;
    [SerializeField]
    public TurretEffectType deathEffect;
    [SerializeField]
    public float hp;
    [SerializeField]
    public float speed;


    [Header("ATK Info")]
    [SerializeField]
    public EnemyEffectType bulletEffect;
    [SerializeField]
    public EnemyBulletType bullettype;
    [SerializeField]
    public bool isAttackable;
    [SerializeField]
    public float range;
    [SerializeField]
    public float viewAngle;
    [SerializeField]
    public float turnSpeed;
    [SerializeField]
    public float fireRate;
    [SerializeField]
    public bool useLaser;
}
