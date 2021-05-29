using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarberShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopName; // name of the shop 
    [SerializeField] private TextMeshProUGUI shopAddress; //address of the shop
    public string ShopName
    {
        set { shopName.text = value; }
        get { return shopName.text; }
    }
    public string ShopAddress
    {
        set { shopAddress.text = value; }
    }
}
