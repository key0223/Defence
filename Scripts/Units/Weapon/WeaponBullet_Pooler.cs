using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponBulletType
{
    WEAPONBULLET_ARROW,
    WEAPONBULLET_BULLET,
    WEAPONBULLET_LASER,
    WEAPONBULLET_MAX,
}
public class WeaponBullet_Pooler : MonoBehaviour
{
    Queue<GameObject>[] weaponBulletQueue = new Queue<GameObject>[(int)WeaponBulletType.WEAPONBULLET_MAX];

    [SerializeField]
    public WeaponBulletData[] weaponBulletDatas;
    [SerializeField]
    int maxWeaponBullet = 10;

    private void Awake()
    {
        CreatWeaponBulletPool();
    }

    public void CreatWeaponBulletPool()
    {
        weaponBulletQueue[(int)WeaponBulletType.WEAPONBULLET_ARROW] = new Queue<GameObject>();
        weaponBulletQueue[(int)WeaponBulletType.WEAPONBULLET_BULLET] = new Queue<GameObject>();

        GameObject[] bulletGO = new GameObject[weaponBulletQueue.Length];

        for (int i = 0; i < weaponBulletDatas.Length; i++)
        {
            bulletGO[i] = new GameObject { name = weaponBulletDatas[i].name };
            bulletGO[i].transform.parent = transform;

            for (int j = 0; j < maxWeaponBullet; j++)
            {
                GameObject weaponBulletGO = ResourceManager.Instance.Instantiate(weaponBulletDatas[i].prefabPath, bulletGO[i].transform);

                Bullet bullet = weaponBulletGO.GetComponent<Bullet>();
                bullet.weaponBulletType = weaponBulletDatas[i].weaponBulletType;
                bullet.effectType = weaponBulletDatas[i].effectType;
                bullet.damage = weaponBulletDatas[i].bulletLevels[i].damage;
                bullet.speed = weaponBulletDatas[i].bulletLevels[i].speed;
                bullet.explosionRadius = weaponBulletDatas[i].bulletLevels[i].explosionRadius;

                weaponBulletGO.gameObject.SetActive(false);
                weaponBulletQueue[(int)weaponBulletDatas[i].weaponBulletType].Enqueue(weaponBulletGO);
            }
        }
    }

    public GameObject GetWeaponBullet(WeaponBulletType weaponBulletType)
    {

        if (weaponBulletQueue[(int)weaponBulletType].Count>0)
        {
            return weaponBulletQueue[(int)weaponBulletType].Dequeue();
        }
        return null;
    }

    public void ExpiredBullet(GameObject bulletGo)
    {
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        weaponBulletQueue[(int)bullet.weaponBulletType].Enqueue(bulletGo);
        bulletGo.SetActive(false);
    }
}
