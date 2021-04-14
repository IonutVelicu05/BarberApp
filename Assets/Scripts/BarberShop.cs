using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarberShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopName;
    [SerializeField] private TextMeshProUGUI shopAddress;

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
