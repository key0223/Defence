using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    WeaponBullet_Pooler weaponBulletPooler;
    EnemyStat targetStat;

    [SerializeField]
    protected GameObject target;

    [Header("Turret Info")]
    public WeaponType weaponType;
    public float range;
    [SerializeField]
    protected Transform partToRotate;
    public float turnSpeed;

    [Header("Bullet Info")]
    public WeaponBulletType weaponBulletType;
    public EffectType effectType;
    [SerializeField]
    protected Transform firePoint;
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Laser")]
    public bool useLaser;

    public float damageOverTime= 30f;
    public float slowPct = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [SerializeField]
    protected string enemyTag = "Enemy";

    protected void Awake()
    {
        weaponBulletPooler = FindObjectOfType<WeaponBullet_Pooler>();
    }
    protected void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    protected void Update()
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

        LockOnTarget();

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

    protected void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
            targetStat = nearestEnemy.GetComponent<EnemyStat>();
        }
        else
        {
            target = null;
        }
    }
    protected void LockOnTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {

       targetStat.TakeDamege(damageOverTime *Time.deltaTime);
        targetStat.Slow(slowPct);
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

    protected void Shoot()
    {
        GameObject bulletGO = weaponBulletPooler.GetWeaponBullet(weaponBulletType);
        bulletGO.SetActive(true);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
