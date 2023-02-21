using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    Weapon_Pooler weaponPooler;
    EffectPooler effectPooler;

    ShopItem shopItem;
    WeaponType currentWeaponType;
    

    private GameObject turretToBuild;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(Instance);
        weaponPooler = FindObjectOfType<Weapon_Pooler>();
        effectPooler= FindObjectOfType<EffectPooler>();
        currentWeaponType = WeaponType.WEAPON_NONE;
    }

    public bool CanBuild { get { return currentWeaponType != WeaponType.WEAPON_NONE; } }
    public bool HasMoney { get { return PlayerStats.money >= shopItem.cost; } }
    public void BuildTurretOn(Node node)
    {
        if (currentWeaponType == WeaponType.WEAPON_NONE)
            return;

        if(PlayerStats.money <shopItem.cost)
        {
            Debug.Log("Not enough money to build ");
            return;
        }
        PlayerStats.money -= shopItem.cost;
        GameObject selectedGO = weaponPooler.GetWeapon(currentWeaponType);
        selectedGO.transform.position = node.GetBuildPosition();
        selectedGO.transform.rotation = Quaternion.identity;
        selectedGO.SetActive(true);

        node.turret = selectedGO;

        GameObject effect = effectPooler.GetEffect(EffectType.EFFECT_BUILDTURRET);
        effect.transform.position = node.GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

        Debug.Log("Turret build! Money left: "+ PlayerStats.money);
    }
    
    public void SetTurretToBuild(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        currentWeaponType = shopItem.weaponType;
    }
}
