using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretBullet : Bullet
{
    public TurretBulletType turretBulletType;
    public TurretEffectType effectType;

    private void Update()
    {
        if (target == null)
        {
            bulletPooler.ExpiredTurretBullet(gameObject);
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
    protected override void HitTarget()
    {
        if (effect == null)
        {
            effect = effectPooler.GetTurretEffect(effectType);
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
        bulletPooler.ExpiredTurretBullet(gameObject);
    }
    protected override void Explode()
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

    protected override void Damage(GameObject enemy)
    {
        EnemyStat enemyStat = enemy.GetComponent<EnemyStat>();
        if (enemyStat != null)
        {
            enemyStat.TakeDamege(damage);
        }
    }
}
