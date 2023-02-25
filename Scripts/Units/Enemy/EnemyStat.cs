using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    EnemyPooler enemyPooler;
    EffectPooler effectPooler;
    public EnemyType enemyType;
    public EffectType effectType;
    public float hp = 100f;
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    private void Start()
    {
       startSpeed = speed;
    }
    private void Awake()
    {
        enemyPooler= FindObjectOfType<EnemyPooler>();
        effectPooler= FindObjectOfType<EffectPooler>();
    }
    public void TakeDamege(float amount)
    {
        Debug.Log("Take Damage");
        hp -= amount;

        if(hp <= 0)
        {
            Die();
        }

    }
    public void Slow(float amount)
    {
        startSpeed = speed * (1f - amount);
    }
     void Die()
    {
        GameObject deathEffect = effectPooler.GetEffect(effectType);
        deathEffect.transform.position = transform.position;
        deathEffect.transform.rotation = Quaternion.identity;
        deathEffect.SetActive(true);

        enemyPooler.ExpiredEnemy(gameObject);
    }
}
