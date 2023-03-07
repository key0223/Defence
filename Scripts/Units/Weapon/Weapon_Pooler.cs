using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    WEAPON_BALLISTA,
    WEAPON_BLASTER,
    WEAPON_LASER_BEAMER,
    WEAPON_CANNON,
    WEAPON_CATAPULT,
    WEAPON_MAX,
    WEAPON_NONE,
}
public class Weapon_Pooler : MonoBehaviour
{
    Queue<GameObject>[] weaponQueue = new Queue<GameObject>[(int)WeaponType.WEAPON_MAX];

    [SerializeField]
    public WeaponData[] weaponDatas;

    [SerializeField]
    int maxWeapon = 10;

    private void Awake()
    {
        CreatWeaponPool();
    }
    public void CreatWeaponPool()
    {
        weaponQueue[(int)WeaponType.WEAPON_BALLISTA] = new Queue<GameObject>();
        weaponQueue[(int)WeaponType.WEAPON_BLASTER] = new Queue<GameObject>();
        weaponQueue[(int)WeaponType.WEAPON_LASER_BEAMER] = new Queue<GameObject>();

        GameObject[] turretParentGO = new GameObject[weaponQueue.Length];

        for (int i = 0; i < weaponDatas.Length; i++)
        {
            turretParentGO[i] = new GameObject { name = weaponDatas[i].name };
            turretParentGO[i].transform.parent = transform;

            for (int j = 0; j < maxWeapon; j++)
            {
                GameObject newTurret = ResourceManager.Instance.Instantiate(weaponDatas[i].prefabPath, turretParentGO[i].transform);

                Turret turret = newTurret.GetComponent<Turret>();

                turret.weaponType= weaponDatas[i].weaponType;
                turret.range = weaponDatas[i].range;
                turret.turnSpeed= weaponDatas[i].turnSpeed;

                turret.weaponBulletType= weaponDatas[i].weaponBulletType;
                turret.effectType= weaponDatas[i].effectType;
                turret.fireRate= weaponDatas[i].fireRate;
                turret.useLaser= weaponDatas[i].useLaser;

                newTurret.SetActive(false);
                weaponQueue[(int)weaponDatas[i].weaponType].Enqueue(newTurret);
            }
        }
    }

    public GameObject GetWeapon(WeaponType weaponType)
    {
        if (weaponQueue[(int)weaponType].Count<= 0)
            return null;

        if(weaponQueue[(int)weaponType].Count > 0)
        {
            return weaponQueue[(int)weaponType].Dequeue();
        }
        return null;
    }
    public void ExpiredTurret(GameObject weaponGo)
    {
        Turret turret = weaponGo.GetComponent<Turret>();
        weaponQueue[(int)turret.weaponType].Enqueue(weaponGo);
        weaponGo.SetActive(false);
    }
}
