using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    Weapon_Pooler weaponPooler;

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
    }

    public bool CanBuild { get { return currentWeaponType != WeaponType.WEAPON_NONE; } }
    public void BuildTurretOn(Node node)
    {
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

        Debug.Log("Turret build! Money left: "+ PlayerStats.money);
    }
    /*
    public WeaponType GetTurretToBuild()
    {
        return currentWeaponType;
    }
    */
    public void SetTurretToBuild(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        currentWeaponType = shopItem.weaponType;
    }
}
