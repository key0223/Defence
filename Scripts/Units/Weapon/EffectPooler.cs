using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretEffectType
{
    TURRET_EFFECT_BULLET,
    TURRET_EFFECT_BULLET_EXPLOSION,
    TURRET_EFFECT_BUILDTURRET,
    TURRET_EFFECT_SELL,
    TURRET_EFFECT_ENEMYDEATH,
    TURRET_EFFECT_MAX
}

public enum EnemyEffectType
{
    ENEMY_EFFECT_BULLET,
    ENEMY_EFFECT_MAX,
}
public class EffectPooler : MonoBehaviour
{
    Queue<GameObject>[] turretEffectQueue = new Queue<GameObject>[(int)TurretEffectType.TURRET_EFFECT_MAX];
    Queue<GameObject>[] enemyEffectQueue = new Queue<GameObject>[(int)EnemyEffectType.ENEMY_EFFECT_MAX];

    [Header("Turret")]

    [SerializeField]
    public GameObject arrowEffectPrefab;
    [SerializeField]
    public GameObject bulletEffetcPrefab;
    [SerializeField]
    public GameObject buildEffetcPrefab;
    [SerializeField]
    public GameObject sellEffetcPrefab;

    [Header("Enemy")]

    [SerializeField]
    public GameObject enemyBulletEffectPrefab;
    [SerializeField]
    public GameObject enemyDeathEffetcPrefab;
    
    [SerializeField]
    int maxTurretEffect = 10;
    [SerializeField]
    int maxEnemyEffect = 10;

    private void Awake()
    {
        TurretEffectQueue();
        EnemyEffectQueue();
    }

    


    public void TurretEffectQueue()
    {
        turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_BULLET] = new Queue<GameObject>();
        turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_BULLET_EXPLOSION] = new Queue<GameObject>();
        turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_BUILDTURRET] = new Queue<GameObject>();
        turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_ENEMYDEATH] = new Queue<GameObject>();
        turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_SELL] = new Queue<GameObject>();

        GameObject arrowPool = new GameObject { name = arrowEffectPrefab.name };
        arrowPool.transform.parent = transform;
        GameObject bulletPool = new GameObject { name = bulletEffetcPrefab.name };
        bulletPool.transform.parent = transform;
        GameObject buildEffectPool = new GameObject { name = buildEffetcPrefab.name };
        buildEffectPool.transform.parent = transform;
        GameObject enemyEffectPool = new GameObject { name = enemyDeathEffetcPrefab.name };
        enemyEffectPool.transform.parent = transform;
        GameObject sellEffectPool = new GameObject { name = sellEffetcPrefab.name };
        sellEffectPool.transform.parent = transform;

        for (int i = 0; i < maxTurretEffect; i++)
        {
            GameObject effectGO = Instantiate(arrowEffectPrefab, arrowPool.transform);
            effectGO.SetActive(false);
            turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_BULLET].Enqueue(effectGO);
        }

        for (int i = 0; i < maxTurretEffect; i++)
        {
            GameObject effectGO = Instantiate(bulletEffetcPrefab, bulletPool.transform);
            effectGO.SetActive(false);
            turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_BULLET_EXPLOSION].Enqueue(effectGO);
        }

        for (int i = 0; i < maxTurretEffect; i++)
        {
            GameObject effectGO = Instantiate(buildEffetcPrefab, buildEffectPool.transform);
            effectGO.SetActive(false);
            turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_BUILDTURRET].Enqueue(effectGO);
        }

        for (int i = 0; i < maxTurretEffect; i++)
        {
            GameObject effectGO = Instantiate(enemyDeathEffetcPrefab, enemyEffectPool.transform);
            effectGO.SetActive(false);
            turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_ENEMYDEATH].Enqueue(effectGO);
        }

        for (int i = 0; i < maxTurretEffect; i++)
        {
            GameObject effectGO = Instantiate(sellEffetcPrefab, sellEffectPool.transform);
            effectGO.SetActive(false);
            turretEffectQueue[(int)TurretEffectType.TURRET_EFFECT_SELL].Enqueue(effectGO);
        }
    }
    public void EnemyEffectQueue()
    {
        enemyEffectQueue[(int)EnemyEffectType.ENEMY_EFFECT_BULLET] = new Queue<GameObject>();
        GameObject bulletPool = new GameObject { name = enemyBulletEffectPrefab.name };
        bulletPool.transform.parent = transform;

        for (int i = 0; i < maxEnemyEffect; i++)
        {
            GameObject effectGO = Instantiate(arrowEffectPrefab, bulletPool.transform);
            effectGO.SetActive(false);
            enemyEffectQueue[(int)EnemyEffectType.ENEMY_EFFECT_BULLET].Enqueue(effectGO);
        }
    }

    public GameObject GetTurretEffect(TurretEffectType effectType)
    {
        if (turretEffectQueue[(int)effectType].Count>0)
        {
            return turretEffectQueue[(int)effectType].Dequeue();
        }
        return null;
    }
    public GameObject GetEnemyEffect(EnemyEffectType enemyEffectType)
    {
        if (enemyEffectQueue[(int)enemyEffectType].Count>0)
        {
            return enemyEffectQueue[(int)enemyEffectType].Dequeue();
        }
        return null;
    }

    public void ExpiredTurretEffect(GameObject effectGo)
    {
        Effect Effect = effectGo.GetComponent<Effect>();
        turretEffectQueue[(int)Effect.effectType].Enqueue(effectGo);
        effectGo.SetActive(false);
    }

    public void ExpiredEnemyEffect(GameObject effectGo)
    {
        Effect Effect = effectGo.GetComponent<Effect>();
        enemyEffectQueue[(int)Effect.effectType].Enqueue(effectGo);
        effectGo.SetActive(false);
    }
}
