using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {
        shop= FindObjectOfType<Shop>();
        button= GetComponent<Button>();
        button.onClick.AddListener(ItemClicked);

    }

    public void ItemClicked()
    {
        shop.Selected(this);
    }

}
