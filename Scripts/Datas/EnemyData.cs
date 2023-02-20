using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/EnemyDatas", fileName ="Enemy_")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    public string enemyName;
    [SerializeField]
    public string prefabPath;
    [SerializeField]
    public EnemyType enemyType;
    [SerializeField]
    public float hp;
    [SerializeField]
    public float speed;

}
