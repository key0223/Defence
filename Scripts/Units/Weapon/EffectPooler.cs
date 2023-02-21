using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    EFFECT_BULLET,
    EFFECT_BULLET_EXPLOSION,
    EFFECT_BUILDTURRET,
    EFFECT_ENEMY,
    EFFECT_MAX
}
public class EffectPooler : MonoBehaviour
{
    Queue<GameObject>[] effectQueue = new Queue<GameObject>[(int)EffectType.EFFECT_MAX];
    [SerializeField]
    public GameObject arrowEffectPrefab;
    [SerializeField]
    public GameObject bulletEffetcPrefab;
    [SerializeField]
    public GameObject buildEffetcPrefab;
    [SerializeField]
    public GameObject enemyEffetcPrefab;
    [SerializeField]
    int maxWeaponEffect = 10;
    
    

    private void Awake()
    {
        CreateEffectPool();
    }
   


    public void CreateEffectPool()
    {
        effectQueue[(int)EffectType.EFFECT_BULLET] = new Queue<GameObject>();
        effectQueue[(int)EffectType.EFFECT_BULLET_EXPLOSION]= new Queue<GameObject>();
        effectQueue[(int)EffectType.EFFECT_BUILDTURRET] = new Queue<GameObject>();
        effectQueue[(int)EffectType.EFFECT_ENEMY] = new Queue<GameObject>();

        GameObject arrowPool = new GameObject { name = arrowEffectPrefab.name };
        arrowPool.transform.parent = transform;
        GameObject bulletPool = new GameObject { name=bulletEffetcPrefab.name };
        bulletPool.transform.parent=transform;
        GameObject buildEffectPool = new GameObject { name = buildEffetcPrefab.name };
        buildEffectPool.transform.parent = transform;
        GameObject enemytEffectPool = new GameObject { name = enemyEffetcPrefab.name };
        enemytEffectPool.transform.parent = transform;

        for (int i = 0; i < maxWeaponEffect; i++)
        {
            GameObject effectGO = Instantiate(arrowEffectPrefab, arrowPool.transform);
            effectGO.SetActive(false);
            effectQueue[(int)EffectType.EFFECT_BULLET].Enqueue(effectGO);
        }

        for (int i = 0; i < maxWeaponEffect; i++)
        {
            GameObject effectGO = Instantiate(bulletEffetcPrefab, bulletPool.transform);
            effectGO.SetActive(false);
            effectQueue[(int)EffectType.EFFECT_BULLET_EXPLOSION].Enqueue(effectGO);
        }

        for (int i = 0; i < maxWeaponEffect; i++)
        {
            GameObject effectGO = Instantiate(buildEffetcPrefab, buildEffectPool.transform);
            effectGO.SetActive(false);
            effectQueue[(int)EffectType.EFFECT_BUILDTURRET].Enqueue(effectGO);
        }

        for (int i = 0; i < maxWeaponEffect; i++)
        {
            GameObject effectGO = Instantiate(enemyEffetcPrefab, enemytEffectPool.transform);
            effectGO.SetActive(false);
            effectQueue[(int)EffectType.EFFECT_ENEMY].Enqueue(effectGO);
        }
    }

    public GameObject GetEffect(EffectType effectType)
    {
        if (effectQueue[(int)effectType].Count>0)
        {
            return effectQueue[(int)effectType].Dequeue();
        }
        return null;
    }

    public void ExpiredEffect(GameObject effectGo)
    {
        Effect Effect = effectGo.GetComponent<Effect>();
        effectQueue[(int)Effect.effectType].Enqueue(effectGo);
        effectGo.SetActive(false);
    }
}
