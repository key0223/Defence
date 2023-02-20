using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    WEAPON_BALLISTA,
    WEAPON_BLASTER,
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

        GameObject[] weaponGO = new GameObject[weaponQueue.Length];

        for (int i = 0; i < weaponDatas.Length; i++)
        {
            weaponGO[i] = new GameObject { name = weaponDatas[i].name };
            weaponGO[i].transform.parent = transform;

            for (int j = 0; j < maxWeapon; j++)
            {
                GameObject newWeapon = ResourceManager.Instance.Instantiate(weaponDatas[i].prefabPath, weaponGO[i].transform);

                WeaponStat weaponStat = newWeapon.GetComponent<WeaponStat>();
                weaponStat.weaponType = weaponDatas[i].weaponType;
                weaponStat.weaponBulletType = weaponDatas[i].weaponBulletData.weaponBulletType;
                weaponStat.fireRate= weaponDatas[i].fireRate;
                newWeapon.gameObject.SetActive(false);
                weaponQueue[(int)weaponDatas[i].weaponType].Enqueue(newWeapon);
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
}
