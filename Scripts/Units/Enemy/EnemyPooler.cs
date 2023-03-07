using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    ENEMY_UFO_GREEN,
    ENEMY_UFO_PURPLE,
    ENEMY_UFO_RED,
    ENEMY_MAX,
}
public class EnemyPooler : MonoBehaviour
{
    Queue<GameObject>[] enemyQueue = new Queue<GameObject>[(int)EnemyType.ENEMY_MAX];

    [SerializeField]
    EnemyData[] enemyDatas;

    [SerializeField]
    int maxEnemy = 30;

    private void Awake()
    {
        CreateEnemyPool();
    }
    public void CreateEnemyPool()
    {
        enemyQueue[(int)EnemyType.ENEMY_UFO_GREEN] = new Queue<GameObject>();
        enemyQueue[(int)EnemyType.ENEMY_UFO_PURPLE] = new Queue<GameObject>();
        enemyQueue[(int)EnemyType.ENEMY_UFO_RED] = new Queue<GameObject>();

        GameObject[] go = new GameObject[enemyQueue.Length];

        for (int i = 0; i < enemyDatas.Length; i++)
        {
            go[i] = new GameObject { name = enemyDatas[i].name };
            go[i].transform.parent = transform;

            for (int j = 0; j < maxEnemy; j++)
            {
                GameObject enemyGo = ResourceManager.Instance.InstantiateEnemy(enemyDatas[i].prefabPath, go[i].transform);

                EnemyStat enemyStat = enemyGo.GetComponent<EnemyStat>();
                enemyStat.enemyType = enemyDatas[i].enemyType;
                enemyStat.effectType = enemyDatas[i].effectType;
                enemyStat.startHp = enemyDatas[i].hp;
                enemyStat.startSpeed = enemyDatas[i].speed;

                enemyGo.gameObject.SetActive(false);
                enemyQueue[(int)enemyDatas[i].enemyType].Enqueue(enemyGo);
            }
        }
    }

    public GameObject GetEnemy(EnemyType enemyType)
    {
        if (enemyQueue[(int)enemyType].Count > 0)
        {
            return enemyQueue[(int)enemyType].Dequeue();
        }
        return null;
    }

    public void ExpiredEnemy(GameObject enemyGo)
    {
        EnemyStat enemyStat = enemyGo.GetComponent<EnemyStat>();
        enemyQueue[(int)enemyStat.enemyType].Enqueue(enemyGo);
        enemyGo.SetActive(false);
    }
}
