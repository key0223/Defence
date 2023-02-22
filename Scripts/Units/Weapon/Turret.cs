using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    WeaponBullet_Pooler weaponBulletPooler;

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
    public bool useLaser;
    private float fireCountdown = 0f;

    
    [SerializeField]
    protected string enemyTag = "Enemy";

    protected void Awake()
    {
        weaponBulletPooler= FindObjectOfType<WeaponBullet_Pooler>();
    }
    protected void Start()
    {
        InvokeRepeating("UpdateTarget",0f, 0.5f);
    }
    protected void Update()
    {
        if (target == null)
            return;

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

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
