using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet :Bullet
{
    public EnemyBulletType enemyBulletType;
    public EnemyEffectType effectType;

    private void Update()
    {
        if (target == null)
        {
            bulletPooler.ExpiredEnemyBullet(gameObject);
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
            effect = effectPooler.GetEnemyEffect(effectType);
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
        bulletPooler.ExpiredEnemyBullet(gameObject);
    }

    protected override void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Turret"))
            {
                Damage(collider.gameObject);
            }
        }
    }
    protected override void Damage(GameObject _turret)
    {
        Debug.Log("Enemy Attack");
        
        Turret turret = _turret.GetComponent<Turret>();
        if(turret != null)
        {
            turret.TakeDamage(damage);
        }
    }
}
