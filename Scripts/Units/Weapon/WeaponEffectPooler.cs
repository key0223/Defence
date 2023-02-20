using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectPooler : MonoBehaviour
{
    Queue<GameObject>[] bulletEffectQueue = new Queue<GameObject>[(int)WeaponBulletType.WEAPONBULLET_MAX];
    [SerializeField]
    public GameObject arrowEffectPrefab;
    [SerializeField]
    public GameObject bulletEffetcPrefab;
    [SerializeField]
    int maxWeaponEffect = 10;

    private void Awake()
    {
        CreateWeaponBulletEffectPool();
    }
   


    public void CreateWeaponBulletEffectPool()
    {
        bulletEffectQueue[(int)WeaponBulletType.WEAPONBULLET_BULLET] = new Queue<GameObject>();
        bulletEffectQueue[(int)WeaponBulletType.WEAPONBULLET_ARROW]= new Queue<GameObject>();

        GameObject arrowPool = new GameObject { name = arrowEffectPrefab.name };
        arrowPool.transform.parent = transform;
        GameObject bulletPool = new GameObject { name=bulletEffetcPrefab.name };
        bulletPool.transform.parent=transform;
        for (int i = 0; i < maxWeaponEffect; i++)
        {
            GameObject arrowEffectGO = Instantiate(arrowEffectPrefab, arrowPool.transform);
            BulletEffect bulletEffect = arrowEffectGO.GetComponent<BulletEffect>();
            bulletEffect.weaponBulletType = WeaponBulletType.WEAPONBULLET_ARROW;

            arrowEffectGO.SetActive(false);
            bulletEffectQueue[(int)WeaponBulletType.WEAPONBULLET_ARROW].Enqueue(arrowEffectGO);
        }

        for (int i = 0; i < maxWeaponEffect; i++)
        {
            GameObject bulletEffectGO = Instantiate(bulletEffetcPrefab, bulletPool.transform);
            BulletEffect bulletEffect = bulletEffectGO.GetComponent<BulletEffect>();
            bulletEffect.weaponBulletType = WeaponBulletType.WEAPONBULLET_BULLET;

            bulletEffectGO.SetActive(false);
            bulletEffectQueue[(int)WeaponBulletType.WEAPONBULLET_BULLET].Enqueue(bulletEffectGO);
        }
    }

    public GameObject GetBulletEffect(WeaponBulletType weaponBulletType)
    {
        if (bulletEffectQueue[(int)weaponBulletType].Count>0)
        {
            return bulletEffectQueue[(int)weaponBulletType].Dequeue();
        }
        return null;
    }

    public void ExpiredBulletEffet(GameObject effectGo)
    {
        BulletEffect bulletEffect = effectGo.GetComponent<BulletEffect>();
        bulletEffectQueue[(int)bulletEffect.weaponBulletType].Enqueue(effectGo);
        effectGo.SetActive(false);
    }
}
