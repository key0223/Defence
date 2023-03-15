using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : MonoBehaviour
{
    EnemyPooler enemyPooler;
    EffectPooler effectPooler;

    [Header("Info")]
    public EnemyType enemyType;
    public TurretEffectType enemytDeathEffect;
    [HideInInspector]
    public float startHp = 100f;
    [HideInInspector]
    public float startSpeed = 10f;
    public Image healthBar;
    public float speed;
    [SerializeField]
    private float hp;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        hp = startHp;
    }
    private void Awake()
    {
        enemyPooler= FindObjectOfType<EnemyPooler>();
        effectPooler= FindObjectOfType<EffectPooler>();
    }
    public void TakeDamege(float amount)
    {
        hp -= amount;
        healthBar.fillAmount = hp / startHp;

        if (hp <= 0 && !isDead)
        {
           Die();
        }

    }
    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }
     void Die()
    {
        isDead= true;

        GameObject deathEffect = effectPooler.GetTurretEffect(enemytDeathEffect);
        deathEffect.transform.position = transform.position;
        deathEffect.transform.rotation = Quaternion.identity;
        deathEffect.SetActive(true);

        WaveSpawner.EnemiesAlive--;

        enemyPooler.ExpiredEnemy(gameObject);
        EnemyInit();
    }

    void EnemyInit()
    {
        isDead = false;
        hp = startHp;
    }

}
