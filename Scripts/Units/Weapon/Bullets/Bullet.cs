using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemyPooler enemyPooler;
    WeaponBullet_Pooler weaponBullet_Pooler;
    WeaponEffectPooler weaponEffectPooler;

    public Transform firePoint;

    public WeaponBulletType weaponBulletType;

    private GameObject target;
    public float speed = 60f;
    public float explosionRadius = 0f;

    //public GameObject impactEffect;

    private void Awake()
    {
        enemyPooler= FindObjectOfType<EnemyPooler>();
        weaponBullet_Pooler = FindObjectOfType<WeaponBullet_Pooler>();
        weaponEffectPooler= FindObjectOfType<WeaponEffectPooler>();
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
        //GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        GameObject effectIns = weaponEffectPooler.GetBulletEffect(weaponBulletType);
        effectIns.transform.position = transform.position;
        effectIns.transform.rotation = transform.rotation;
        effectIns.SetActive(true);

        //Destroy(effectIns, 5f);

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
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                Damage(collider.gameObject);
            }
        }
    }

    void Damage(GameObject enemy)
    {
        enemyPooler.ExpiredEnemy(enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
