using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    [SerializeField]
    Node node;
    BulletPooler bulletPooler;
    TurretPooler turretPooler;
    EnemyStat targetStat;

    [SerializeField]
    protected GameObject target;

    [Header("Turret Info")]
    public TurretType turretType;
    public float range;
    [SerializeField]
    protected Transform partToRotate;
    [HideInInspector]
    public float startHp = 100f;
    [SerializeField]
    private float hp;
    public Image healthBar;
    private bool isDead = false;
    public float turnSpeed;
    public int upgradedCount = 0;


    [Header("Bullet Info")]
    public TurretBulletType turretBulletType;
    public TurretEffectType effectType;
    
    [SerializeField]
    protected Transform firePoint;
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Laser")]
    public bool useLaser;

    public float damageOverTime = 30f;
    public float slowPct = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [SerializeField]
    protected string enemyTag = "Enemy";

    protected void Awake()
    {
        node = FindObjectOfType<Node>();
        bulletPooler = FindObjectOfType<BulletPooler>();
        turretPooler = FindObjectOfType<TurretPooler>();
    }
   
    protected void Start()
    {
        hp = startHp;

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
        targetStat.TakeDamege(damageOverTime * Time.deltaTime);
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
        GameObject bulletGO = bulletPooler.GetTurretBullet(turretBulletType);
        TurretBullet bullet = bulletGO.GetComponent<TurretBullet>();

        bulletGO.SetActive(true);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        if (bullet != null)
            bullet.Seek(target);
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        healthBar.fillAmount = hp / startHp;

        if(hp <=0 && !isDead)
        {
            isDead = true;

            node.DieTurret(gameObject);
            TurretInit();
        }
    }

    void TurretInit()
    {
        isDead = false;
        hp = startHp;
        upgradedCount = 0;
    }
    

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
