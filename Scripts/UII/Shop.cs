using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void Selected(ShopItem shopItem)
    {
        
        buildManager.SetTurretToBuild(shopItem);
    }
    
}
