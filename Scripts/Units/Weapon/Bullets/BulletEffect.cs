using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletEffect : MonoBehaviour
{
    WeaponEffectPooler weaponEffectPooler;

    public WeaponBulletType weaponBulletType;

    private void Awake()
    {
        weaponEffectPooler= FindObjectOfType<WeaponEffectPooler>();
    }

    private void OnEnable()
    {
        Invoke("Expired", 5f);
    }

    void Expired()
    {
        weaponEffectPooler.ExpiredBulletEffet(this.gameObject);
    }
}
