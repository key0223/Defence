using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretBulletType
{
    TURRETBULLET_ARROW,
    TURRETBULLET_BULLET,
    TURRETBULLET_LASER,
    TURRETBULLET_MAX,
}

public enum EnemyBulletType
{
    ENEMYBULLET_BULLET,
    ENEMYBULLET_MAX
}

public class BulletPooler : MonoBehaviour
{
    Queue<GameObject>[] turretBulletQueue = new Queue<GameObject>[(int)TurretBulletType.TURRETBULLET_MAX];
    Queue<GameObject>[] enemyBulletQueue = new Queue<GameObject>[(int)EnemyBulletType.ENEMYBULLET_MAX];

    [SerializeField]
    public WeaponBulletData[] turretBulletDatas;
    [SerializeField]
    public EnemyBulletData[] enemyBulletDatas;

    [SerializeField]
    int maxWeaponBullet = 10;
    [SerializeField]
    int maxEnemyBullet = 10;
    private void Awake()
    {
        CreateTurretBulletPool();
        CreatEnemyBulletPool();
    }

    public void CreateTurretBulletPool()
    {
        turretBulletQueue[(int)TurretBulletType.TURRETBULLET_ARROW] = new Queue<GameObject>();
        turretBulletQueue[(int)TurretBulletType.TURRETBULLET_BULLET] = new Queue<GameObject>();

        GameObject[] bulletGO = new GameObject[turretBulletQueue.Length];

        for (int i = 0; i < turretBulletDatas.Length; i++)
        {
            bulletGO[i] = new GameObject { name = turretBulletDatas[i].name };
            bulletGO[i].transform.parent = transform;

            for (int j = 0; j < maxWeaponBullet; j++)
            {
                GameObject weaponBulletGO = ResourceManager.Instance.Instantiate(turretBulletDatas[i].prefabPath, bulletGO[i].transform);

                TurretBullet bullet = weaponBulletGO.GetComponent<TurretBullet>();
                bullet.turretBulletType = turretBulletDatas[i].turretBulletType;
                bullet.effectType = turretBulletDatas[i].effectType;
                bullet.damage = turretBulletDatas[i].bulletLevels[i].damage;
                bullet.speed = turretBulletDatas[i].bulletLevels[i].speed;
                bullet.explosionRadius = turretBulletDatas[i].bulletLevels[i].explosionRadius;

                weaponBulletGO.gameObject.SetActive(false);
                turretBulletQueue[(int)turretBulletDatas[i].turretBulletType].Enqueue(weaponBulletGO);
            }
        }
    }

    public void CreatEnemyBulletPool()
    {
        enemyBulletQueue[(int)EnemyBulletType.ENEMYBULLET_BULLET] = new Queue<GameObject>();

        GameObject[] bulletGO = new GameObject[enemyBulletQueue.Length];

        for (int i = 0; i < enemyBulletDatas.Length; i++)
        {
            bulletGO[i] = new GameObject { name = enemyBulletDatas[i].name };
            bulletGO[i].transform.parent = transform;

            for (int j = 0; j < maxEnemyBullet; j++)
            {
                GameObject weaponBulletGO = ResourceManager.Instance.Instantiate(enemyBulletDatas[i].prefabPath, bulletGO[i].transform);

                EnemyBullet bullet = weaponBulletGO.GetComponent<EnemyBullet>();
                bullet.enemyBulletType = enemyBulletDatas[i].enemyBulletType;
                bullet.effectType = enemyBulletDatas[i].effectType;
                bullet.damage = enemyBulletDatas[i].bulletLevels[i].damage;
                bullet.speed = enemyBulletDatas[i].bulletLevels[i].speed;
                bullet.explosionRadius = enemyBulletDatas[i].bulletLevels[i].explosionRadius;

                weaponBulletGO.gameObject.SetActive(false);
                enemyBulletQueue[(int)enemyBulletDatas[i].enemyBulletType].Enqueue(weaponBulletGO);
            }
        }
    }

    public GameObject GetTurretBullet(TurretBulletType TurretBulletType)
    {

        if (turretBulletQueue[(int)TurretBulletType].Count>0)
        {
            return turretBulletQueue[(int)TurretBulletType].Dequeue();
        }
        return null;
    }

    public GameObject GetEnemyBullet(EnemyBulletType EnemyBulletType)
    {

        if (enemyBulletQueue[(int)EnemyBulletType].Count > 0)
        {
            return enemyBulletQueue[(int)EnemyBulletType].Dequeue();
        }
        return null;
    }

    public void ExpiredTurretBullet(GameObject bulletGo)
    {
        TurretBullet bullet = bulletGo.GetComponent<TurretBullet>();
        bulletGo.SetActive(false);
        turretBulletQueue[(int)bullet.turretBulletType].Enqueue(bulletGo);
    }
    public void ExpiredEnemyBullet(GameObject bulletGo)
    {
        EnemyBullet bullet = bulletGo.GetComponent<EnemyBullet>();
        bulletGo.SetActive(false);
        turretBulletQueue[(int)bullet.enemyBulletType].Enqueue(bulletGo);
    }
}
