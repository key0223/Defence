using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    [SerializeField]
    public Wave[] waves;

    //EnemyType enemyType = EnemyType.ENEMY_UFO_GREEN;
    EnemyPooler enemyPooler;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f; // 스폰 대기 시간

    private int waveIndex = 0;

    public TextMeshProUGUI waveCountdownText;

    public GameManager gameManager;
    private void Awake()
    {
        enemyPooler = FindObjectOfType<EnemyPooler>();
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

       
    }

    public void SpawnEnemy(EnemyType enemy)
    {
        GameObject enemyGo = enemyPooler.GetEnemy(enemy);
        enemyGo.transform.position = spawnPoint.position;
        enemyGo.transform.rotation = spawnPoint.rotation;
        enemyGo.SetActive(true);
    }
}
