using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BarberShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopName; // name of the shop 
    [SerializeField] private TextMeshProUGUI shopAddress; //address of the shop
    [SerializeField] private Image shopLogo; //logo of the shop
    [SerializeField] private TextMeshProUGUI shopEmployees; //total number of employees - 3; because 3 are shown with pictures.

    public string ShopEmployees
    {
        get { return shopEmployees.text; }
        set { shopEmployees.text = value; }
    }
    public Image ShopLogo
    {
        get { return shopLogo; }
        set { shopLogo.sprite = value.sprite; }
    }
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
