using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarberShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopName;
    [SerializeField] private TextMeshProUGUI shopAddress;
    private int WhatToPick;

    public string ShopName
    {
        set { shopName.text = value; }
        get { return shopName.text; }
    }
    public string ShopAddress
    {
        set { shopAddress.text = value; }
    }
    public void showBarbers()
    {
        StartCoroutine(ShowBarbersEnumerator());
    }
    IEnumerator ShowBarbersEnumerator()
    {
        for(int i = 0; i<4; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("whatToPick", WhatToPick);
            form.AddField("shopName", ShopName);
            WWW www = new WWW("http://localhost/barberapp/showBarbers.php", form);
            yield return www;

        }
    }
}
