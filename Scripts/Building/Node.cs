using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildManager;
    TurretPooler turretPooler;
    EffectPooler effectPooler;

    TurretType currentTurretType = TurretType.TURRET_NONE;

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
        turretPooler = FindObjectOfType<TurretPooler>();
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
        if (currentTurretType == TurretType.TURRET_NONE)
            return;

        if (PlayerStats.money < shopItem.cost)
        {
            Debug.Log("Not enough money to build ");
            return;
        }
        PlayerStats.money -= shopItem.cost;

        GameObject selectedGO = turretPooler.GetTurret(currentTurretType);
        selectedGO.transform.position = GetBuildPosition();
        selectedGO.transform.rotation = Quaternion.identity;
        selectedGO.SetActive(true);

        turret = selectedGO;
        m_turret = selectedGO.GetComponent<Turret>();

        GameObject effect = effectPooler.GetTurretEffect(TurretEffectType.TURRET_EFFECT_BUILDTURRET);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

       
        Debug.Log("Turret build! Money left: " + PlayerStats.money);
    }

    public void SellTurret()
    {
        PlayerStats.money += buildManager.GetTurretToBuild().GetSellAmount();

        GameObject effect = effectPooler.GetTurretEffect(TurretEffectType.TURRET_EFFECT_SELL);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

        turretPooler.ExpiredTurret(turret);
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

        GameObject effect = effectPooler.GetTurretEffect(TurretEffectType.TURRET_EFFECT_BUILDTURRET);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);
    }

    public void DieTurret(GameObject _turret)
    {
        ShopItem shopItem = buildManager.GetTurretToBuild();
        GameObject effect = effectPooler.GetTurretEffect(TurretEffectType.TURRET_EFFECT_ENEMYDEATH);
        effect.transform.position = GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

        turretPooler.ExpiredTurret(_turret);
        turret = null;

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

        currentTurretType = buildManager.GetTurretToBuild().turretType;
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
