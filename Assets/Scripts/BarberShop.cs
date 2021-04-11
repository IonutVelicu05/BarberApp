using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarberShop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shopName;
    [SerializeField] TextMeshProUGUI shopAddress;

    public string ShopName
    {
        set { shopName.text = value; }
    }
    public string ShopAddress
    {
        set { shopAddress.text = value; }
    }
}
