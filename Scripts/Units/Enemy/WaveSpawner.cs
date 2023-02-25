using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    EnemyType enemyType = EnemyType.ENEMY_UFO_GREEN;
    EnemyPooler enemyPooler;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f; // 스폰 대기 시간

    private int waveIndex = 0;

    public TextMeshProUGUI waveCountdownText;

    private void Awake()
    {
        enemyPooler= FindObjectOfType<EnemyPooler>(); 
    }

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f,Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.rounds++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SpawnEnemy()
    {
        GameObject enemyGo = enemyPooler.GetEnemy(enemyType);
        enemyGo.transform.position = spawnPoint.position;
        enemyGo.transform.rotation = spawnPoint.rotation;
        enemyGo.SetActive(true);

        //Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
