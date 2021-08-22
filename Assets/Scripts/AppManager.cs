using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject adminButton;
    [SerializeField] private GameObject manageShopButton;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject manageShopMenu;
    [SerializeField] private GameObject adminMenu;
    [SerializeField] private GameObject registerButton;
    [SerializeField] private GameObject registerMenu;
    [SerializeField] private GameObject barberMenuBTN;
    [SerializeField] private GameObject backBTN;
    [SerializeField] private GameObject profileMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject locationPickMenu;
    private Account account;
    [SerializeField] private Appointments appointments;
    [SerializeField] private TextMeshProUGUI pageTitle;
    //translation

    private int selectedLanguage = 2; // 1 = romana ;; 2 = engleza
    public int SelectedLanguage
    {
        get { return selectedLanguage; }
    }
    [SerializeField] private TextMeshProUGUI locationShowShopBTN;
    [SerializeField] private TextMeshProUGUI locationShowShopsInfo;
    [SerializeField] private TextMeshProUGUI settingsMenuLanguageTxt;
    [SerializeField] private TextMeshProUGUI settingsMenuLoginBtn;
    [SerializeField] private TextMeshProUGUI settingsMenuManageShopsBtn;
    [SerializeField] private TextMeshProUGUI settingsMenuRegisterBtn;
    [SerializeField] private TextMeshProUGUI settingsMenuBarberMenuBtn;
    [SerializeField] private TextMeshProUGUI settingsMenuCreateShopBtn;
    [SerializeField] private TextMeshProUGUI loginMenuLoginBtn;
    [SerializeField] private TextMeshProUGUI loginMenuBackBtn;
    [SerializeField] private TextMeshProUGUI registerMenuRegisterBtn;
    [SerializeField] private TextMeshProUGUI registerMenuBackBtn;
    [SerializeField] private TextMeshProUGUI registerMenuNextBtn;
    [SerializeField] private TextMeshProUGUI barberMenuTimePerCutTxt;
    [SerializeField] private TextMeshProUGUI barberMenuTimePerCutBtn;
    [SerializeField] private TextMeshProUGUI barberMenuCheckAppointmentsTxt;
    [SerializeField] private TextMeshProUGUI barberMenuCheckAppointmentsBtn;
    [SerializeField] private TextMeshProUGUI barberMenuEditPricesTxt;
    [SerializeField] private TextMeshProUGUI barberMenuEditPricesBtn;
    [SerializeField] private TextMeshProUGUI editShopSelectShopTxt;
    [SerializeField] private TextMeshProUGUI editShopDescriptionBtn;
    [SerializeField] private TextMeshProUGUI editShopWorkingProgramBtn;
    [SerializeField] private TextMeshProUGUI editShopPhotosBtn;
    [SerializeField] private TextMeshProUGUI editShopBarbersBtn;
    [SerializeField] private TextMeshProUGUI editShopPricesBtn;
    [SerializeField] private Text timeToCutMenuPlaceholder;
    [SerializeField] private TextMeshProUGUI timeToCutMenuSubmitBtn;
    [SerializeField] private TextMeshProUGUI timeToCutMenuInfoTxt;
    [SerializeField] private TextMeshProUGUI editPricesMenuHaircutBtn;
    [SerializeField] private TextMeshProUGUI editPricesMenuBeardBtn;
    [SerializeField] private TextMeshProUGUI editPricesMenuMustacheBtn;
    [SerializeField] private TextMeshProUGUI editPricesMenuHairColourBtn;
    [SerializeField] private TextMeshProUGUI editPricesMenuEyebrowBtn;
    [SerializeField] private TextMeshProUGUI editPricesMenuInfoTxt;
    [SerializeField] private Text editPricesMenuInputPlaceholder;
    [SerializeField] private Text editPricesMenuInputUpdateBtn;
    [SerializeField] private Text editPricesMenuInputBackBtn;
    [SerializeField] private Text shopMenuEditDescriptionPlaceholder;
    [SerializeField] private TextMeshProUGUI shopMenuEditDescriptionUpdateBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramInfoTxt;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramMondayBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramTuesdayBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramWednesdayBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramThursdayBtn;
    [SerializeField] private TextMeshProUGUI editShopmenuWorkingProgramFridayBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramSaturdayBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramSundayBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramHoursInfoTxt;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramHoursOpensAtTxt;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramHoursClosesAtTxt;
    [SerializeField] private TextMeshProUGUI editShopMenuWorkingProgramHoursUpdateBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuPhotosInfoTxt;
    [SerializeField] private TextMeshProUGUI editShopMenuSelectPhotosBtn;
    [SerializeField] private TextMeshProUGUI editShopMenuUpdatePhotosBtn;
    [SerializeField] private TextMeshProUGUI salonMenuInfoBtn;
    [SerializeField] private TextMeshProUGUI salonMenuBarberBtn;
    [SerializeField] private TextMeshProUGUI salonMenuBarberFirstName;
    [SerializeField] private TextMeshProUGUI salonMenuBarberLastName;
    [SerializeField] private TextMeshProUGUI appointmentMenuCheckHoursBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuCheckHoursBackBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuNextToServicesBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuNextToServicesBackBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuMentionsInfoTxt;
    [SerializeField] private Text appointmentMenuMentionsPlaceholder;
    [SerializeField] private TextMeshProUGUI appointmentMenuInsertNameInfoTxt;
    [SerializeField] private Text appointmentMenuInsertNamePlaceholder;
    [SerializeField] private TextMeshProUGUI appointmentMenuCreateAppointmentBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuHaircutServiceBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuBeardServiceBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuMustacheServiceBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuHairColourBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuEyebrowBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuBackToHourBtn;

    //end of translation

    //SHOP 
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
    [SerializeField] private Image shopPhoto; //the photo user is currently seeing
    private Texture2D[] shopImage = new Texture2D[6];
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private InputField imageFromPhoneName;
    [SerializeField] private Dropdown whatImageToUpload;
    private string description_ro;
    private string description_en;
    private int descriptionToEdit = 1; // 1 = descrierea in romana ;; 2 = descrierea in engleza
    [SerializeField] private TextMeshProUGUI editDescriptionHint;
    [SerializeField] private GameObject editDescriptionRoBtn;
    [SerializeField] private GameObject editDescriptionEnBtn;
    //SHOP

    [SerializeField] private Dropdown countyDropdown;
    [SerializeField] private Dropdown cityDropdown;
    private string selectedCounty;
    private string selectedCity;
    private List<string> countyList = new List<string>();
    private List<string> cityList = new List<string>();
    private int uploadedImageNumber; //the number of the uploading image
    private int shopImageNumber = 1;
    private List<string> uploadImageList = new List<string>();
    private bool anotherShopSelected = false; //when selecting a shop it sets it to true and if its true when selecting it shows the first image of
    //the shop; sets shopImageNumber to 1;
    private Texture2D imageFromPhone;
    // ~~~ User (boss) manage shop ~~~
    private string[] shopsOfUser;
    private string[] shopsOfUserAddress;
    private string[] shopsOfUserCity;
    private GameObject selectedShopToManage;
    [SerializeField] private GameObject manageShopPrefab;
    private TextMeshProUGUI manageShopNameTXT;
    private TextMeshProUGUI manageShopAddressTXT;
    private TextMeshProUGUI manageShopCityTXT;
    private List<GameObject> shopsOfUserList = new List<GameObject>();
    [SerializeField] private InputField shopInsertEditPrice;
    private int editGeneralPrices = 0; // 1=haircut; 2=beard; 3=mustache; 4=colour; 5=eyebrow
    private bool shopGeneralPrice;
    [SerializeField] private GameObject EditGeneralPricesBTN;
    // ~~ end of manage shop ~~

    // ~~ Barber manage profile ~~
    [SerializeField] private GameObject EditBarberPricesBTN;
    private int editBarberPrices = 0; // 1=haircut; 2=beard; 3=mustache; 4=colour; 5=eyebrow
    [SerializeField] private InputField barberInsertEditPrice;
    // ~~ end of barber manage profile ~~

    //ERROR INFO //
    [SerializeField] private GameObject errorObj;
    [SerializeField] private TextMeshProUGUI errorTXT;
    private string generalErrorEng = "Something went wrong. Please try again in a few minutes. If the problem continues contact the administrator.";
    private string generalErrorRo = "Ceva nu a functionat ! Te rugam sa incerci din nou in cateva minute. Daca problema continua contacteaza un administrator.";
    //END ERROR INFO//


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
    private string lastShopSelected; // name of the last shop sleceted (if its another shop, it shows the first image)
    private List<GameObject> shopList = new List<GameObject>();
    private string[] shopWorkingHours = new string[8];

    private int whatToPickBarber = 1;
    private List<GameObject> barberList = new List<GameObject>();
    private string[] firstName;
    private string[] lastName;
    private string[] fiveStarReviews;
    private string[] fourStarReviews;
    private string[] threeStarReviews;
    private string[] twoStarReviews;
    private string[] oneStarReviews;
    [SerializeField] private GameObject barberPrefab;
    private int timeToCut;
    //boss create shop --
    [SerializeField] private InputField createShopName;
    [SerializeField] private InputField createShopAddress;
    [SerializeField] private GameObject createShopMenuBTN;
    [SerializeField] private Dropdown createShopCityDropdown;
    [SerializeField] private Dropdown createShopCountyDropdown;
    [SerializeField] private Toggle createShopGeneralPrices;


    public void OpenProfileMenu()
    {
        if(shopList.Count < 1)
        {
            profileMenu.SetActive(true);
            settingsMenu.SetActive(false);
            locationPickMenu.SetActive(true);
        }
        else
        {
            profileMenu.SetActive(true);
            settingsMenu.SetActive(false);
            locationPickMenu.SetActive(false);
        }
    }
    public void OpenSettingsMenu()
    {
        if (selectedCity == null)
        {
            settingsMenu.SetActive(true);
            profileMenu.SetActive(false);
            locationPickMenu.SetActive(true);
        }
        else
        {
            settingsMenu.SetActive(true);
            profileMenu.SetActive(false);
            locationPickMenu.SetActive(false);
        }
    }
    public void OpenLocationMenu()
    {
        locationPickMenu.SetActive(true);
        settingsMenu.SetActive(false);
        profileMenu.SetActive(false);
    }
    public void Start()
    {
        account = gameObject.GetComponent<Account>();
        countyDropdown.ClearOptions(); //choosing from which location to show shops
        updateLanguageTexts();
        countyDropdown.AddOptions(countyList);

        createShopCountyDropdown.ClearOptions();
        createShopCountyDropdown.AddOptions(countyList);

        afterLogin();
        countyDropdown.onValueChanged.AddListener(delegate { CountyPicked(countyDropdown); });
        cityDropdown.onValueChanged.AddListener(delegate { CityPicked(cityDropdown); });

        createShopCountyDropdown.onValueChanged.AddListener(delegate { CreateShopCountyPicked(createShopCountyDropdown); });
        whatImageToUpload.onValueChanged.AddListener(delegate { WhatImageToUpload(whatImageToUpload); });


    }

    public int GetTimeToCut()
    {
        return timeToCut;
    }
    public void SelectRomanianLanguage()
    {
        selectedLanguage = 1;
        updateLanguageTexts();
    }
    public void SelectEnglishLanguage()
    {
        selectedLanguage = 2;
        updateLanguageTexts();
    }
    public string GetSelectedShopName()
    {
        return selectedShopName;
    }

    public void EditGeneralHaircutPrice()
    {
        editGeneralPrices = 1;
    }
    public void EditGeneralBeardPrice()
    {
        editGeneralPrices = 2;
    }
    public void EditGeneralMustachePrice()
    {
        editGeneralPrices = 3;
    }
    public void EditGeneralHaircolourPrice()
    {
        editGeneralPrices = 4;
    }
    public void EditGeneralEyebrowPrice()
    {
        editGeneralPrices = 5;
    }
    public void EditBarberHaircutPrice()
    {
        editBarberPrices = 1;
    }
    public void EditBarberBeardPrice()
    {
        editBarberPrices = 2;
    }
    public void EditBarberMustachePrice()
    {
        editBarberPrices = 3;
    }
    public void EditBarberHaircolourPrice()
    {
        editBarberPrices = 4;
    }
    public void EditBarberEyebrowPrice()
    {
        editBarberPrices = 5;
    }
    public void UpdateGeneralPrices()
    {
        StartCoroutine(UpdateGeneralPricesEnum());
    }
    IEnumerator UpdateGeneralPricesEnum()
    {
        if (int.TryParse(shopInsertEditPrice.text, out int a) == false)
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Introdu un numar";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Please insert a real number.";
            }
            errorObj.SetActive(true);
        }
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("whatToUpdate", editGeneralPrices.ToString()));
        form.Add(new MultipartFormDataSection("price", shopInsertEditPrice.text));
        form.Add(new MultipartFormDataSection("shopName", selectedShopToManage.name));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/updategeneralprices.php", form);
        yield return web.SendWebRequest();
        if (web.isNetworkError || web.isHttpError)
        {
            Debug.Log(web.error);
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Ceva nu a functionat, incearca din nou mai tarziu.";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Something went wrong, please try again later.";
            }
            errorObj.SetActive(true);
        }
        else
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Pret actualizat cu succes.";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Price updated successfully.";
            }
            errorObj.SetActive(true);
        }
    }
    public void UpdateBarberPrices()
    {
        StartCoroutine(UpdateBarberPricesEnum());
    }
    IEnumerator UpdateBarberPricesEnum()
    {
        if (int.TryParse(barberInsertEditPrice.text, out int a) == false)
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Introdu un numar real";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Please insert a real number.";
            }
            errorObj.SetActive(true);
        }
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("whatToUpdate", editBarberPrices.ToString()));
        form.Add(new MultipartFormDataSection("price", barberInsertEditPrice.text));
        form.Add(new MultipartFormDataSection("shopName", account.WorksAt));
        form.Add(new MultipartFormDataSection("username", account.AccountUsername));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/updatebarberprices.php", form);
        yield return web.SendWebRequest();
        if (web.isNetworkError || web.isHttpError)
        {
            Debug.Log(web.error);
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Ceva nu a functionat, incearca din nou mai tarziu.";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Something went wrong, please try again later.";
            }
            errorObj.SetActive(true);
        }
        else
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Pret actualizat cu succes.";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Price updated successfully.";
            }
            errorObj.SetActive(true);
        }
    }
    public void showBarbers()
    {
        StartCoroutine(ShowBarbersEnumerator());
    }
    IEnumerator ShowBarbersEnumerator()
    {
        loadingScreen.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("whatToPick", whatToPickBarber);
            form.AddField("shopName", selectedShopName);
            WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/showBarbers.php", form);
            yield return www;
            switch (whatToPickBarber)
            {
                case 1:
                    firstName = www.text.Split('\t');
                    whatToPickBarber = 2;
                    break;
                case 2:
                    lastName = www.text.Split('\t');
                    whatToPickBarber = 3;
                    break;
                case 3:
                    fiveStarReviews = www.text.Split('\t');
                    whatToPickBarber = 4;
                    break;
                case 4:
                    fourStarReviews = www.text.Split('\t');
                    whatToPickBarber = 5;
                    break;
                case 5:
                    threeStarReviews = www.text.Split('\t');
                    whatToPickBarber = 6;
                    break;
                case 6:
                    twoStarReviews = www.text.Split('\t');
                    whatToPickBarber = 7;
                    break;
                case 7:
                    oneStarReviews = www.text.Split('\t');
                    whatToPickBarber = 1;
                    break;
            }
        }
        if (lastName.Length > 0)
        {
            if (barberList.Count > 0)
            {
                foreach (GameObject barber in barberList)
                {
                    Destroy(barber.gameObject);
                }
                barberList.Clear();
            }
        }
        for (int j = 0; j < lastName.Length -1; j++)
        {
            GameObject barber = Instantiate(barberPrefab);
            barber.name = firstName[j] + "\t" + lastName[j];
            if(selectedLanguage == 1)
            {
                barber.GetComponent<Barber>().FirstNameUI = "Prenume: " + firstName[j];
                barber.GetComponent<Barber>().LastNameUI = "Nume: " + lastName[j];
            }
            else if(selectedLanguage == 2)
            {
                barber.GetComponent<Barber>().FirstNameUI = "First name: " + firstName[j];
                barber.GetComponent<Barber>().LastNameUI = "Last name: " + lastName[j];
            }
            barber.GetComponent<Barber>().FiveStarUI = fiveStarReviews[j];
            barber.GetComponent<Barber>().FourStarUI = fourStarReviews[j];
            barber.GetComponent<Barber>().ThreeStarUI = threeStarReviews[j];
            barber.GetComponent<Barber>().TwoStarUI = twoStarReviews[j];
            barber.GetComponent<Barber>().OneStarUI = oneStarReviews[j];
            barber.transform.SetParent(barberPrefab.transform.parent, false);
            barber.SetActive(true);
            barberList.Add(barber);
        }
        loadingScreen.SetActive(false);
    }
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

    public void afterLogin()
    {
        loginButton.SetActive(!account.IsLogged);
        manageShopButton.SetActive(account.IsBoss);
        registerButton.SetActive(!account.IsLogged);
        registerMenu.SetActive(false);
        loginMenu.SetActive(false);
        manageShopMenu.SetActive(false);
        adminMenu.SetActive(false);
        backBTN.SetActive(false);
        barberMenuBTN.SetActive(account.IsEmployed);
        if (account.IsLogged)
        {
            if (account.IsBoss)
            {
                createShopMenuBTN.SetActive(account.CanCreateShops);
            }
            if(shopsOfUserList.Count <= 0)
            {
                GetShopsOfUser();
            }
        }
    }
    public void CreateShop()
    {
        StartCoroutine(CreateShopEnum());
    }
    IEnumerator CreateShopEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("shopName", createShopName.text));
        form.Add(new MultipartFormDataSection("shopAddress", createShopAddress.text));
        form.Add(new MultipartFormDataSection("shopCounty", createShopCountyDropdown.options[createShopCountyDropdown.value].text));
        form.Add(new MultipartFormDataSection("shopCity", createShopCityDropdown.options[createShopCityDropdown.value].text));
        form.Add(new MultipartFormDataSection("username", account.AccountUsername));
        form.Add(new MultipartFormDataSection("personalCode", account.PersonalCode.ToString()));
        form.Add(new MultipartFormDataSection("generalPrices", createShopGeneralPrices.isOn.ToString()));

        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/createshop.php", form);
        yield return webreq.SendWebRequest();
        if (webreq.isNetworkError || webreq.isHttpError)
        {
            Debug.Log(webreq.error);
            if (selectedLanguage == 1)
            {
                errorTXT.text = generalErrorRo;
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = generalErrorEng;
            }
            errorObj.SetActive(true);
        }
        else
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Salon creat cu succes! Verifica meniul saloanelor pentru mai multe optiuni de editare a salonului.";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Shop created ! Check the manage shops menu for more edit options.";
            }
            errorObj.SetActive(true);
            createShopName.text = "";
            createShopAddress.text = "";
            createShopCountyDropdown.RefreshShownValue();
            createShopCityDropdown.RefreshShownValue();
            createShopMenuBTN.SetActive(account.ShopsToCreate - account.ShopsCreated > 0);
        }
    }
    void CountyAddToList(params string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            countyList.Add(list[i]);
        }
    }
    void CityAddToList(params string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            cityList.Add(list[i]);
        }
    }
    void ImageAddToList(params string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            uploadImageList.Add(list[i]);
        }
    }
    public void updateLanguageTexts() //whenever the language is changed all the texts will be updated here.
    {
        if (selectedLanguage == 1)
        {
            loadingScreen.SetActive(true);
            countyList.Clear();
            countyDropdown.ClearOptions(); //choosing from which location to show shops
            CountyAddToList("Alege un judet", "Olt", "Dolj", "Timis");
            cityList.Clear();
            cityDropdown.ClearOptions();
            CityAddToList("Nu ai ales un judet");
            cityDropdown.AddOptions(cityList);
            countyDropdown.AddOptions(countyList);
            locationShowShopBTN.text = "Afiseaza";
            locationShowShopsInfo.text = "Alege locatia saloanelor.";
            settingsMenuLanguageTxt.text = "Limba";
            settingsMenuLoginBtn.text = "Conectare";
            settingsMenuRegisterBtn.text = "Înregistrare";
            settingsMenuManageShopsBtn.text = "Saloane";
            settingsMenuBarberMenuBtn.text = "Meniu Frizer";
            settingsMenuCreateShopBtn.text = "Creaza salon";
            loginMenuLoginBtn.text = "Conectare";
            loginMenuBackBtn.text = "Înapoi";
            registerMenuRegisterBtn.text = "Înregistrare";
            registerMenuBackBtn.text = "Înapoi";
            registerMenuNextBtn.text = "Înainte";
            barberMenuTimePerCutTxt.text = "Modifica timpul necesar pentru o tunsoare";
            barberMenuTimePerCutBtn.text = "Timp tuns";
            barberMenuCheckAppointmentsTxt.text = "Verifica programarile tale";
            barberMenuCheckAppointmentsBtn.text = "Afiseaza programari";
            barberMenuEditPricesTxt.text = "Modifica preturile serviciilor tale";
            barberMenuEditPricesBtn.text = "Modifica preturi";
            editShopSelectShopTxt.text = "Alege ce salon doresti sa modifici";
            editShopDescriptionBtn.text = "Modifica descrierea";
            editShopWorkingProgramBtn.text = "Modifica program de munca";
            editShopPhotosBtn.text = "Modifica poze";
            editShopPricesBtn.text = "Modifica preturi";
            editShopBarbersBtn.text = "Modifica angajati";
            timeToCutMenuPlaceholder.text = "Scrie aici...";
            timeToCutMenuSubmitBtn.text = "Actualizeaza";
            timeToCutMenuInfoTxt.text = "Introdu timpul de care ai nevoie intre clienti. \n Timpul necesar pentru realizarea unei tunsori.";
            editPricesMenuHaircutBtn.text = "Tunsoare";
            editPricesMenuBeardBtn.text = "Barba";
            editPricesMenuMustacheBtn.text = "Mustata";
            editPricesMenuHairColourBtn.text = "Vopsit";
            editPricesMenuEyebrowBtn.text = "Pensat";
            editPricesMenuInfoTxt.text = "Pentru ce serviciu doresti sa modifici pretul ?";
            editPricesMenuInputPlaceholder.text = "Introdu pretul...";
            editPricesMenuInputUpdateBtn.text = "Actualizeaza";
            editPricesMenuInputBackBtn.text = "Inapoi";
            shopMenuEditDescriptionPlaceholder.text = "Apasa aici pentru a scrie descrierea..";
            shopMenuEditDescriptionUpdateBtn.text = "Actualizeaza";
            editShopMenuWorkingProgramMondayBtn.text = "Luni";
            editShopMenuWorkingProgramTuesdayBtn.text = "Marti";
            editShopMenuWorkingProgramWednesdayBtn.text = "Miercuri";
            editShopMenuWorkingProgramThursdayBtn.text = "Joi";
            editShopmenuWorkingProgramFridayBtn.text = "Vineri";
            editShopMenuWorkingProgramSaturdayBtn.text = "Sambata";
            editShopMenuWorkingProgramSundayBtn.text = "Duminica";
            editShopMenuWorkingProgramInfoTxt.text = "Pentru care zi doresti sa modifici programul de lucru ?";
            editShopMenuWorkingProgramHoursInfoTxt.text = "Alege intre ce ore este deschis salonul";
            editShopMenuWorkingProgramHoursOpensAtTxt.text = "Se deschide \n la ora ->";
            editShopMenuWorkingProgramHoursClosesAtTxt.text = "Se inchide \n la ora ->";
            editShopMenuWorkingProgramHoursUpdateBtn.text = "Actualizeaza";
            editShopMenuPhotosInfoTxt.text = "Alege o poza din telefonul tau";
            editShopMenuSelectPhotosBtn.text = "Alege poza";
            editShopMenuUpdatePhotosBtn.text = "Actualizeaza";
            whatImageToUpload.ClearOptions();
            uploadImageList.Clear();
            ImageAddToList("Ce imagine vrei sa schimbi", "Imaginea 1", "Imaginea 2", "Imaginea 3", "Imaginea 4", "Imaginea 5");
            whatImageToUpload.AddOptions(uploadImageList);
            salonMenuInfoBtn.text = "Informatii";
            salonMenuBarberBtn.text = "Frizeri";
            salonMenuBarberFirstName.text = "Prenume: ";
            salonMenuBarberLastName.text = "Nume: ";
            appointmentMenuCheckHoursBtn.text = "Verifica ora";
            appointmentMenuCheckHoursBackBtn.text = "Inapoi";
            appointmentMenuNextToServicesBtn.text = "Inainte";
            appointmentMenuNextToServicesBackBtn.text = "Inapoi";
            appointmentMenuMentionsInfoTxt.text = "Daca ai informatii/mentiuni pentru frizer scrie-le mai jos";
            appointmentMenuMentionsPlaceholder.text = "Scrie aici..";
            appointmentMenuInsertNameInfoTxt.text = "Introdu mai jos numele care va fi afisat frizerului pentru programare.";
            appointmentMenuInsertNamePlaceholder.text = "Scrie aici...";
            appointmentMenuHaircutServiceBtn.text = "Tunsoare";
            appointmentMenuBeardServiceBtn.text = "Barba";
            appointmentMenuMustacheServiceBtn.text = "Mustata";
            appointmentMenuHairColourBtn.text = "Vopsit";
            appointmentMenuEyebrowBtn.text = "Pensat";
            appointmentMenuBackToHourBtn.text = "Inapoi";
            appointmentMenuCreateAppointmentBtn.text = "Creeaza";
            shopDescription.text = description_ro;
            editDescriptionHint.text = "Selecteaza limba pentru care doresti sa modifici descrierea inainte de a apasa `Actualizeaza`";
            account.ProfileUsername = "Nume utilizator: " + account.AccountUsername;
            account.ProfileFirstName = "Prenume: " + account.GetProfileFirstName;
            account.ProfileLastName = "Nume: " + account.GetProfileLastName;
            for(int i = 0; i < barberList.Count; i++)
            {
               
                barberList[i].GetComponent<Barber>().FirstNameUI = "Prenume: " + firstName[i];
                barberList[i].GetComponent<Barber>().LastNameUI = "Nume: " + lastName[i];
            }

            loadingScreen.SetActive(false);
        }
        else if (selectedLanguage == 2)
        {
            loadingScreen.SetActive(true);
            countyList.Clear();
            countyDropdown.ClearOptions(); //choosing from which location to show shops
            CountyAddToList("Pick a county", "Olt", "Dolj", "Timis");
            cityList.Clear();
            cityDropdown.ClearOptions();
            CityAddToList("Pick a county first");
            cityDropdown.AddOptions(cityList);
            countyDropdown.AddOptions(countyList);
            locationShowShopBTN.text = "Show";
            locationShowShopsInfo.text = "Pick the shop's location.";
            settingsMenuLanguageTxt.text = "Language";
            settingsMenuLoginBtn.text = "Login";
            settingsMenuRegisterBtn.text = "Register";
            settingsMenuManageShopsBtn.text = "Manage Shops";
            settingsMenuBarberMenuBtn.text = "Barber Menu";
            settingsMenuCreateShopBtn.text = "Create shop";
            loginMenuLoginBtn.text = "Login";
            loginMenuBackBtn.text = "Back";
            registerMenuRegisterBtn.text = "Register";
            registerMenuBackBtn.text = "Back";
            registerMenuNextBtn.text = "Next";
            barberMenuTimePerCutTxt.text = "Edit the time needed for you to finish a cut";
            barberMenuTimePerCutBtn.text = "Time per cut";
            barberMenuCheckAppointmentsTxt.text = "Check your appointments";
            barberMenuCheckAppointmentsBtn.text = "Show appointments";
            barberMenuEditPricesTxt.text = "Edit the price of your services";
            barberMenuEditPricesBtn.text = "Edit prices";
            editShopSelectShopTxt.text = "Select which saloon do you want to edit";
            editShopDescriptionBtn.text = "Edit description";
            editShopWorkingProgramBtn.text = "Edit working program";
            editShopPhotosBtn.text = "Edit photos";
            editShopPricesBtn.text = "Edit prices";
            editShopBarbersBtn.text = "Edit barbers";
            timeToCutMenuPlaceholder.text = "Type here...";
            timeToCutMenuSubmitBtn.text = "Update";
            timeToCutMenuInfoTxt.text = "Type in the time needed for you to do a cut. \n The time you need to finish your client's haircut.";
            editPricesMenuHaircutBtn.text = "Haircut";
            editPricesMenuBeardBtn.text = "Beard";
            editPricesMenuMustacheBtn.text = "Mustache";
            editPricesMenuHairColourBtn.text = "Hair Colour";
            editPricesMenuEyebrowBtn.text = "Eyebrows";
            editPricesMenuInfoTxt.text = "For which service do you want to edit the price ?";
            editPricesMenuInputPlaceholder.text = "Enter price...";
            editPricesMenuInputUpdateBtn.text = "Update";
            editPricesMenuInputBackBtn.text = "Back";
            shopMenuEditDescriptionPlaceholder.text = "Click here to write your shop's description.";
            shopMenuEditDescriptionUpdateBtn.text = "Update";
            editShopMenuWorkingProgramMondayBtn.text = "Monday";
            editShopMenuWorkingProgramTuesdayBtn.text = "Tuesday";
            editShopMenuWorkingProgramWednesdayBtn.text = "Wednesday";
            editShopMenuWorkingProgramThursdayBtn.text = "Thursday";
            editShopmenuWorkingProgramFridayBtn.text = "Friday";
            editShopMenuWorkingProgramSaturdayBtn.text = "Saturday";
            editShopMenuWorkingProgramSundayBtn.text = "Sunday";
            editShopMenuWorkingProgramInfoTxt.text = "For which day do you want to edit the working program ?";
            editShopMenuWorkingProgramHoursInfoTxt.text = "Choose the working program of your shop";
            editShopMenuWorkingProgramHoursOpensAtTxt.text = "Opens at";
            editShopMenuWorkingProgramHoursClosesAtTxt.text = "Closes at";
            editShopMenuWorkingProgramHoursUpdateBtn.text = "Update";
            editShopMenuPhotosInfoTxt.text = "Pick a photo from your phone";
            editShopMenuSelectPhotosBtn.text = "Select photo";
            editShopMenuUpdatePhotosBtn.text = "Update";
            whatImageToUpload.ClearOptions();
            uploadImageList.Clear();
            ImageAddToList("Which image to change", "Image 1", "Image 2", "Image 3", "Image 4", "Image 5");
            whatImageToUpload.AddOptions(uploadImageList);
            salonMenuInfoBtn.text = "Informations";
            salonMenuBarberBtn.text = "Barbers";
            salonMenuBarberFirstName.text = "First name: ";
            salonMenuBarberLastName.text = "Last name: ";
            appointmentMenuCheckHoursBtn.text = "Check hours";
            appointmentMenuCheckHoursBackBtn.text = "Back";
            appointmentMenuNextToServicesBtn.text = "Next";
            appointmentMenuNextToServicesBackBtn.text = "Back";
            appointmentMenuMentionsInfoTxt.text = "If you have any information the barber should know about  type it bellow";
            appointmentMenuMentionsPlaceholder.text = "Type here..";
            appointmentMenuInsertNameInfoTxt.text = "Enter below the name that will be shown to the barber for this appointment.";
            appointmentMenuInsertNamePlaceholder.text = "Type here...";
            appointmentMenuHaircutServiceBtn.text = "Haircut";
            appointmentMenuBeardServiceBtn.text = "Beard";
            appointmentMenuMustacheServiceBtn.text = "Mustache";
            appointmentMenuHairColourBtn.text = "Hair colour";
            appointmentMenuEyebrowBtn.text = "Eyebrows";
            appointmentMenuBackToHourBtn.text = "Back";
            appointmentMenuCreateAppointmentBtn.text = "Create";
            shopDescription.text = description_en;
            editDescriptionHint.text = "Select the language of the description you want to edit before pressing `Update`";
            account.ProfileUsername = "Username: " + account.AccountUsername;
            account.ProfileFirstName = "First name: " + account.GetProfileFirstName;
            account.ProfileLastName = "Last name: " + account.GetProfileLastName;
            for (int i = 0; i < barberList.Count; i++)
            {
                barberList[i].GetComponent<Barber>().FirstNameUI = "First name: " + firstName[i];
                barberList[i].GetComponent<Barber>().LastNameUI = "Last name: " + lastName[i];
            }

            loadingScreen.SetActive(false);
        }
    }
    public void SelectSalon()
    {
        pageTitle.text = "Saloon page";
        shopImageNumber = 1;
        getShopImages();
        selectedShopName = EventSystem.current.currentSelectedGameObject.name;
        if(selectedShopName == lastShopSelected)
        {
            shopImageNumber = 1;
        }
        else
        {
            lastShopSelected = selectedShopName;
        }
        ShowShopDescription();
        GetShopWorkingHours();
    }
    public void ShowShops()
    {
        StartCoroutine(ShowShopsEnumerator());
    }
    IEnumerator ShowShopsEnumerator()
    {
        loadingScreen.SetActive(true);
        for(int i=0; i<2; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("whatToPick", whatToPick);
            form.AddField("city", selectedCity);
            form.AddField("county", selectedCounty);

            WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/showShops.php", form);
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
            if(shopList.Count > 0)
            {
                foreach(GameObject shop in shopList)
                {
                    Destroy(shop.gameObject);
                }
                shopList.Clear();
            }
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
        loadingScreen.SetActive(false);
    }
    public void DescriptionToEditEN()
    {
        descriptionToEdit = 2;
        editDescriptionEnBtn.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f);
        editDescriptionRoBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }   
    public void DescriptionToEditRO()
    {
        descriptionToEdit = 1;
        editDescriptionRoBtn.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f);
        editDescriptionEnBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }
    public void EditShopDescription()
    {
        StartCoroutine(EditShopDescriptionEnum());
    }
    IEnumerator EditShopDescriptionEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("description", ShopDescriptionField.text));
        form.Add(new MultipartFormDataSection("shopName", selectedShopToManage.name));
        form.Add(new MultipartFormDataSection("language", descriptionToEdit.ToString()));
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/editshopdescription.php", form);
        yield return webreq.SendWebRequest();
        if (webreq.isNetworkError || webreq.isHttpError)
        {
            Debug.Log(webreq.error);
            if (selectedLanguage == 1)
            {
                errorTXT.text = generalErrorRo;
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = generalErrorEng;
            }
            errorObj.SetActive(true);
        }
        else
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Descrierea salonului a fost modificata cu succes !";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Shop's description changed successfully !";
            }
            errorObj.SetActive(true);
        }
    }
    public void ShowShopDescription()
    {
        StartCoroutine(ShowShopDescriptionEnum());
    }
    IEnumerator ShowShopDescriptionEnum()
    {

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("shopname", selectedShopName));
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/showdescription.php", form);
        yield return webreq.SendWebRequest();
        if(webreq.isHttpError || webreq.isNetworkError)
        {
            if(selectedLanguage == 1)
            {
                errorTXT.text = generalErrorRo;
            }
            else if(selectedLanguage == 2)
            {
                errorTXT.text = generalErrorEng;
            }
            errorObj.SetActive(true);
        }
        else
        {
            description_ro = webreq.downloadHandler.text.Split('\t')[1];
            description_en = webreq.downloadHandler.text.Split('\t')[2];
            if (selectedLanguage == 1)
            {
                shopDescription.text = description_ro;
            }
            else if (selectedLanguage == 2)
            {
                shopDescription.text = description_en;
            }
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
            GameObject openWorkingHourBTN = Instantiate(openWorkingHourPrefab, openWorkingHourPrefab.transform.position, openWorkingHourPrefab.transform.rotation, openWorkingHourPrefab.transform.parent);
            GameObject closeWorkingHourBTN = Instantiate(closeWorkingHourPrefab, closeWorkingHourPrefab.transform.position, closeWorkingHourPrefab.transform.rotation, closeWorkingHourPrefab.transform.parent);
            closeWorkingHourBTN.SetActive(true);
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
            GameObject openWorkingMinuteBTN = Instantiate(openWorkingMinutePrefab, openWorkingMinutePrefab.transform.position, openWorkingMinutePrefab.transform.rotation, openWorkingMinutePrefab.transform.parent);
            GameObject closeWorkingMinuteBTN = Instantiate(closeWorkingMinutePrefab, closeWorkingMinutePrefab.transform.position, closeWorkingMinutePrefab.transform.rotation, closeWorkingMinutePrefab.transform.parent);

            closeWorkingMinuteBTN.SetActive(true);
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
        form.AddField("shopName", selectedShopToManage.name);

        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/editshopworkinghours.php", form);
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
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/getworkinghours.php", form);
        yield return www;
        
        if (www.text[0] != '0')
        {
            Debug.Log(www.text);
        }
        if (selectedLanguage == 1)
        {
            mondayHours.text = "Luni: " + www.text.Split('\t')[1];
            tuesdayHours.text = "Marti: " + www.text.Split('\t')[2];
            wednesdayHours.text = "Miercuri: " + www.text.Split('\t')[3];
            thursdayHours.text = "Joi: " + www.text.Split('\t')[4];
            fridayHours.text = "Vineri: " + www.text.Split('\t')[5];
            saturdayHours.text = "Sambata: " + www.text.Split('\t')[6];
            sundayHours.text = "Duminica: " + www.text.Split('\t')[7];
        }
        else if (selectedLanguage == 2)
        {
            mondayHours.text = "Monday: " + www.text.Split('\t')[1];
            tuesdayHours.text = "Tuesday: " + www.text.Split('\t')[2];
            wednesdayHours.text = "Wednesday: " + www.text.Split('\t')[3];
            thursdayHours.text = "Thursday: " + www.text.Split('\t')[4];
            fridayHours.text = "Friday: " + www.text.Split('\t')[5];
            saturdayHours.text = "Saturday: " + www.text.Split('\t')[6];
            sundayHours.text = "Sunday: " + www.text.Split('\t')[7];
        }
        for (int i = 0; i < 8; i++) // daca nu e nicio ora setata nu se lucreaza in ziua aia
        {
            if (www.text.Split('\t')[i] == null || www.text.Split('\t')[i] == "")
            {
                switch (i)
                {
                    case 1:
                        if(selectedLanguage == 1)
                        {
                            mondayHours.text = "Luni: Inchis.";
                        }
                        else if(selectedLanguage == 2)
                        {
                            mondayHours.text = "Monday: Not working";
                        }
                        break;
                    case 2:
                        if (selectedLanguage == 1)
                        {
                            tuesdayHours.text = "Marti: Inchis.";
                        }
                        else if (selectedLanguage == 2)
                        {
                            tuesdayHours.text = "Tuesday: Not working";
                        }
                        break;
                    case 3:
                        if (selectedLanguage == 1)
                        {
                            wednesdayHours.text = "Miercuri: Inchis.";
                        }
                        else if (selectedLanguage == 2)
                        {
                            wednesdayHours.text = "Wednesday: Not working";
                        }
                        break;
                    case 4:
                        if (selectedLanguage == 1)
                        {
                            thursdayHours.text = "Joi: Inchis.";
                        }
                        else if (selectedLanguage == 2)
                        {
                            thursdayHours.text = "Thursday: Not working";
                        }
                        break;
                    case 5:
                        if (selectedLanguage == 1)
                        {
                            fridayHours.text = "Vineri: Inchis.";
                        }
                        else if (selectedLanguage == 2)
                        {
                            fridayHours.text = "Friday: Not working";
                        }
                        break;
                    case 6:
                        if (selectedLanguage == 1)
                        {
                            saturdayHours.text = "Sambata: Inchis.";
                        }
                        else if (selectedLanguage == 2)
                        {
                            saturdayHours.text = "Saturday: Not working";
                        }
                        break;
                    case 7:
                        if (selectedLanguage == 1)
                        {
                            sundayHours.text = "Duminica: Inchis.";
                        }
                        else if (selectedLanguage == 2)
                        {
                            sundayHours.text = "Sunday: Not working";
                        }
                        break;
                }
            }
        }
    }
    public void nextShopImage()
    {
        if (shopImageNumber < 5)
        {
        shopImageNumber++;
        }
        setimagine();
    }
    public void previousShopImage()
    {
        if(shopImageNumber > 1)
        {
        shopImageNumber--;
        }
        setimagine();
    }
    public void setimagine()
    {
        shopPhoto.sprite = Sprite.Create(shopImage[shopImageNumber], new Rect(0, 0, shopImage[shopImageNumber].width, shopImage[shopImageNumber].height), new Vector2(0, 0));
    }
    public void getShopImages()
    {
        StartCoroutine(GetShopImages());
    }
    IEnumerator GetShopImages()
    {
        for(int i =0; i<6; i++)
        {
            loadingScreen.SetActive(true);  //activeaza loading screenu
            WWW www = new WWW("http://mybarber.vlcapps.com/shop_photos/" + selectedShopName + "/image" + i + ".jpg");
            WWW w = new WWW("http://mybarber.vlcapps.com/shop_photos/" + selectedShopName + "/image" + i + ".png");
            yield return www;
            yield return w;
            if (isFailedImage(www.texture)) // check if the image is the question mark
            {
                shopImage[i] = w.texture; //set the texture to a variable
                if (i >= 5) //if the loop got the 5th photo close the loading screen and set the shown image to 1
                {
                    shopPhoto.sprite = Sprite.Create(shopImage[shopImageNumber], new Rect(0, 0, shopImage[shopImageNumber].width, shopImage[shopImageNumber].height), new Vector2(0, 0));
                    loadingScreen.SetActive(false);
                    shopMenu.SetActive(true);
                }
            }
            else
            {
                shopImage[i] = www.texture; //set the texture to a variable
                if (i >= 5) //if the loop got the 5th photo close the loading screen and set the shown image to 1
                {
                    shopPhoto.sprite = Sprite.Create(shopImage[shopImageNumber], new Rect(0, 0, shopImage[shopImageNumber].width, shopImage[shopImageNumber].height), new Vector2(0, 0));
                    loadingScreen.SetActive(false);
                    shopMenu.SetActive(true);
                }
            }
        }
    }
    private bool isFailedImage(Texture tex) // to check if the downloaded image is equal to the question mark image(when it fails loading)
    {
        if (!tex) return true;

        byte[] png1 = (tex as Texture2D).EncodeToPNG(); // encode the downloaded texture to bytes
        // bytes representation of the question mark image
        byte[] questionMarkPNG = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 2, 0, 0, 0, 75, 109, 41, 220, 0, 0, 0, 65, 73, 68, 65, 84, 8, 29, 85, 142, 81, 10, 0, 48, 8, 66, 107, 236, 254, 87, 110, 106, 35, 172, 143, 74, 243, 65, 89, 85, 129, 202, 100, 239, 146, 115, 184, 183, 11, 109, 33, 29, 126, 114, 141, 75, 213, 65, 44, 131, 70, 24, 97, 46, 50, 34, 72, 25, 39, 181, 9, 251, 205, 14, 10, 78, 123, 43, 35, 17, 17, 228, 109, 164, 219, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130, };

        return Equivalent(png1, questionMarkPNG); //return if they are the same or not
    }
    public bool Equivalent(byte[] bytes1, byte[] bytes2) // check 2 arrays of bytes to see if they are the same or not
    {
        if (bytes1.Length != bytes2.Length) return false;
        for (int i = 0; i < bytes1.Length; i++)
            if (!bytes1[i].Equals(bytes2[i])) return false;
        return true;
    }
    public void uploadImage()
    {
        StartCoroutine(StartUploading());
    }
    IEnumerator StartUploading()
    {
        WWWForm form = new WWWForm();
        byte[] textureBytes = null;

        //Get a copy of the texture, because we can't access original texure data directly. 
        Texture2D photoTexture = GetTextureCopy(imageFromPhone);
        textureBytes = photoTexture.EncodeToJPG();
        string imageName = "image" + uploadedImageNumber + ".jpg";
        form.AddBinaryData("myimage", textureBytes, imageName, "imagebro.jpg");
        form.AddField("shopName", selectedShopToManage.name);

        WWW w = new WWW("http://mybarber.vlcapps.com/appscripts/uploadimage.php", form);

        yield return w;
        Debug.Log(w.text);
        w.Dispose();
    }

    Texture2D GetTextureCopy(Texture2D source)
    {
        //Create a RenderTexture
        RenderTexture rt = RenderTexture.GetTemporary(
                               source.width,
                               source.height,
                               0,
                               RenderTextureFormat.Default,
                               RenderTextureReadWrite.Linear
                           );

        //Copy source texture to the new render (RenderTexture) 
        Graphics.Blit(source, rt);

        //Store the active RenderTexture & activate new created one (rt)
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        //Create new Texture2D and fill its pixels from rt and apply changes.
        Texture2D readableTexture = new Texture2D(source.width, source.height);
        readableTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readableTexture.Apply();

        //activate the (previous) RenderTexture and release texture created with (GetTemporary( ) ..)
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return readableTexture;
    }
    public void PickPhoneImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image

                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                imageFromPhone = texture; // poza de upload sa fie textura luata din telefon
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }
    public void WhatImageToUpload(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 1: uploadedImageNumber = 1;
                break;
            case 2: uploadedImageNumber = 2;
                break;
            case 3: uploadedImageNumber = 3;
                break;
            case 4: uploadedImageNumber = 4;
                break;
            case 5: uploadedImageNumber = 5;
                break;
        }
    }
    public void GetShopsOfUser()
    {
        StartCoroutine(GetShopsOfUserEnum());
    }
    IEnumerator GetShopsOfUserEnum()
    {
        for(int i = 1; i <4; i++)
        {
            List<IMultipartFormSection> newform = new List<IMultipartFormSection>();
            newform.Add(new MultipartFormDataSection("username", account.AccountUsername));
            newform.Add(new MultipartFormDataSection("whatData", i.ToString()));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getshopsofuser.php", newform);
            yield return webreq.SendWebRequest();
            if (webreq.isNetworkError || webreq.isHttpError)
            {
                Debug.Log(webreq.error);
            }
            else
            {
                if(i == 1)
                {
                    shopsOfUser = webreq.downloadHandler.text.Split('\t');
                    Debug.Log(webreq.downloadHandler.text);
                }
                else if(i == 2)
                {
                    shopsOfUserAddress = webreq.downloadHandler.text.Split('\t');
                    Debug.Log("ADRESS = " + webreq.downloadHandler.text);
                }
                else if(i == 3)
                {
                    shopsOfUserCity = webreq.downloadHandler.text.Split('\t');
                }
            }
        }
    }
    public void CheckBarberCanEditPrices()
    {
        StartCoroutine(CheckBarberCanEditPricesEnum());
    }
    IEnumerator CheckBarberCanEditPricesEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("shopName", account.WorksAt));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/checkbarbereditprice.php", form);
        yield return web.SendWebRequest();
        if(web.downloadHandler.text == "0")
        {
            EditBarberPricesBTN.SetActive(false);
        }
        else
        {
            EditBarberPricesBTN.SetActive(true);
        }
    }
    public void SelectShopToManage()
    {
        selectedShopToManage = EventSystem.current.currentSelectedGameObject.gameObject;
        StartCoroutine(CheckForShopGeneralPrice());
    }
    IEnumerator CheckForShopGeneralPrice()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("shopName", selectedShopToManage.name));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/hasgeneralprice.php", form);
        yield return web.SendWebRequest();
        if(web.downloadHandler.text == "0")
        {
            shopGeneralPrice = false;
            EditGeneralPricesBTN.SetActive(false);
        }
        else
        {
            shopGeneralPrice = true;
            EditGeneralPricesBTN.SetActive(true);
        }
    }
    public void CreateShopsToManage()
    {
        if(shopsOfUserList.Count > 0)
        {
            foreach(GameObject obj in shopsOfUserList)
            {
                Destroy(obj);
            }
            shopsOfUserList.Clear();
        }
        for(int i=0; i<shopsOfUser.Length -1; i++)
        {
            GameObject shopToManage = Instantiate(manageShopPrefab, manageShopPrefab.transform.position, manageShopPrefab.transform.rotation, manageShopPrefab.transform.parent);
            shopToManage.name = shopsOfUser[i];
            manageShopNameTXT = shopToManage.transform.Find("ShopName").transform.Find("Name").GetComponent<TextMeshProUGUI>();
            manageShopNameTXT.text = shopsOfUser[i];
            manageShopAddressTXT = shopToManage.transform.Find("ShopAddress").transform.Find("Address").GetComponent<TextMeshProUGUI>();
            manageShopAddressTXT.text = shopsOfUserAddress[i];
            manageShopCityTXT = shopToManage.transform.Find("ShopCity").transform.Find("City").GetComponent<TextMeshProUGUI>();
            manageShopCityTXT.text = shopsOfUserCity[i];
            shopToManage.SetActive(true);
            shopsOfUserList.Add(shopToManage);
        }
    }
    public void CreateShopCountyPicked(Dropdown createShopCounty)
    {
        switch (createShopCounty.value)
        {
            case 1:
                createShopCityDropdown.interactable = true;
                if (selectedLanguage == 1)
                {
                    List<string> cityOltOptions = new List<string> { "Alege un oras", "Caracal", "Farcasele", "Slatina", "Corabia" };
                    createShopCityDropdown.AddOptions(cityOltOptions);                    
                    createShopCityDropdown.ClearOptions();
                }
                else if (selectedLanguage == 2)
                {
                    List<string> cityOltOptions = new List<string> { "Pick a city", "Caracal", "Farcasele", "Slatina", "Corabia" };
                    createShopCityDropdown.ClearOptions();
                    createShopCityDropdown.AddOptions(cityOltOptions);
                }
                break;
            case 2:
                createShopCityDropdown.interactable = true;
                createShopCityDropdown.ClearOptions();
                if (selectedLanguage == 1)
                {
                    List<string> cityDoljOptions = new List<string> { "Alege un oras", "Craiova", "Radovan", "Gogosu", "Gura Racului" };
                    createShopCityDropdown.AddOptions(cityDoljOptions);
                }
                else if (selectedLanguage == 2)
                {
                    List<string> cityDoljOptions = new List<string> { "Pick a city","Craiova", "Radovan", "Gogosu", "Gura Racului" };
                    createShopCityDropdown.AddOptions(cityDoljOptions);
                }
                break;
        }
    }
    public void CityPicked(Dropdown cityDropdown)
    {
        if (selectedCounty == countyList[1])
        {
            switch (cityDropdown.value)
            {
                case 1:
                    selectedCity = cityList[1];
                    break;
                case 2:
                    selectedCity = cityList[2];
                    break;
                case 3:
                    selectedCity = cityList[3];
                    break;
                case 4:
                    selectedCity = cityList[4];
                    break;
            }
        }
        else if (selectedCounty == countyList[2])
        {
            switch (cityDropdown.value)
            {
                case 1:
                    selectedCity = cityList[1];
                    break;
                case 2:
                    selectedCity = cityList[2];
                    break;
                case 3:
                    selectedCity = cityList[3];
                    break;
                case 4: 
                    selectedCity = cityList[4];
                    break;
                case 5:
                    selectedCity = cityList[5];
                    break;
            }
        }
    }
    public void CountyPicked(Dropdown countyPicked)
    {
        switch (countyPicked.value)
        {
            case 1:
                cityDropdown.interactable = true;
                cityList.Clear();
                if(selectedLanguage == 1)
                {
                    CityAddToList("Alege un oras", "Caracal", "Slatina", "Grojdibodu", "Conacu Piatra-Olt");
                    cityDropdown.ClearOptions();
                    cityDropdown.AddOptions(cityList);
                    selectedCounty = countyList[1];
                }
                else if(selectedLanguage == 2)
                {
                    CityAddToList("Pick a city", "Caracal", "Slatina", "Grojdibodu", "Conacu Piatra-Olt");
                    cityDropdown.ClearOptions();
                    cityDropdown.AddOptions(cityList);
                    selectedCounty = countyList[1];
                }
                break;
            case 2:
                cityDropdown.interactable = true;
                cityList.Clear();
                if (selectedLanguage == 1)
                {
                    CityAddToList("Alege un oras", "Craiova", "Radovan", "Gura Racului", "Gogosu");
                    cityDropdown.ClearOptions();
                    cityDropdown.AddOptions(cityList);
                    selectedCounty = countyList[2];
                }
                else if (selectedLanguage == 2)
                {
                    CityAddToList("Pick a city", "Craiova", "Radovan", "Gura Racului", "Gogosu");
                    cityDropdown.ClearOptions();
                    cityDropdown.AddOptions(cityList);
                    selectedCounty = countyList[2];
                }
                break;
                
        }
    }
}
