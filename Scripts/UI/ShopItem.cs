using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    Button button;

    Shop shop;

    [SerializeField]
    public WeaponType weaponType;
    [SerializeField]
    public int cost;
    [SerializeField]
    public float upgradeCost;
    [SerializeField]
    TextMeshProUGUI costText;

    private void Awake()
    {
        shop= FindObjectOfType<Shop>();
        button= GetComponent<Button>();
        button.onClick.AddListener(ItemClicked);

        costText.text = cost.ToString();
    }

    public void ItemClicked()
    {
        shop.Selected(this);
    }
    public float GetSellAmount()
    {
        return cost / 2;
    }

}
