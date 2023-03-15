using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretType
{
    TURRET_BALLISTA,
    TURRET_BLASTER,
    TURRET_LASER_BEAMER,
    TURRET_CANNON,
    TURRET_CATAPULT,
    TURRET_MAX,
    TURRET_NONE,
}
public class TurretPooler : MonoBehaviour
{
    Queue<GameObject>[] turretQueue = new Queue<GameObject>[(int)TurretType.TURRET_MAX];

    [SerializeField]
    public TurretData[] turretDatas;

    [SerializeField]
    int maxWeapon = 10;

    private void Awake()
    {
        CreatWeaponPool();
    }
    public void CreatWeaponPool()
    {
        turretQueue[(int)TurretType.TURRET_BALLISTA] = new Queue<GameObject>();
        turretQueue[(int)TurretType.TURRET_BLASTER] = new Queue<GameObject>();
        turretQueue[(int)TurretType.TURRET_LASER_BEAMER] = new Queue<GameObject>();

        GameObject[] turretParentGO = new GameObject[turretQueue.Length];

        for (int i = 0; i < turretDatas.Length; i++)
        {
            turretParentGO[i] = new GameObject { name = turretDatas[i].name };
            turretParentGO[i].transform.parent = transform;

            for (int j = 0; j < maxWeapon; j++)
            {
                GameObject newTurret = ResourceManager.Instance.Instantiate(turretDatas[i].prefabPath, turretParentGO[i].transform);

                Turret turret = newTurret.GetComponent<Turret>();

                turret.turretType= turretDatas[i].turretType;
                turret.range = turretDatas[i].range;
                turret.turnSpeed= turretDatas[i].turnSpeed;

                turret.turretBulletType= turretDatas[i].turretBulletType;
                turret.effectType= turretDatas[i].effectType;
                turret.fireRate= turretDatas[i].fireRate;
                turret.useLaser= turretDatas[i].useLaser;

                newTurret.SetActive(false);
                turretQueue[(int)turretDatas[i].turretType].Enqueue(newTurret);
            }
        }
    }

    public GameObject GetTurret(TurretType turretType)
    {
        if (turretQueue[(int)turretType].Count<= 0)
            return null;

        if(turretQueue[(int)turretType].Count > 0)
        {
            return turretQueue[(int)turretType].Dequeue();
        }
        return null;
    }
    public void ExpiredTurret(GameObject turretGo)
    {
        Turret turret = turretGo.GetComponent<Turret>();
        turretQueue[(int)turret.turretType].Enqueue(turretGo);
        turretGo.SetActive(false);
    }
}
