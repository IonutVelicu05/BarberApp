using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject adminButton;
    [SerializeField] private GameObject manageShopButton;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject manageShopMenu;
    [SerializeField] private GameObject adminMenu;
    [SerializeField] private GameObject backBTN;
    [SerializeField] private Account account;
    private BarberShop barberShopClass;
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private TextMeshProUGUI shopDescription;
    [SerializeField] private InputField ShopDescriptionField;
    [SerializeField] private GameObject openWorkingHourPrefab;
    [SerializeField] private GameObject openWorkingMinutePrefab;
    [SerializeField] private GameObject closeWorkingHourPrefab;
    [SerializeField] private GameObject closeWorkingMinutePrefab;
    [SerializeField] private TextMeshProUGUI mondayHours;
    [SerializeField] private TextMeshProUGUI tuesdayHours;
    [SerializeField] private TextMeshProUGUI wednesdayHours;
    [SerializeField] private TextMeshProUGUI thursdayHours;
    [SerializeField] private TextMeshProUGUI fridayHours;
    [SerializeField] private TextMeshProUGUI saturdayHours;
    [SerializeField] private TextMeshProUGUI sundayHours;

    private Image tempOpenHourImage;
    private Image tempOpenMinuteImage;
    private Image tempCloseHourImage;
    private Image tempCloseMinuteImage;
    private string selectedOpenWorkingMinute;
    private string selectedOpenWorkingHour;
    private string selectedCloseWorkingHour;
    private string selectedCloseWorkingMinute;
    private string updateWorkingProgram;
    private int selectedWorkingDay; //selected day to edit its working hours
    private int whatToPick = 1;
    private string[] shopName;
    private string[] shopAddress;
    private string selectedShopName;
    private List<GameObject> shopList = new List<GameObject>();
    private string[] shopWorkingHours = new string[8];



    //select day for changing its working hours
    public void Monday()
    {
        selectedWorkingDay = 1;
    }
    public void Tuesday()
    {
        selectedWorkingDay = 2;
    }
    public void Wednesday()
    {
        selectedWorkingDay = 3;
    }
    public void Thursday()
    {
        selectedWorkingDay = 4;
    }
    public void Friday()
    {
        selectedWorkingDay = 5;
    }
    public void Saturday()
    {
        selectedWorkingDay = 6;
    }
    public void Sunday()
    {
        selectedWorkingDay = 7;
    }

    public void backButton()
    {
        loginButton.SetActive(!account.IsLogged);
        manageShopButton.SetActive(account.IsBoss);
        adminButton.SetActive(account.IsAdmin);
        loginMenu.SetActive(false);
        manageShopMenu.SetActive(false);
        adminMenu.SetActive(false);
        backBTN.SetActive(false);
    }
    public void Start()
    {
        ShowShops();
        backButton();
    }
    public void SelectSalon()
    {
        selectedShopName = EventSystem.current.currentSelectedGameObject.name;
        ShowShopDescription();
    }
    public void ShowShops()
    {
        StartCoroutine(ShowShopsEnumerator());
    }
    IEnumerator ShowShopsEnumerator()
    {
        for(int i=0; i<2; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("whatToPick", whatToPick);

            WWW www = new WWW("http://localhost/barberapp/showShops.php", form);
            yield return www;

            switch (whatToPick)
            {
                case 1: shopName = www.text.Split('\t');
                    whatToPick = 2;
                    break;
                case 2: shopAddress = www.text.Split('\t');
                    whatToPick = 1;
                    break;
            }
        }
        if(shopName.Length > 0)
        {
            for(int i=0; i<shopName.Length-1; i++)
            {
                GameObject barberShop = Instantiate(shopPrefab);
                barberShop.name = shopName[i];
                barberShop.SetActive(true);
                barberShop.GetComponent<BarberShop>().ShopName = shopName[i];
                barberShop.GetComponent<BarberShop>().ShopAddress = shopAddress[i];
                barberShop.transform.SetParent(shopPrefab.transform.parent, false);
                shopList.Add(barberShop);
            }
        }
    }

    public void EditShopDescription()
    {
        StartCoroutine(EditShopDescriptionEnum());
    }
    IEnumerator EditShopDescriptionEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("description", ShopDescriptionField.text);
        form.AddField("username", account.AccountUsername);
        WWW www = new WWW("http://localhost/barberapp/editshopdescription.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("descruiption changed");
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void ShowShopDescription()
    {
        StartCoroutine(ShowShopDescriptionEnum());
    }
    IEnumerator ShowShopDescriptionEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("shopname", selectedShopName);

        WWW www = new WWW("http://localhost/barberapp/showdescription.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            shopDescription.text = www.text.Split('\t')[1];
            Debug.Log("showssccs");
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void SelectOpenWorkingHour()
    {
        selectedOpenWorkingHour = EventSystem.current.currentSelectedGameObject.name;
        if (tempOpenHourImage != null)
        {
            tempOpenHourImage.color = Color.white;
        }
        tempOpenHourImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        tempOpenHourImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }
    public void SelectOpenWorkingMinute()
    {
        selectedOpenWorkingMinute = EventSystem.current.currentSelectedGameObject.name;
        if (tempOpenMinuteImage != null)
        {
            tempOpenMinuteImage.color = Color.white;
        }
        tempOpenMinuteImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        tempOpenMinuteImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }
    public void SelectCloseWorkingHour()
    {
        selectedCloseWorkingHour = EventSystem.current.currentSelectedGameObject.name;
        if (tempCloseHourImage != null)
        {
            tempCloseHourImage.color = Color.white;
        }
        tempCloseHourImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        tempCloseHourImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }
    public void SelectCloseWorkingMinute()
    {
        selectedCloseWorkingMinute = EventSystem.current.currentSelectedGameObject.name;
        if (tempCloseMinuteImage != null) //daca deja este un buton selectat il deselecteaza/face culoare alb
        {
            tempCloseMinuteImage.color = Color.white;
        }
        tempCloseMinuteImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>(); //schimba valoarea la tempimage
        tempCloseMinuteImage.color = new Color(0.53f, 0.53f, 0.53f, 1f); //schimba culoarea la imagine / selecteaza butonu
    }
    public void CreateHoursButtons() //creating buttons for every hour after selecting the day to modify the program for
    {
        for(int i = 0; i < 25; i ++)
        {
            GameObject openWorkingHourBTN = Instantiate(openWorkingHourPrefab);
            GameObject closeWorkingHourBTN = Instantiate(closeWorkingHourPrefab);
            closeWorkingHourBTN.transform.SetParent(closeWorkingHourPrefab.transform.parent);
            closeWorkingHourBTN.SetActive(true);
            openWorkingHourBTN.transform.SetParent(openWorkingHourPrefab.transform.parent);
            openWorkingHourBTN.SetActive(true);
            if (i < 10)
            {
                closeWorkingHourBTN.name = "0" + i.ToString();
                closeWorkingHourBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
                openWorkingHourBTN.name = "0" + i.ToString();
                openWorkingHourBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
            }
            else
            {
                closeWorkingHourBTN.name = i.ToString();
                closeWorkingHourBTN.GetComponentInChildren<Text>().text = i.ToString();
                openWorkingHourBTN.name = i.ToString();
                openWorkingHourBTN.GetComponentInChildren<Text>().text = i.ToString();
            }

        }
    }
    public void CreateMinuteButtons() //creating buttons for every minute after selecting the day to modify the program for
    {
        for(int i =0; i<60; i++)
        {
            GameObject openWorkingMinuteBTN = Instantiate(openWorkingMinutePrefab);
            GameObject closeWorkingMinuteBTN = Instantiate(closeWorkingMinutePrefab);
            closeWorkingMinuteBTN.transform.SetParent(closeWorkingMinutePrefab.transform.parent);
            closeWorkingMinuteBTN.SetActive(true);
            openWorkingMinuteBTN.transform.SetParent(openWorkingMinutePrefab.transform.parent);
            openWorkingMinuteBTN.SetActive(true);
            if (i < 10)
            {
                openWorkingMinuteBTN.name = "0" + i.ToString();
                openWorkingMinuteBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
                closeWorkingMinuteBTN.name = "0" + i.ToString();
                closeWorkingMinuteBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
            }
            else
            {
                openWorkingMinuteBTN.name = i.ToString();
                openWorkingMinuteBTN.GetComponentInChildren<Text>().text = i.ToString();
                closeWorkingMinuteBTN.name = i.ToString();
                closeWorkingMinuteBTN.GetComponentInChildren<Text>().text = i.ToString();
            }
        }
    }
    public void UpdateShopWorkingHours()
    {
        StartCoroutine(UpdateShopWH());
    }
    IEnumerator UpdateShopWH()
    {
        if(selectedOpenWorkingMinute == null || selectedOpenWorkingMinute == "") //in caz ca nu alege niciun minut se pune 00
        {
            selectedOpenWorkingMinute = "00";
        }
        if(selectedCloseWorkingMinute == null || selectedCloseWorkingMinute == "") //in caz ca nu alege niciun minut se pune 00
        {
            selectedCloseWorkingMinute = "00";
        }
        updateWorkingProgram = selectedOpenWorkingHour + ":" + selectedOpenWorkingMinute + "-" 
            + selectedCloseWorkingHour + ":" + selectedCloseWorkingMinute;

        WWWForm form = new WWWForm();
        form.AddField("selectedDay", selectedWorkingDay);
        form.AddField("workingHours", updateWorkingProgram);
        form.AddField("bossName", account.AccountUsername);

        WWW www = new WWW("http://localhost/barberapp/editshopworkinghours.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("updated");
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void GetShopWorkingHours()
    {
        StartCoroutine(ShopWorkingHoursEnum());
    }
    IEnumerator ShopWorkingHoursEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("shopName", selectedShopName);
        WWW www = new WWW("http://localhost/barberapp/getworkinghours.php", form);
        yield return www;
        if (www.text[0] != '0')
        {
            Debug.Log(www.text);
        }
        mondayHours.text = "Monday: " + www.text.Split('\t')[1];
        tuesdayHours.text = "Tuesday: " + www.text.Split('\t')[2];
        wednesdayHours.text = "Wednesday: " + www.text.Split('\t')[3];
        thursdayHours.text = "Thursday: " + www.text.Split('\t')[4];
        fridayHours.text = "Friday: " + www.text.Split('\t')[5];
        saturdayHours.text = "Saturday: " + www.text.Split('\t')[6];
        sundayHours.text = "Sunday: " + www.text.Split('\t')[7];
    }
}
