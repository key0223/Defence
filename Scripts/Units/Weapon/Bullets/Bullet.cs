using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemyPooler enemyPooler;
    WeaponBullet_Pooler weaponBullet_Pooler;
    EffectPooler effectPooler;

    public Transform firePoint;

    public WeaponBulletType weaponBulletType;
    public EffectType effectType;

    private GameObject target;
    private GameObject effect;
    public float damage;
    public float speed;
    public float explosionRadius;

    private void Awake()
    {
        enemyPooler = FindObjectOfType<EnemyPooler>();
        weaponBullet_Pooler = FindObjectOfType<WeaponBullet_Pooler>();
        effectPooler = FindObjectOfType<EffectPooler>();
    }
    public void Seek(GameObject _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            weaponBullet_Pooler.ExpiredBullet(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target.transform);
    }


    void HitTarget()
    {
        if(effect== null)
        {
            effect = effectPooler.GetEffect(effectType);
            effect.SetActive(true);
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
        }

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        weaponBullet_Pooler.ExpiredBullet(gameObject);

    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.gameObject);
            }
        }
    }

    void Damage(GameObject enemy)
    {
        EnemyStat enemyStat = enemy.GetComponent<EnemyStat>();
        if (enemyStat != null)
        {
            enemyStat.TakeDamege(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
