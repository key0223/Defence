using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyPooler enemyPooler;
    EnemyStat enemyStat;

    private float speed;
    private Transform target;
    private int wavepointIndex = 0;

    private void Awake()
    {
        enemyPooler= FindObjectOfType<EnemyPooler>();
        enemyStat= GetComponent<EnemyStat>();
    }
    private void Start()
    {
        speed = enemyStat.startSpeed;
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position)<= 0.4f)
        {
            GetNextWaypoint();
        }

        enemyStat.startSpeed = speed;
    }

    void GetNextWaypoint()
    {

        if(wavepointIndex >= Waypoints.points.Length -1)
        {
            EndPath();
            //Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    private void OnDisable()
    {
        wavepointIndex = 0;
        target = Waypoints.points[0];
    }

    void EndPath()
    {
        PlayerStats.lives--;
        WaveSpawner.EnemiesAlive--;

        enemyPooler.ExpiredEnemy(gameObject);
    }
}
