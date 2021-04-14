using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mondayHours;
    [SerializeField] private TextMeshProUGUI tuesdayHours;
    [SerializeField] private TextMeshProUGUI wednesdayHours;
    [SerializeField] private TextMeshProUGUI thursdayHours;
    [SerializeField] private TextMeshProUGUI fridayHours;
    [SerializeField] private TextMeshProUGUI saturdayHours;
    [SerializeField] private TextMeshProUGUI sundayHours;
    private BarberShop barberShopClass = new BarberShop();

    public void GetShopWorkingHours()
    {
        StartCoroutine(ShopWorkingHoursEnum());
    }
    IEnumerator ShopWorkingHoursEnum()
    {
        Debug.Log(barberShopClass.ShopName);
        WWWForm form = new WWWForm();
        form.AddField("shopName", barberShopClass.ShopName);
        WWW www = new WWW("http://localhost/barberapp/getworkinghours.php", form);
        yield return www;
        if (www.text[0] != '0')
        {
            Debug.Log(www.text);
        }
        mondayHours.text = www.text.Split('\t')[1];
        tuesdayHours.text = www.text.Split('\t')[2];
        wednesdayHours.text = www.text.Split('\t')[3];
        thursdayHours.text = www.text.Split('\t')[4];
        fridayHours.text = www.text.Split('\t')[5];
        saturdayHours.text = www.text.Split('\t')[6];
        sundayHours.text = www.text.Split('\t')[7];
    }
}
