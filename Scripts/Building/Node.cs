using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    BuildManager buildManager;
    Weapon_Pooler weapon_Pooler;

    public Color hoverColor;
    public Color startColor;
    public Color notEnoughMoneyColor;
    private Renderer rend;

    public Vector3 positionOffset;
    public GameObject turret;


    private void Start()
    {
        buildManager = BuildManager.Instance;
        weapon_Pooler = FindObjectOfType<Weapon_Pooler>();
        rend = GetComponent<Renderer>();
        startColor = rend.materials[1].color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (turret != null)
        {
            Debug.Log("Cant build there! - ");
            return;
        }

        buildManager.BuildTurretOn(this);
        /*
        GameObject turretToBuild = weapon_Pooler.GetWeapon(buildManager.GetTurretToBuild());
        turret = turretToBuild;
        turret.SetActive(true);
        turret.transform.position = transform.position + positionOffset;
        turret.transform.rotation = transform.rotation;
        */
        //GameObject turretToBuild = buildManager.GetTurretToBuild();
        //turret = Instantiate(turretToBuild, transform.position+ positionOffset, transform.rotation);
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
