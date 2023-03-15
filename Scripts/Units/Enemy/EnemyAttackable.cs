using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackable : MonoBehaviour
{
    BulletPooler bulletPooler;
    

    [SerializeField]
    private GameObject target;

    [Header("Enemy ATK Info")]
    public float range;
    [Range(0, 360)]
    public float viewAngle;
    [SerializeField]
    private Transform partToRotate;
    public float turnSpeed;
    public int upgradedCount = 0;

    [Header("Bullet Info")]
    public EnemyEffectType effectType;
    public EnemyBulletType bulletType;
    //public float damage;
    //public float explosionRadius;
    [SerializeField]
    private Transform firePoint;
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Laser")]
    public bool useLaser;
    public float damageOverTime = 30f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [SerializeField]
    private string targetTag = "Turret";


    
    private void Awake()
    {
        bulletPooler = FindObjectOfType<BulletPooler>();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void UpdateTarget()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTurret = null;

        foreach (GameObject turret in turrets)
        {
            Vector3 dirToTarget = (turret.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, turret.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestTurret = turret;
                }
            }
        }

        if (nearestTurret != null && shortestDistance <= range)
        {
            target = nearestTurret;
            //targetStat = nearestEnemy.GetComponent<EnemyStat>();
        }
        else
        {
            target = null;
        }
    }

    protected void Shoot()
    {
        GameObject bulletGO = bulletPooler.GetEnemyBullet(bulletType);
        EnemyBullet bullet = bulletGO.GetComponent<EnemyBullet>();
        bullet.damage = bulletPooler.enemyBulletDatas[(int)bulletType].bulletLevels[upgradedCount].damage;
        bullet.speed = bulletPooler.enemyBulletDatas[(int)bulletType].bulletLevels[upgradedCount].speed;
        bullet.explosionRadius = bulletPooler.enemyBulletDatas[(int)bulletType].bulletLevels[upgradedCount].explosionRadius;

        bulletGO.SetActive(true);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        if (bullet != null)
            bullet.Seek(target);
    }

    void Laser()
    {
        //targetStat.TakeDamege(damageOverTime * Time.deltaTime);
        //targetStat.Slow(slowPct);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 dir = firePoint.position - target.transform.position;

        impactEffect.transform.position = target.transform.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleGlobal)
    {
        if(!angleGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad),0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
