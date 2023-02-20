using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_FindingTarget : MonoBehaviour
{
    Unit_Rotating unit_Rotating;
    Attack attack;

    [SerializeField]
    private GameObject target;
    public float range = 8f;

    public string enemyTag = "Enemy";

    private void Awake()
    {
        unit_Rotating= GetComponent<Unit_Rotating>();
        attack = GetComponent<Attack>();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy= null;

        foreach(GameObject enemy in enemies) 
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance ) 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
            unit_Rotating.target = this.target;
            attack.target = this.target;
        }
        else
        {
            target = null;
            unit_Rotating.target= null;
            attack.target= null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
