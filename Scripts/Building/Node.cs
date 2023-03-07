using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    BuildManager buildManager;
    Weapon_Pooler weaponPooler;
    EffectPooler effectPooler;

    WeaponType currentWeaponType = WeaponType.WEAPON_NONE;

    public Color hoverColor;
    public Color startColor;
    public Color notEnoughMoneyColor;
    private Renderer rend;

    public Vector3 positionOffset;
    public GameObject turret;
    public Turret m_turret;

    public int upgradedCount = 0;

    private void Start()
    {
        buildManager = BuildManager.Instance;
        weaponPooler = FindObjectOfType<Weapon_Pooler>();
        effectPooler = FindObjectOfType<EffectPooler>();
        rend = GetComponent<Renderer>();
        startColor = rend.materials[1].color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void BuildTurret(ShopItem shopItem)
    {
        if (currentWeaponType == WeaponType.WEAPON_NONE)
            return;

        if (PlayerStats.money < shopItem.cost)
        {
            Debug.Log("Not enough money to build ");
            return;
        }
        PlayerStats.money -= shopItem.cost;

        GameObject selectedGO = weaponPooler.GetWeapon(currentWeaponType);
        selectedGO.transform.position = GetBuildPosition();
        selectedGO.transform.rotation = Quaternion.identity;
        selectedGO.SetActive(true);

        turret = selectedGO;
        m_turret = selectedGO.GetComponent<Turret>();

        GameObject effect = effectPooler.GetEffect(EffectType.EFFECT_BUILDTURRET);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

        Debug.Log("Turret build! Money left: " + PlayerStats.money);
    }

    public void SellTurret()
    {
        PlayerStats.money += buildManager.GetTurretToBuild().GetSellAmount();

        GameObject effect = effectPooler.GetEffect(EffectType.EFFECT_SELL);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

        weaponPooler.ExpiredTurret(turret);
        turret = null;
    }
    public void UpgradeTurret()
    {
        ShopItem shopItem = buildManager.GetTurretToBuild();

        if (PlayerStats.money < shopItem.upgradeCost)
        {
            return;
        }
        PlayerStats.money -= shopItem.upgradeCost;

        //총알 능력치 업그레이드 
        upgradedCount++;
        m_turret.upgradedCount = upgradedCount;

        GameObject effect = effectPooler.GetEffect(EffectType.EFFECT_BUILDTURRET);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild)
            return;

        currentWeaponType = buildManager.GetTurretToBuild().weaponType;
        BuildTurret(buildManager.GetTurretToBuild());
        
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        if(buildManager.HasMoney)
        {
            rend.materials[1].color = hoverColor;

        }
        else
        {
            rend.materials[1].color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.materials[1].color = startColor;
    }
}
