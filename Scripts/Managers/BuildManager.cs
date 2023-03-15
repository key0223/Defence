using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    TurretPooler turretPooler;
    EffectPooler effectPooler;
    NodeUI nodeUI;

    ShopItem shopItem;
    Node selectedNode;
    TurretType currentTurretType;
    public TurretType CurrentTurretType { get; set; }


    private GameObject turretToBuild;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(Instance);

        turretPooler = FindObjectOfType<TurretPooler>();
        effectPooler = FindObjectOfType<EffectPooler>();
        nodeUI = FindObjectOfType<NodeUI>();
        currentTurretType = TurretType.TURRET_NONE;
    }

    public bool CanBuild { get { return currentTurretType != TurretType.TURRET_NONE; } }
    public bool HasMoney { get { return PlayerStats.money >= shopItem.cost; } }

    /*
    public void BuildTurretOn(Node node)
    {
        if (currentWeaponType == WeaponType.WEAPON_NONE)
            return;

        if (PlayerStats.money < shopItem.cost)
        {
            Debug.Log("Not enough money to build ");
            return;
        }
        PlayerStats.money -= shopItem.cost;
        GameObject selectedGO = turretPooler.GetTurret(currentWeaponType);
        selectedGO.transform.position = node.GetBuildPosition();
        selectedGO.transform.rotation = Quaternion.identity;
        selectedGO.SetActive(true);

        node.turret = selectedGO;

        GameObject effect = effectPooler.GetTurretEffect(TurretEffectType.TURRET_EFFECT_BUILDTURRET);
        effect.transform.position = node.GetBuildPosition();
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);

        Debug.Log("Turret build! Money left: " + PlayerStats.money);
    }
    */
    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    
    public void  DeselectNode()
    {
        selectedNode= null;
        nodeUI.Hide();
    }
    public void SetTurretToBuild(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        currentTurretType = shopItem.turretType;

        DeselectNode();
    }
    public ShopItem GetTurretToBuild()
    {
        return shopItem;
    }
}
