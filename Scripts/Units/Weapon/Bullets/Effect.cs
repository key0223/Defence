using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    EffectPooler effectPooler;

    public EffectType effectType;

    private void Awake()
    {
        effectPooler= FindObjectOfType<EffectPooler>();
    }

    private void OnEnable()
    {
        Invoke("Expired", 5f);
    }

    void Expired()
    {
        effectPooler.ExpiredEffect(this.gameObject);
    }
}
