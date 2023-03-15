using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //EnemyPooler enemyPooler;
    protected BulletPooler bulletPooler;
    protected EffectPooler effectPooler;

    public Transform firePoint;

   

    protected GameObject target;
    protected GameObject effect;

    public float damage;
    public float speed;
    public float explosionRadius;

    protected float bulletTime = 3f;

    

    protected void Awake()
    {
        //enemyPooler = FindObjectOfType<EnemyPooler>();
        bulletPooler = FindObjectOfType<BulletPooler>();
        effectPooler = FindObjectOfType<EffectPooler>();
    }

    /*
    protected void OnEnable()
    {
        StartCoroutine(bulletTimeOver());
    }

    protected IEnumerator bulletTimeOver()
    {
        float timer =+ Time.deltaTime;

        if(timer>= bulletTime)
        {
            bulletPooler.ExpiredTurretBullet(gameObject);
        }
        yield return null;
    }
    */
    public void Seek(GameObject _target)
    {
        target = _target;
    }

    /*
    protected void Update()
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
    */

    protected abstract void HitTarget();

    protected abstract void Explode();

    protected abstract void Damage(GameObject enemy);


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
