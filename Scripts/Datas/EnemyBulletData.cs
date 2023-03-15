using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Datas/EnemyBulletDatas", fileName = "EnemyBullet_")]
public class EnemyBulletData : ScriptableObject
{
    [SerializeField]
    public string enemyBulletName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public EnemyBulletType enemyBulletType;
    [SerializeField]
    public EnemyEffectType effectType;
    [SerializeField]
    public BulletLevel[] bulletLevels;
}
