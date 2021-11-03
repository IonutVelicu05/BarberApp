using System;
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
    [SerializeField] private GameObject adminMenu;
    [SerializeField] private GameObject registerButton;
    [SerializeField] private GameObject registerMenu;
    /* application menu state. ( which menu is opened at a specific time ) so when the user press the back button on his phone the menu will change to the earlier one.
     * state 0 = main menu (shop list menu / the one that is shown when the app opens );
     * state 1 = setting menu 1 ( the whole settings menu, the one shown when the settings button is pressed )
     * state 1.01 = login menu
     * state 1.02 = register menu first fields.
     * state 1.03 = register menu second fields.
     * state 1.04 = create shop menu (name & address)
     * state 1.05 = create shop menu (location)
     * state 1.06 = barber menu
     * state 1.07 = barber time to cut
     * state 1.08 = barber appointments calendar.
     * state 1.09 = barber appointments list. (after selecting a day from the calnedar.)
     * state 1.10 = manage shops menu ( shop list )
     * state 1.11 = manage shops menu ( the menu after selecting a shop from the list )
     * state 1.12 = edit description menu
     * state 1.13 = edit working program menu ( days list )
     * state 1.14 = edit working program menu ( selecting hours & minutes )
     * state 1.15 = edit employees menu
     * state 1.16 = edit employees menu ( add employee )
     * state 1.17 = edit employees menu ( add employee info )
     * state 1.18 = edit services menu 
     * state 1.19 = edit services menu ( add service )
     * state 1.20 = edit services menu (add service info )
     * state 1.21 = edit services menu ( after selecting a service )
     * state 1.22 = edit shop logo menu
     * 
     * state 2.00 = shop menu ( info and working program texts are active )
     * state 2.01 = shop menu barber scrollview is active
     * state 2.10 = appointment calendar
     * state 2.11 = appoinmennt hour & minute selection
     * state 2.12 = appointment services selection
     * 
     * 
     */
    private float applicationMenuState;
    private event EventHandler OnBackButtonPressed;
    [SerializeField] private GameObject registerMenuFirstFields;
    [SerializeField] private GameObject registerMenuSecondFields;
    [SerializeField] private GameObject createShopMenuFirstFields;
    [SerializeField] private GameObject createShopMenuSecondFields;
    [SerializeField] private GameObject barberMenu;
    [SerializeField] private GameObject barberTimeToCutMenu;
    [SerializeField] private GameObject barberAppointmentsCalendar;
    [SerializeField] private GameObject barberAppointmentsList;
    [SerializeField] private GameObject manageShopMenuList;
    [SerializeField] private GameObject manageShopMenu;
    [SerializeField] private GameObject editDescriptionMenu;
    [SerializeField] private GameObject editWorkingProgramMenuDayList;
    [SerializeField] private GameObject editWorkingProgramMenuHMList;
    [SerializeField] private GameObject editEmployeesMenu;
    [SerializeField] private GameObject editEmployeesMenuAddEmployee;
    [SerializeField] private GameObject editEmployeesMenuAddEmployeeInfo;
    [SerializeField] private GameObject editServicesMenu;
    [SerializeField] private GameObject editServicesMenuAddService;
    [SerializeField] private GameObject editServicesMenuAddServiceInfo;
    [SerializeField] private GameObject editServicesMenuServiceSelected;
    [SerializeField] private GameObject editShopLogoMenu;
    [SerializeField] private GameObject shopMenuBarberList;
    [SerializeField] private GameObject shopMenuInfoList;
    [SerializeField] private GameObject appointmentCalendar;
    [SerializeField] private GameObject appointmentHMList;
    [SerializeField] private GameObject appointmentServiceList;
    [SerializeField] private GameObject shopListScrollView;
    [SerializeField] private GameObject appointmentDayList;

    //end of back button state
    [SerializeField] private GameObject barberMenuBTN;
    [SerializeField] private GameObject backBTN;
    [SerializeField] private GameObject profileMenu;
    [SerializeField] private Image profilePicture;
    private Texture2D profilePictureTexture;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject locationPickMenu;
    private Account account;
    private Appointments appointmentsClass;
    [SerializeField] private TextMeshProUGUI pageTitle;
    [SerializeField] private GameObject logoutBtn;
    [SerializeField] private Image homepageImage;
    [SerializeField] private Image calendarImage;
    //translation

    private int selectedLanguage = 1; // 1 = romana ;; 2 = engleza
    public int SelectedLanguage
    {
        get { return selectedLanguage; }
        set { selectedLanguage = value; }
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
    [SerializeField] private TextMeshProUGUI logoutBtnTxt;
    [SerializeField] private TextMeshProUGUI barberMenuTimePerCutTxt;
    [SerializeField] private TextMeshProUGUI barberMenuTimePerCutBtn;
    [SerializeField] private TextMeshProUGUI barberMenuCheckAppointmentsTxt;
    [SerializeField] private TextMeshProUGUI barberMenuCheckAppointmentsBtn;
    [SerializeField] private TextMeshProUGUI editShopSelectShopTxt;
    [SerializeField] private TextMeshProUGUI editShopDescriptionBtn;
    [SerializeField] private TextMeshProUGUI editShopWorkingProgramBtn;
    [SerializeField] private TextMeshProUGUI editShopPhotosBtn;
    [SerializeField] private TextMeshProUGUI editShopBarbersBtn;
    [SerializeField] private TextMeshProUGUI editShopServicesBtn;
    [SerializeField] private TextMeshProUGUI editShopLogoImageBtn;
    [SerializeField] private Text timeToCutMenuPlaceholder;
    [SerializeField] private TextMeshProUGUI timeToCutMenuSubmitBtn;
    [SerializeField] private TextMeshProUGUI timeToCutMenuInfoTxt;
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
    [SerializeField] private TextMeshProUGUI appointmentMenuCheckHoursBackBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuNextToServicesBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuNextToServicesBackBtn;
    [SerializeField] private TextMeshProUGUI appointmentMenuMentionsInfoTxt;
    [SerializeField] private Text appointmentMenuMentionsPlaceholder;
    [SerializeField] private TextMeshProUGUI appointmentMenuCreateAppointmentBtn;
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
    private List<GameObject> openWorkingHourList = new List<GameObject>();
    private List<GameObject> closeWorkingHourList = new List<GameObject>();
    private List<GameObject> openWorkingMinuteList = new List<GameObject>();
    private List<GameObject> closeWorkingMinuteList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI mondayHours;
    [SerializeField] private TextMeshProUGUI tuesdayHours;
    [SerializeField] private TextMeshProUGUI wednesdayHours;
    [SerializeField] private TextMeshProUGUI thursdayHours;
    [SerializeField] private TextMeshProUGUI fridayHours;
    [SerializeField] private TextMeshProUGUI saturdayHours;
    [SerializeField] private TextMeshProUGUI sundayHours;
    [SerializeField] private Image shopPhoto; //the photo user is currently seeing
    [SerializeField] private Sprite shopPhotoNull;
    private Texture2D[] shopImage = new Texture2D[6];
    private Texture2D[] logoImage;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private InputField imageFromPhoneName;
    [SerializeField] private Image selectedLogoImage;
    [SerializeField] private Dropdown whatImageToUpload;
    private string description_ro;
    private string description_en;
    private int descriptionToEdit = 1; // 1 = descrierea in romana ;; 2 = descrierea in engleza
    [SerializeField] private GameObject editDescriptionRoBtn;
    [SerializeField] private GameObject editDescriptionEnBtn;
    //SHOP

    [SerializeField] private GameObject countyPrefab;
    [SerializeField] private GameObject cityPrefab;
    [SerializeField] private GameObject countyScrollview;
    [SerializeField] private GameObject cityScrollview;
    [SerializeField] private GameObject pickCountyBTN;
    [SerializeField] private GameObject pickCityBTN;
    private List<GameObject> countyObjectList = new List<GameObject>();
    private List<GameObject> cityObjectList = new List<GameObject>();
    private string selectedCounty;
    private string selectedCity;
    private List<string> countyList = new List<string>();
    private List<string> cityList = new List<string>();
    private int uploadedImageNumber; //the number of the uploading image
    private int shopImageNumber = 1;
    private string shopImageUrlPng;
    private string shopImageUrlJpg;
    private List<string> uploadImageList = new List<string>();
    //the shop; sets shopImageNumber to 1;
    private Texture2D imageFromPhone;
    private Texture2D logoFromPhone;
    [SerializeField] private TextMeshProUGUI imageNumberTxt;
    [SerializeField] private GameObject loadingScreenImages;
    [SerializeField] private GameObject noImageLoadedErrorObj;
    [SerializeField] private TextMeshProUGUI noImageLoadedErrorTxt;

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
    // ~~ end of manage shop ~~


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
    private string[] barberUsername;
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

    //create employees
    [SerializeField] private InputField employeePersonalCodeInput;
    [SerializeField] private GameObject employeeInfoBG;
    [SerializeField] private TextMeshProUGUI employeeInfoTxt;
    [SerializeField] private GameObject addEmployeeBTN;
    [SerializeField] private GameObject deleteEmployeeBTN;
    [SerializeField] private GameObject employeePrefab;
    [SerializeField] private GameObject deleteQuestionObject;
    [SerializeField] private TextMeshProUGUI deleteEmployeeQuestion;
    private List<GameObject> employeeObjectList = new List<GameObject>();
    private string[] employeeFirstNameList;
    private string[] employeeLastNameList;
    private string[] employeePersonalCodeList;
    private string selectedEmployee;
    private bool deleteEmployee = false;
    private Color deleteEmployeeNormalColor = new Color(0.85f, 0.4f, 0.4f, 1f);
    private Color deleteEmployeeSelectedColor = new Color(0.6f, 0.24f, 0.24f, 1f);

    //create service
    [SerializeField] private InputField createServiceNameInputRO;
    [SerializeField] private InputField createServiceNameInputEN;
    [SerializeField] private InputField createServicePriceInput;
    [SerializeField] private InputField createServiceCoinInput;
    [SerializeField] private GameObject createServicePrefab;
    [SerializeField] private TextMeshProUGUI serviceActualPrice;
    [SerializeField] private InputField editServicePriceInput;
    [SerializeField] private TextMeshProUGUI createServiceRoNameInfo;
    [SerializeField] private TextMeshProUGUI createServiceEnNameInfo;
    [SerializeField] private TextMeshProUGUI createServicePriceInfo;
    [SerializeField] private TextMeshProUGUI serviceInfoRoNameInput;
    [SerializeField] private TextMeshProUGUI serviceInfoEnNameInput;
    [SerializeField] private TextMeshProUGUI serviceInfoPriceInput;
    [SerializeField] private GameObject createServiceInfoBG;
    private List<GameObject> createServiceObjectList = new List<GameObject>();
    private string[] createServiceNameListRO;
    private string[] createServiceNameListEN;
    private string[] createServicePriceList;
    private string[] createServiceCoinList;
    private int createServicePrice;
    private string createServiceNameRO;
    private string createServiceNameEN;
    private string serviceName;
    [SerializeField] private GameObject deleteServiceQuestion;
    [SerializeField] private GameObject createServiceBTN;
    [SerializeField] private GameObject deleteServiceBTN;
    [SerializeField] private TextMeshProUGUI deleteServiceQuestionText;
    [SerializeField] private GameObject serviceInputPriceBG;
    private bool deleteService = false;
    private Color deleteServiceNormalColor = new Color(0.85f, 0.4f, 0.4f, 1f);
    private Color DeleteServiceSelectedColor = new Color(0.6f, 0.24f, 0.24f, 1f);



    public void PickACounty()
    {
        if (countyObjectList.Count > 0)
        {
            foreach (GameObject obj in countyObjectList)
            {
                Destroy(obj);
            }
            countyObjectList.Clear();
        }
        for (int i = 0; i < countyList.Count; i++)
        {
            GameObject county = Instantiate(countyPrefab, countyPrefab.transform.parent);
            county.name = countyList[i];
            county.gameObject.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = countyList[i];
            county.SetActive(true);
            countyObjectList.Add(county);
        }
        countyScrollview.SetActive(!countyScrollview.activeInHierarchy);
    }
    public void PickACity()
    {
        if (cityObjectList.Count > 0)
        {
            foreach (GameObject obj in cityObjectList)
            {
                Destroy(obj);
            }
            cityObjectList.Clear();
        }
        for (int i = 0; i < cityList.Count; i++)
        {
            GameObject city = Instantiate(cityPrefab, cityPrefab.transform.parent);
            city.name = cityList[i];
            city.gameObject.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = cityList[i];
            city.SetActive(true);
            cityObjectList.Add(city);
        }
        cityScrollview.SetActive(!cityScrollview.activeInHierarchy);
    }
    public void SelectCity()
    {
        selectedCity = EventSystem.current.currentSelectedGameObject.name;
        cityScrollview.SetActive(!cityScrollview.activeInHierarchy);
        pickCityBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = selectedCity;
    }
    public void SelectCounty()
    {
        selectedCounty = EventSystem.current.currentSelectedGameObject.name;
        countyScrollview.SetActive(!countyScrollview.activeInHierarchy);
        pickCountyBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = selectedCounty;
        GetDatabaseCities();
        pickCityBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = "Pick a city";
    }
    public void ShowCreateServiceInfo()
    {
        createServiceInfoBG.SetActive(!createServiceInfoBG.activeInHierarchy);
    }

    public void ShowCreateEmployeeInfo()
    {
        employeeInfoBG.SetActive(!employeeInfoBG.activeInHierarchy);
    }
    public void GetDatabaseCounties()
    {
        StartCoroutine(GetDatabaseCountiesEnum());
    }
    IEnumerator GetDatabaseCountiesEnum()
    {
        UnityWebRequest webreq = UnityWebRequest.Get("http://mybarber.vlcapps.com/appscripts/getcounties.php");
        yield return webreq.SendWebRequest();
        countyList.Clear();
        for (int i = 0; i < webreq.downloadHandler.text.Split('\t').Length - 1; i++)
        {
            countyList.Add(webreq.downloadHandler.text.Split('\t')[i]);
        }
        webreq.Dispose();
    }
    public void GetDatabaseCities()
    {
        StartCoroutine(GetDatabaseCitiesEnum());
    }
    IEnumerator GetDatabaseCitiesEnum()
    {
        loadingScreen.SetActive(true);
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("county", selectedCounty));
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getcities.php", form);
        yield return webreq.SendWebRequest();
        cityList.Clear();
        Debug.Log(webreq.downloadHandler.text);
        for (int i = 0; i < webreq.downloadHandler.text.Split('\t').Length - 1; i++)
        {
            cityList.Add(webreq.downloadHandler.text.Split('\t')[i]);
        }
        pickCityBTN.GetComponent<Button>().interactable = true;
        webreq.Dispose();
        loadingScreen.SetActive(false);
    }
    public void OpenProfileMenu()
    {
        if (shopList.Count < 1)
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
    public void DeleteEmployeeBTN()
    {
        deleteEmployee = !deleteEmployee;
        if (deleteEmployee)
        {
            deleteEmployeeBTN.GetComponent<Image>().color = deleteEmployeeSelectedColor;
        }
        else
        {
            deleteEmployeeBTN.GetComponent<Image>().color = deleteEmployeeNormalColor;
        }
    }
    public void DeleteServiceBTN()
    {
        deleteService = !deleteService;
        if (deleteService)
        {
            deleteServiceBTN.GetComponent<Image>().color = DeleteServiceSelectedColor;
        }
        else
        {
            deleteServiceBTN.GetComponent<Image>().color = deleteServiceNormalColor;
        }
    }
    public void Start()
    {
        applicationMenuState = 0;
        GetDatabaseCounties();
        account = gameObject.GetComponent<Account>();
        appointmentsClass = gameObject.GetComponent<Appointments>();
        updateLanguageTexts();

        createShopCountyDropdown.ClearOptions();
        createShopCountyDropdown.AddOptions(countyList);

        afterLogin();

        createShopCountyDropdown.onValueChanged.AddListener(delegate { CreateShopCountyPicked(createShopCountyDropdown); });
        whatImageToUpload.onValueChanged.AddListener(delegate { WhatImageToUpload(whatImageToUpload); });
        OnBackButtonPressed += BackButtonPressed;

    }
    public void ChangeApplicationMenuState(float state)
    {
        applicationMenuState = state;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
    private void BackButtonPressed(object sender, EventArgs e)
    {
        Debug.Log("event fired functie");
        switch (applicationMenuState)
        {
            case 0:
                Debug.Log("quit app");
                Application.Quit();
                break;
            case 1:
                settingsMenu.SetActive(false);
                applicationMenuState = 0;
                break;
            case (float)1.01:
                applicationMenuState = 1;
                afterLogin();
                loginMenu.SetActive(false);
                break;
            case (float)1.02:
                applicationMenuState = 1;
                afterLogin();
                registerMenu.SetActive(false);
                break;
            case (float)1.03:
                applicationMenuState = (float)1.02;
                afterLogin();
                registerMenuSecondFields.SetActive(false);
                break;
            case (float)1.04:
                applicationMenuState = 1;
                afterLogin();
                createShopMenuFirstFields.SetActive(false);
                break;
            case (float)1.05:
                applicationMenuState = (float)1.04;
                createShopMenuSecondFields.SetActive(false);
                break;
            case (float)1.06:
                applicationMenuState = 1;
                afterLogin();
                barberMenu.SetActive(false);
                break;
            case (float)1.07:
                applicationMenuState = (float)1.06;
                barberMenu.SetActive(true);
                barberTimeToCutMenu.SetActive(false);
                break;
            case (float)1.08:
                applicationMenuState = (float)1.06;
                barberMenu.SetActive(true);
                barberAppointmentsCalendar.SetActive(false);
                break;
            case (float)1.09:
                applicationMenuState = (float)1.08;
                barberAppointmentsCalendar.SetActive(true);
                barberAppointmentsList.SetActive(false);
                break;
            case (float)1.10:
                applicationMenuState = 1;
                afterLogin();
                manageShopMenuList.SetActive(false);
                break;
            case (float)1.11:
                applicationMenuState = (float)1.10;
                manageShopMenuList.SetActive(true);
                manageShopMenu.SetActive(false);
                break;
            case (float)1.12:
                applicationMenuState = (float)1.11;
                editDescriptionMenu.SetActive(false);
                manageShopMenu.SetActive(true);
                break;
            case (float)1.13:
                applicationMenuState = (float)1.11;
                editWorkingProgramMenuDayList.SetActive(false);
                manageShopMenu.SetActive(true);
                break;
            case (float)1.14:
                applicationMenuState = (float)1.13;
                editWorkingProgramMenuHMList.SetActive(false);
                editWorkingProgramMenuDayList.SetActive(true);
                break;
            case (float)1.15:
                applicationMenuState = (float)1.11;
                editEmployeesMenu.SetActive(false);
                manageShopMenu.SetActive(true);
                break;
            case (float)1.16:
                applicationMenuState = (float)1.15;
                editEmployeesMenuAddEmployee.SetActive(false);
                editEmployeesMenu.SetActive(true);
                break;
            case (float)1.17:
                applicationMenuState = (float)1.16;
                editEmployeesMenuAddEmployeeInfo.SetActive(false);
                editEmployeesMenuAddEmployee.SetActive(true);
                break;
            case (float)1.18:
                applicationMenuState = (float)1.11;
                editServicesMenu.SetActive(false);
                manageShopMenu.SetActive(true);
                break;
            case (float)1.19:
                applicationMenuState = (float)1.19;
                editServicesMenuAddService.SetActive(false);
                editServicesMenu.SetActive(true);
                break;
            case (float)1.20:
                applicationMenuState = (float)1.19;
                editServicesMenuAddServiceInfo.SetActive(false);
                editServicesMenuAddService.SetActive(true);
                break;
            case (float)1.21:
                editServicesMenuServiceSelected.SetActive(false);
                editServicesMenu.SetActive(true);
                break;
            case (float)1.22:
                applicationMenuState = (float)1.11;
                editShopLogoMenu.SetActive(false);
                manageShopMenu.SetActive(true);
                break;
            case (float)2.00:
                applicationMenuState = 0;
                shopMenu.SetActive(false);
                shopListScrollView.SetActive(true);
                break;
            case (float)2.01:
                applicationMenuState = (float)2.00;
                shopMenuBarberList.SetActive(false);
                shopMenuInfoList.SetActive(true);
                break;
            case (float)2.10:
                applicationMenuState = (float)2.01;
                shopMenuBarberList.SetActive(true);
                shopMenu.SetActive(true);
                appointmentCalendar.SetActive(false);
                break;
            case (float)2.11:
                applicationMenuState = (float)2.10;
                appointmentHMList.SetActive(false);
                appointmentCalendar.SetActive(true);
                appointmentDayList.SetActive(true);
                break;
            case (float)2.12:
                applicationMenuState = (float)2.11;
                appointmentServiceList.SetActive(false);
                appointmentHMList.SetActive(true);
                break;
        }
    }
    public int GetTimeToCut()
    {
        return timeToCut;
    }
    public void SelectRomanianLanguage()
    {
        selectedLanguage = 1;
        account.Save();
        updateLanguageTexts();
    }
    public void SelectEnglishLanguage()
    {
        selectedLanguage = 2;
        account.Save();
        updateLanguageTexts();
    }
    public string GetSelectedShopName()
    {
        return selectedShopName;
    }

    public void showBarbers()
    {
        if(selectedShopName != lastShopSelected)
        {
            StartCoroutine(ShowBarbersEnumerator());
            Debug.Log("arat frizeru");
        }
    }
    IEnumerator ShowBarbersEnumerator()
    {
        loadingScreen.SetActive(true);
        for (int i = 0; i < 9; i++)
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
                    www.Dispose();
                    break;
                case 2:
                    lastName = www.text.Split('\t');
                    whatToPickBarber = 3;
                    www.Dispose();
                    break;
                case 3:
                    fiveStarReviews = www.text.Split('\t');
                    whatToPickBarber = 4;
                    www.Dispose();
                    break;
                case 4:
                    fourStarReviews = www.text.Split('\t');
                    whatToPickBarber = 5;
                    www.Dispose();
                    break;
                case 5:
                    threeStarReviews = www.text.Split('\t');
                    whatToPickBarber = 6;
                    www.Dispose();
                    break;
                case 6:
                    twoStarReviews = www.text.Split('\t');
                    whatToPickBarber = 7;
                    www.Dispose();
                    break;
                case 7:
                    oneStarReviews = www.text.Split('\t');
                    whatToPickBarber = 8;
                    www.Dispose();
                    break;
                case 8:
                    barberUsername = www.text.Split('\t');
                    whatToPickBarber = 1;
                    www.Dispose();
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
        for (int j = 0; j < lastName.Length - 1; j++)
        {
            GameObject barber = Instantiate(barberPrefab);
            barber.name = barberUsername[j] + "\t" + firstName[j] + "\t" + lastName[j];
            if (selectedLanguage == 1)
            {
                barber.GetComponent<Barber>().FirstNameUI = "Prenume: " + firstName[j];
                barber.GetComponent<Barber>().LastNameUI = "Nume: " + lastName[j];
            }
            else if (selectedLanguage == 2)
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
            /*
            UnityWebRequest webjpg = new UnityWebRequest();
            Texture2D logoTexture;
            shopImageUrlJpg = "http://mybarber.vlcapps.com/barber_photos/" + barberUsername[j] + "/profile_picture.jpg";
            UnityWebRequest headjpg = UnityWebRequest.Head(shopImageUrlJpg); //check if the image exists on server
            yield return headjpg.SendWebRequest();
            if (headjpg.responseCode < 400) //if the image exists execute code below
            {
                webjpg = UnityWebRequestTexture.GetTexture(shopImageUrlJpg); //get the image texture from url
                yield return webjpg.SendWebRequest();
                logoTexture = DownloadHandlerTexture.GetContent(webjpg); //set the texture to a variable
                barber.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = Sprite.Create(logoTexture, new Rect(0, 0, logoTexture.width, logoTexture.height), new Vector2(0, 0));
                webjpg.Dispose();
            } */
            barber.SetActive(true);
            barberList.Add(barber);
        }
        loadingScreen.SetActive(false);
    }
    public void ShowBarberPhoto()
    {
        GameObject barber = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.transform.parent.gameObject;
        StartCoroutine(ShowBarberPhotoEnum(barber));
    }
    IEnumerator ShowBarberPhotoEnum(GameObject barber)
    {
        string barberUsername = barber.name.Split('\t')[0];
        UnityWebRequest webjpg = new UnityWebRequest();
        Texture2D logoTexture;
        shopImageUrlJpg = "http://mybarber.vlcapps.com/barber_photos/" + barberUsername + "/profile_picture.jpg";
        UnityWebRequest headjpg = UnityWebRequest.Head(shopImageUrlJpg); //check if the image exists on server
        yield return headjpg.SendWebRequest();
        if (headjpg.responseCode < 400) //if the image exists execute code below
        {
            webjpg = UnityWebRequestTexture.GetTexture(shopImageUrlJpg); //get the image texture from url
            yield return webjpg.SendWebRequest();
            logoTexture = DownloadHandlerTexture.GetContent(webjpg); //set the texture to a variable
            GameObject avatar = barber.transform.Find("Avatar").gameObject;
            avatar.GetComponent<Image>().sprite = Sprite.Create(logoTexture, new Rect(0, 0, logoTexture.width, logoTexture.height), new Vector2(0, 0));
            avatar.transform.Find("Text").gameObject.SetActive(false);
            avatar.transform.Find("ShowphotoBTN").gameObject.SetActive(false);
            webjpg.Dispose();
        }
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
        createShopMenuBTN.SetActive(false);
        loginButton.SetActive(!account.IsLogged);
        manageShopButton.SetActive(account.IsBoss);
        registerButton.SetActive(!account.IsLogged);
        registerMenu.SetActive(false);
        loginMenu.SetActive(false);
        manageShopMenu.SetActive(false);
        adminMenu.SetActive(false);
        backBTN.SetActive(false);
        logoutBtn.SetActive(account.IsLogged);
        barberMenuBTN.SetActive(account.IsEmployed);
        if (account.IsLogged)
        {
            if (account.IsBoss)
            {
                createShopMenuBTN.SetActive(account.CanCreateShops);
            }
            if (shopsOfUserList.Count <= 0)
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
        Debug.Log(createShopCityDropdown.options[createShopCityDropdown.value].text + " orasu ");
        Debug.Log(createShopCountyDropdown.options[createShopCountyDropdown.value].text + " judet");
        Debug.Log(createShopName.text + " = nume shop");
        Debug.Log(createShopAddress.text + " = adereasa shop");
        Debug.Log(account.AccountUsername + " = username");
        Debug.Log(account.PersonalCode + " = personcod");
        Debug.Log(account.ShopsCreated + " = shopscreated");
        Debug.Log(account.ShopsToCreate + " = shopstocreate");
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/createshop.php", form);
        yield return webreq.SendWebRequest();
        Debug.Log(webreq.downloadHandler.text);
        if (webreq.result == UnityWebRequest.Result.ConnectionError)
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
                GetShopsOfUser();
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Shop created ! Check the manage shops menu for more edit options.";
                GetShopsOfUser();
            }
            errorObj.SetActive(true);
            createShopName.text = "";
            createShopAddress.text = "";
            createShopCountyDropdown.RefreshShownValue();
            createShopCityDropdown.RefreshShownValue();
            account.ShopsCreated++;
            createShopMenuBTN.SetActive(account.CanCreateShops);
        }
        webreq.Dispose();
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
        appointmentsClass.barberMenuUpdateMonth();
        if (selectedLanguage == 1)
        {
            loadingScreen.SetActive(true);
            if (string.IsNullOrEmpty(selectedCounty))
            {
                pickCityBTN.GetComponent<Button>().interactable = false;
                pickCityBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = "Alege un judet mai intai";
            }
            pickCountyBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = "Alege un judet";
            locationShowShopBTN.text = "Afiseaza";
            locationShowShopsInfo.text = "Alege locatia saloanelor.";
            settingsMenuLanguageTxt.text = "Limba";
            settingsMenuLoginBtn.text = "Conectare";
            settingsMenuRegisterBtn.text = "Înregistrare";
            logoutBtnTxt.text = "Deconectare";
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
            editShopSelectShopTxt.text = "Alege ce salon doresti sa modifici";
            editShopDescriptionBtn.text = "Modifica descrierea";
            editShopWorkingProgramBtn.text = "Modifica program de lucru";
            editShopPhotosBtn.text = "Modifica pozele salonului";
            editShopServicesBtn.text = "Modifica servicii";
            editShopBarbersBtn.text = "Modifica angajati";
            editShopLogoImageBtn.text = "Modifica logo salon";
            timeToCutMenuPlaceholder.text = "Scrie aici...";
            timeToCutMenuSubmitBtn.text = "Actualizeaza";
            timeToCutMenuInfoTxt.text = "Introdu timpul de care ai nevoie intre clienti. \n Timpul necesar pentru realizarea unei tunsori.";
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
            ImageAddToList("Nicio imagine selectata", "Imaginea 1", "Imaginea 2", "Imaginea 3", "Imaginea 4", "Imaginea 5");
            whatImageToUpload.AddOptions(uploadImageList);
            salonMenuInfoBtn.text = "Informatii";
            salonMenuBarberBtn.text = "Frizeri";
            salonMenuBarberFirstName.text = "Prenume: ";
            salonMenuBarberLastName.text = "Nume: ";
            appointmentMenuCheckHoursBackBtn.text = "Inapoi";
            appointmentMenuNextToServicesBtn.text = "Inainte";
            appointmentMenuNextToServicesBackBtn.text = "Inapoi";
            appointmentMenuMentionsInfoTxt.text = "Daca ai informatii/mentiuni pentru frizer scrie-le mai jos";
            appointmentMenuMentionsPlaceholder.text = "Scrie aici..";
            appointmentMenuBackToHourBtn.text = "Inapoi";
            appointmentMenuCreateAppointmentBtn.text = "Creeaza";
            shopDescription.text = description_ro;
            account.ProfileUsername = "Nume utilizator: " + account.AccountUsername;
            account.ProfileFirstName = "Prenume: " + account.GetProfileFirstName;
            account.ProfileLastName = "Nume: " + account.GetProfileLastName;
            for (int i = 0; i < barberList.Count; i++)
            {

                barberList[i].GetComponent<Barber>().FirstNameUI = "Prenume: " + firstName[i];
                barberList[i].GetComponent<Barber>().LastNameUI = "Nume: " + lastName[i];
            }
            editServicePriceInput.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = "Introdu pret nou..";
            createServiceEnNameInfo.text = "Scrie mai jos varianta in engleza a numelui serviciului.";
            createServiceRoNameInfo.text = "Scrie mai jos numele noului serviciu.";
            createServicePriceInfo.text = "Scrie mai jos pretul noului serviciu.";
            serviceInfoRoNameInput.text = "1. In prima caseta de text trebuie introdus este cel in limba romana. Scrie numele serviciului in romana sau lasa spatiul gol daca nu vrei sa ai o versiune in romana a salonului tau.";
            serviceInfoEnNameInput.text = "2. In a doua caseta de text trebuie introdus este cel in limba engleza. Scrie numele serviciului in engleza sau lasa spatiul gol daca nu vrei sa ai o versiune in engleza a salonului tau.";
            serviceInfoPriceInput.text = "3. In ultima caseta de text trebuie introdus pretul pe care vrei sa-l aibe noul tau serviciu.";
            createServiceBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Creeaza serviciu";
            deleteServiceBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Sterge serviciu";
            addEmployeeBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Adauga angajat";
            deleteEmployeeBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Sterge angajat";
            deleteServiceQuestionText.text = "Doresti sa stergi acest serviciu ?";
            noImageLoadedErrorTxt.text = "Imaginea nu a putut fi gasita pe server.";
            employeeInfoTxt.text = "~  Introdu codul personal al persoanei pe care doresti sa o angajezi. Acest cod este gasit pe profilul fiecarui utilizator al aplicatiei dupa ce isi creeaza un cont si se conecteaza.";
            employeePersonalCodeInput.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = "Cod personal...";
            deleteEmployeeQuestion.text = "Doresti sa stergi acest angajat ?";
            loadingScreen.SetActive(false);
        }
        else if (selectedLanguage == 2)
        {
            loadingScreen.SetActive(true);
            if (string.IsNullOrEmpty(selectedCounty))
            {
                pickCityBTN.GetComponent<Button>().interactable = false;
                pickCityBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = "Choose a county first";
            }
            pickCountyBTN.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = "Choose a county";
            locationShowShopBTN.text = "Show";
            locationShowShopsInfo.text = "Pick the shop's location.";
            settingsMenuLanguageTxt.text = "Language";
            settingsMenuLoginBtn.text = "Login";
            settingsMenuRegisterBtn.text = "Register";
            logoutBtnTxt.text = "Logout";
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
            editShopSelectShopTxt.text = "Select which salon do you want to edit";
            editShopDescriptionBtn.text = "Edit description";
            editShopWorkingProgramBtn.text = "Edit working program";
            editShopPhotosBtn.text = "Edit photos";
            editShopServicesBtn.text = "Edit services";
            editShopBarbersBtn.text = "Edit barbers";
            editShopLogoImageBtn.text = "Edit shop's logo";
            timeToCutMenuPlaceholder.text = "Type here...";
            timeToCutMenuSubmitBtn.text = "Update";
            timeToCutMenuInfoTxt.text = "Type in the time needed for you to do a cut. \n The time you need to finish your client's haircut.";
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
            ImageAddToList("No image selected", "Image 1", "Image 2", "Image 3", "Image 4", "Image 5");
            whatImageToUpload.AddOptions(uploadImageList);
            salonMenuInfoBtn.text = "Informations";
            salonMenuBarberBtn.text = "Barbers";
            salonMenuBarberFirstName.text = "First name: ";
            salonMenuBarberLastName.text = "Last name: ";
            appointmentMenuCheckHoursBackBtn.text = "Back";
            appointmentMenuNextToServicesBtn.text = "Next";
            appointmentMenuNextToServicesBackBtn.text = "Back";
            appointmentMenuMentionsInfoTxt.text = "If you have any information the barber should know about  type it bellow";
            appointmentMenuMentionsPlaceholder.text = "Type here..";
            appointmentMenuBackToHourBtn.text = "Back";
            appointmentMenuCreateAppointmentBtn.text = "Create";
            shopDescription.text = description_en;
            account.ProfileUsername = "Username: " + account.AccountUsername;
            account.ProfileFirstName = "First name: " + account.GetProfileFirstName;
            account.ProfileLastName = "Last name: " + account.GetProfileLastName;
            for (int i = 0; i < barberList.Count; i++)
            {
                barberList[i].GetComponent<Barber>().FirstNameUI = "First name: " + firstName[i];
                barberList[i].GetComponent<Barber>().LastNameUI = "Last name: " + lastName[i];
            }
            editServicePriceInput.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = "Enter new price..";
            createServiceRoNameInfo.text = "Type the romanian name of the new service.";
            createServiceEnNameInfo.text = "Type the english name of the new service.";
            createServicePriceInfo.text = "Type the price of the new service.";
            serviceInfoRoNameInput.text = "1. The first input field is for romanian language. You should write the name of the service in romanian or leave it blank if you dont want to have a romanian version of your salon.";
            serviceInfoEnNameInput.text = "2. The second input field is for english language. You should write the name of the service in english or leave it blank if you dont want to have a english version of your salon.";
            serviceInfoPriceInput.text = "3. Last input field is for the price. Type in how much should your new service cost.";
            createServiceBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Create service";
            deleteServiceBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Remove service";
            addEmployeeBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Add employee";
            deleteEmployeeBTN.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = "Remove employee";
            deleteServiceQuestionText.text = "Do you want to delete this service?";
            noImageLoadedErrorTxt.text = "This image couldn't be found on the server.";
            employeeInfoTxt.text = "~ Please type in the personal code of the person you want to hire. This code can be found on every user's profile page after they create an account and log in.";
            employeePersonalCodeInput.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = "Personal code...";
            deleteEmployeeQuestion.text = "Do you want to remove this employee ?";
            loadingScreen.SetActive(false);
        }
    }
    public void CreateShopService()
    {
        createServiceNameRO = createServiceNameInputRO.text;
        createServiceNameEN = createServiceNameInputEN.text;
        if (!int.TryParse(createServicePriceInput.text, out createServicePrice))
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Scrie un numar real.";
                errorObj.SetActive(true);
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "Please type in a real number.";
                errorObj.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(CreateShopServiceEnum());
        }
    }
    IEnumerator CreateShopServiceEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        if (createServiceNameRO.Length > 0)
        {
            form.Add(new MultipartFormDataSection("serviceNameRO", createServiceNameRO));
        }
        if (createServiceNameEN.Length > 0)
        {
            form.Add(new MultipartFormDataSection("serviceNameEN", createServiceNameEN));
        }
        if (createServiceCoinInput.text.Length > 0)
        {
            form.Add(new MultipartFormDataSection("serviceCoinName", createServiceCoinInput.text));
        }
        form.Add(new MultipartFormDataSection("servicePrice", createServicePrice.ToString()));
        form.Add(new MultipartFormDataSection("selectedShop", selectedShopToManage.name));
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/createshopservices.php", form);
        yield return webreq.SendWebRequest();
        if (webreq.downloadHandler.text[0] != '0')
        {
            Debug.Log(webreq.downloadHandler.text);
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
                errorTXT.text = "Serviciu creat cu succes !";
                createServiceNameInputRO.text = "";
                createServiceNameInputEN.text = "";
                createServicePriceInput.text = "";
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "The service was successfully created!";
                createServiceNameInputRO.text = "";
                createServiceNameInputEN.text = "";
                createServicePriceInput.text = "";
            }
            errorObj.SetActive(true);
            GetShopServices();
        }
        webreq.Dispose();
    }
    public void AddEmployee()
    {
        StartCoroutine(AddEmployeeEnum());
    }
    IEnumerator AddEmployeeEnum()
    {
        loadingScreen.SetActive(true);
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("selectedShop", selectedShopToManage.name));
        if (int.TryParse(employeePersonalCodeInput.text, out int muie))
        {
            form.Add(new MultipartFormDataSection("personalCode", employeePersonalCodeInput.text));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/addemployee.php", form);
            yield return webreq.SendWebRequest();
            if (webreq.downloadHandler.text[0] == '0')
            {
                GetEmployeesWCreate();
                if (selectedLanguage == 1)
                {
                    errorTXT.text = "Utilizatorul a fost adaugat ca angajat al salonului.";
                    errorObj.SetActive(true);
                    webreq.Dispose();
                }
                else if (selectedLanguage == 2)
                {
                    errorTXT.text = "The user has been added as an employee of this salon.";
                    errorObj.SetActive(true);
                    webreq.Dispose();
                }
            }
            else
            {
                Debug.Log(webreq.downloadHandler.text);
            }
        }
        else
        {
            if (selectedLanguage == 1)
            {
                errorTXT.text = "Codul introdus nu este corect.";
                errorObj.SetActive(true);
            }
            else if (selectedLanguage == 2)
            {
                errorTXT.text = "This code is incorrect.";
                errorObj.SetActive(true);
            }
        }
        loadingScreen.SetActive(false);
    }
    public void GetEmployeesWCreate()
    {
        StartCoroutine(GetEmployeesWCreateEnum());
    }
    IEnumerator GetEmployeesWCreateEnum()
    {
        int whatToPick = 1;
        loadingScreen.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormDataSection("selectedShop", selectedShopToManage.name));
            form.Add(new MultipartFormDataSection("whattopick", whatToPick.ToString()));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getemployees.php", form);
            yield return webreq.SendWebRequest();
            switch (whatToPick)
            {
                case 1:
                    employeeFirstNameList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 2;
                    webreq.Dispose();
                    break;
                case 2:
                    employeeLastNameList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 3;
                    webreq.Dispose();
                    break;
                case 3:
                    employeePersonalCodeList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 1;
                    webreq.Dispose();
                    break;
            }
        }
        CreateEmployeesButtons();
        loadingScreen.SetActive(false);
    }
    public void GetEmployeesWOCreate()
    {
        StartCoroutine(GetEmployeesEnum());
    }
    IEnumerator GetEmployeesEnum()
    {
        int whatToPick = 1;
        loadingScreen.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormDataSection("selectedShop", selectedShopToManage.name));
            form.Add(new MultipartFormDataSection("whattopick", whatToPick.ToString()));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getemployees.php", form);
            yield return webreq.SendWebRequest();
            switch (whatToPick)
            {
                case 1:
                    employeeFirstNameList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 2;
                    webreq.Dispose();
                    break;
                case 2:
                    employeeLastNameList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 3;
                    webreq.Dispose();
                    break;
                case 3:
                    employeePersonalCodeList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 1;
                    webreq.Dispose();
                    break;
            }
        }
        loadingScreen.SetActive(false);
    }
    public void CreateEmployeesButtons()
    {
        if (employeeObjectList.Count > 0)
        {
            foreach (GameObject obj in employeeObjectList)
            {
                Destroy(obj);
            }
            employeeObjectList.Clear();
        }
        for (int i = 0; i < employeePersonalCodeList.Length - 1; i++)
        {
            GameObject employee = Instantiate(employeePrefab, employeePrefab.transform.parent);
            employee.name = employeePersonalCodeList[i];
            if (selectedLanguage == 1)
            {
                employee.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = "Nume: " + employeeFirstNameList[i] + "  " + employeeLastNameList[i];
                employee.transform.Find("PersonalCode").gameObject.GetComponent<TextMeshProUGUI>().text = "Cod utilizator: " + employeePersonalCodeList[i];
            }
            else if (selectedLanguage == 2)
            {
                employee.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = "Name: " + employeeFirstNameList[i] + "  " + employeeLastNameList[i];
                employee.transform.Find("PersonalCode").gameObject.GetComponent<TextMeshProUGUI>().text = "Personal code: " + employeePersonalCodeList[i];
            }
            employee.SetActive(true);
            employeeObjectList.Add(employee);
        }
    }
    public void GetShopServices()
    {
        StartCoroutine(GetShopServicesEnum());
    }
    IEnumerator GetShopServicesEnum()
    {
        int whatToPick = 1;
        loadingScreen.SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormDataSection("selectedShop", selectedShopToManage.name));
            form.Add(new MultipartFormDataSection("whattopick", whatToPick.ToString()));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getshopservices.php", form);
            yield return webreq.SendWebRequest();

            switch (whatToPick)
            {
                case 1:
                    createServiceNameListRO = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 2;
                    webreq.Dispose();
                    break;
                case 2:
                    createServicePriceList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 3;
                    webreq.Dispose();
                    break;
                case 3:
                    createServiceNameListEN = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 4;
                    webreq.Dispose();
                    break;
                case 4:
                    createServiceCoinList = webreq.downloadHandler.text.Split('\t');
                    whatToPick = 1;
                    webreq.Dispose();
                    break;
            }
        }
        loadingScreen.SetActive(false);
    }
    public void UpdateServicePrice()
    {
        StartCoroutine(UpdateServicePriceEnum());
    }
    IEnumerator UpdateServicePriceEnum()
    {
        if (int.TryParse(editServicePriceInput.text, out int a) == false)
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
        form.Add(new MultipartFormDataSection("serviceName", serviceName));
        form.Add(new MultipartFormDataSection("price", editServicePriceInput.text));
        form.Add(new MultipartFormDataSection("shopName", selectedShopToManage.name));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/updateserviceprice.php", form);
        yield return web.SendWebRequest();
        if (web.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(web.downloadHandler.text);
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
            for (int i = 0; i < createServiceObjectList.Count; i++)
            {
                if (createServiceObjectList[i].name == serviceName)
                {
                    createServicePriceList[i] = editServicePriceInput.text;
                    editServicePriceInput.text = "";
                    serviceActualPrice.text = createServicePriceList[i];
                }
            }
            errorObj.SetActive(true);
        }
        web.Dispose();
    }
    public void DeleteEmployee()
    {
        StartCoroutine(DeleteEmployeeEnum());
    }
    IEnumerator DeleteEmployeeEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("personalCode", selectedEmployee));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/deleteemployee.php", form);
        yield return web.SendWebRequest();
        if (web.downloadHandler.text[0] == '0')
        {
            switch (selectedLanguage)
            {
                case 1:
                    errorTXT.text = "Angajatul a fost sters cu succes!";
                    errorObj.SetActive(true);
                    GetEmployeesWCreate();
                    web.Dispose();
                    break;
                case 2:
                    errorTXT.text = "This employee has been removed successfully!";
                    errorObj.SetActive(true);
                    GetEmployeesWCreate();
                    web.Dispose();
                    break;
            }
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
            switch (selectedLanguage)
            {
                case 1:
                    errorTXT.text = generalErrorRo;
                    web.Dispose();
                    break;
                case 2:
                    errorTXT.text = generalErrorEng;
                    errorObj.SetActive(true);
                    web.Dispose();
                    break;
            }
        }
    }
    public void DeleteService()
    {
        StartCoroutine(DeleteServiceEnum());
    }
    IEnumerator DeleteServiceEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("serviceName", serviceName));
        form.Add(new MultipartFormDataSection("selectedShop", selectedShopToManage.name));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/deleteservice.php", form);
        yield return web.SendWebRequest();
        if (web.downloadHandler.text[0] == '0')
        {
            switch (selectedLanguage)
            {
                case 1:
                    errorTXT.text = "Serviciul a fost sters cu succes!";
                    errorObj.SetActive(true);
                    GetShopServices();
                    break;
                case 2:
                    errorTXT.text = "This service has been deleted successfully!";
                    errorObj.SetActive(true);
                    GetShopServices();
                    break;
            }
        }
        else
        {
            switch (selectedLanguage)
            {
                case 1:
                    errorTXT.text = generalErrorRo;
                    errorObj.SetActive(true);
                    break;
                case 2:
                    errorTXT.text = generalErrorEng;
                    errorObj.SetActive(true);
                    break;
            }
        }
        web.Dispose();
    }
    public void SelectEmployee()
    {
        selectedEmployee = EventSystem.current.currentSelectedGameObject.gameObject.name;
        if (deleteEmployee)
        {
            deleteQuestionObject.SetActive(true);
        }
    }
    public void SelectService()
    {
        serviceName = EventSystem.current.currentSelectedGameObject.gameObject.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text;
        if (deleteService)
        {
            deleteServiceQuestion.SetActive(true);
        }
        else
        {
            for (int i = 0; i < createServiceObjectList.Count; i++)
            {
                if (createServiceObjectList[i].name == serviceName)
                {
                    serviceActualPrice.text = createServicePriceList[i];
                }
            }
            serviceInputPriceBG.SetActive(true);
        }
    }
    public void CreateShopServiceButtons()
    {
        loadingScreen.SetActive(true);
        if (createServiceObjectList.Count > 0)
        {
            foreach (GameObject obj in createServiceObjectList)
            {
                Destroy(obj);
            }
            createServiceObjectList.Clear();
        }
        for (int i = 0; i < createServicePriceList.Length - 1; i++)
        {
            if (selectedLanguage == 1)
            {
                if (createServiceNameListRO[i].Length < 1)
                {
                    GameObject service = Instantiate(createServicePrefab, createServicePrefab.transform.position, createServicePrefab.transform.rotation, createServicePrefab.transform.parent);
                    errorTXT.text = "Unul sau mai multe servicii nu au numele in romana configurate. Am tradus numele lor in urmatoarea limba gasita.";
                    errorObj.SetActive(true);
                    service.name = createServiceNameListEN[i];
                    manageShopNameTXT = service.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                    manageShopNameTXT.text = createServiceNameListEN[i];
                    service.SetActive(true);
                    createServiceObjectList.Add(service);
                }
                else
                {
                    GameObject service = Instantiate(createServicePrefab, createServicePrefab.transform.position, createServicePrefab.transform.rotation, createServicePrefab.transform.parent);
                    service.name = createServiceNameListRO[i];
                    manageShopNameTXT = service.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                    manageShopNameTXT.text = createServiceNameListRO[i];
                    service.SetActive(true);
                    createServiceObjectList.Add(service);
                }
            }
            else if (selectedLanguage == 2)
            {
                if (createServiceNameListEN[i].Length < 1)
                {
                    GameObject service = Instantiate(createServicePrefab, createServicePrefab.transform.position, createServicePrefab.transform.rotation, createServicePrefab.transform.parent);
                    errorTXT.text = "Some services have no english name configured. We've translated their names in the next found language.";
                    errorObj.SetActive(true);
                    service.name = createServiceNameListRO[i];
                    manageShopNameTXT = service.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                    manageShopNameTXT.text = createServiceNameListRO[i];
                    service.SetActive(true);
                    createServiceObjectList.Add(service);
                }
                else
                {
                    GameObject service = Instantiate(createServicePrefab, createServicePrefab.transform.position, createServicePrefab.transform.rotation, createServicePrefab.transform.parent);
                    service.name = createServiceNameListEN[i];
                    manageShopNameTXT = service.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                    manageShopNameTXT.text = createServiceNameListEN[i];
                    service.SetActive(true);
                    createServiceObjectList.Add(service);
                }
            }
        }
        loadingScreen.SetActive(false);
    }
    public void EditDescriptionBTN()
    {
        if (selectedLanguage == 1)
        {
            errorTXT.text = "Selecteaza limba pentru care doresti sa modifici descrierea inainte de a apasa `Actualizeaza`";
            errorObj.SetActive(true);
        }
        else
        {
            errorTXT.text = "Select the language of the description you want to edit before pressing `Update`";
            errorObj.SetActive(true);
        }
    }
    public void SelectSalon()
    {
        pageTitle.text = "salon page";
        shopImageNumber = 1;
        selectedShopName = EventSystem.current.currentSelectedGameObject.name;
        if (selectedShopName == lastShopSelected)
        {
            shopImageNumber = 1;
        }
        else
        {
            showBarbers();
            shopImage = new Texture2D[6];
            shopImageNumber = 1;
            //getShopImages();
            ShowShopDescription();
            GetShopWorkingHours();
            lastShopSelected = selectedShopName;
        }
    }
    public void ShowShops()
    {
        StartCoroutine(ShowShopsEnumerator());
    }
    IEnumerator ShowShopsEnumerator()
    {
        loadingScreen.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            loadingScreen.SetActive(true);  //activeaza loading screenu

            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormDataSection("whatToPick", whatToPick.ToString()));
            form.Add(new MultipartFormDataSection("city", selectedCity));
            form.Add(new MultipartFormDataSection("county", selectedCounty));
            UnityWebRequest www = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/showShops.php", form);
            yield return www.SendWebRequest();
            switch (whatToPick)
            {
                case 1:
                    shopName = www.downloadHandler.text.Split('\t');
                    whatToPick = 2;
                    www.Dispose();
                    break;
                case 2:
                    shopAddress = www.downloadHandler.text.Split('\t');
                    whatToPick = 1;
                    www.Dispose();
                    break;
            }
        }
        if (shopName.Length > 0)
        {
            if (shopList.Count > 0)
            {
                foreach (GameObject shop in shopList)
                {
                    Destroy(shop.gameObject);
                }
                shopList.Clear();
            }
            for (int i = 0; i < shopName.Length - 1; i++)
            {
                GameObject barberShop = Instantiate(shopPrefab);
                barberShop.name = shopName[i];
                barberShop.SetActive(true);
                barberShop.GetComponent<BarberShop>().ShopName = shopName[i];
                barberShop.GetComponent<BarberShop>().ShopAddress = shopAddress[i];
                barberShop.transform.SetParent(shopPrefab.transform.parent, false);
                UnityWebRequest webjpg = new UnityWebRequest();
                Texture2D logoTexture;
                shopImageUrlJpg = "http://mybarber.vlcapps.com/shop_photos/" + shopName[i] + "/shop_logo.jpg";
                UnityWebRequest headjpg = UnityWebRequest.Head(shopImageUrlJpg); //check if the image exists on server
                yield return headjpg.SendWebRequest();
                if (headjpg.responseCode < 400) //if the image exists execute code below
                {
                    webjpg = UnityWebRequestTexture.GetTexture(shopImageUrlJpg); //get the image texture from url
                    yield return webjpg.SendWebRequest();
                    logoTexture = DownloadHandlerTexture.GetContent(webjpg); //set the texture to a variable
                    barberShop.GetComponent<BarberShop>().ShopLogo.sprite = Sprite.Create(logoTexture, new Rect(0, 0, logoTexture.width, logoTexture.height), new Vector2(0, 0));
                    webjpg.Dispose();
                }
                List<IMultipartFormSection> employeesform = new List<IMultipartFormSection>();
                employeesform.Add(new MultipartFormDataSection("shopName", shopName[i]));
                UnityWebRequest employeeswww = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getemployeesnumber.php", employeesform);
                yield return employeeswww.SendWebRequest();
                Debug.Log(employeeswww.downloadHandler.text + " number");
                int employeesNumberInt = int.Parse(employeeswww.downloadHandler.text) - 3;
                if (int.Parse(employeeswww.downloadHandler.text) > 3)
                {
                    barberShop.GetComponent<BarberShop>().ShopEmployees = "+" + employeesNumberInt;
                }
                else
                {
                    barberShop.GetComponent<BarberShop>().ShopEmployees = "";
                }
                barberShop.transform.Find("LogoSalon").transform.Find("Loading").gameObject.SetActive(false);
                employeeswww.Dispose();
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
        if (webreq.result == UnityWebRequest.Result.ConnectionError)
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
        webreq.Dispose();
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
        if (webreq.result == UnityWebRequest.Result.ConnectionError)
        {
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
        webreq.Dispose();
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
        if(openWorkingHourList.Count > 0)
        {
            foreach(GameObject obj in openWorkingHourList)
            {
                Destroy(obj);
            }
            openWorkingHourList.Clear();
        }
        if (closeWorkingHourList.Count > 0)
        {
            foreach (GameObject obj in closeWorkingHourList)
            {
                Destroy(obj);
            }
            closeWorkingHourList.Clear();
        }
        for (int i = 0; i < 24; i++)
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
            openWorkingHourList.Add(openWorkingHourBTN);
            closeWorkingHourList.Add(closeWorkingHourBTN);
        }
    }
    public void CreateMinuteButtons() //creating buttons for every minute after selecting the day to modify the program for
    {
        if (openWorkingMinuteList.Count > 0)
        {
            foreach (GameObject obj in openWorkingMinuteList)
            {
                Destroy(obj);
            }
            openWorkingMinuteList.Clear();
        }
        if (closeWorkingMinuteList.Count > 0)
        {
            foreach (GameObject obj in closeWorkingMinuteList)
            {
                Destroy(obj);
            }
            closeWorkingMinuteList.Clear();
        }
        for (int i = 0; i < 60; i++)
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
            openWorkingMinuteList.Add(openWorkingMinuteBTN);
            closeWorkingMinuteList.Add(closeWorkingMinuteBTN);
        }
    }
    public void UpdateShopWorkingHours()
    {
        StartCoroutine(UpdateShopWH());
    }
    IEnumerator UpdateShopWH()
    {
        if (selectedOpenWorkingMinute == null || selectedOpenWorkingMinute == "") //in caz ca nu alege niciun minut se pune 00
        {
            selectedOpenWorkingMinute = "00";
        }
        if (selectedCloseWorkingMinute == null || selectedCloseWorkingMinute == "") //in caz ca nu alege niciun minut se pune 00
        {
            selectedCloseWorkingMinute = "00";
        }
        updateWorkingProgram = selectedOpenWorkingHour + ":" + selectedOpenWorkingMinute + "-"
            + selectedCloseWorkingHour + ":" + selectedCloseWorkingMinute;

        if (selectedCloseWorkingHour == null || selectedCloseWorkingHour == "" || selectedOpenWorkingHour == null || selectedOpenWorkingHour == "")
        {
            switch (selectedLanguage)
            {
                case 1:
                    errorTXT.text = "Asigura-te ca ai ales ora de deschidere si ora de inchidere a salonului.";
                    errorObj.SetActive(true);
                    break;
                case 2:
                    errorTXT.text = "Make sure you selected the opening and closing hours of your shop.";
                    errorObj.SetActive(true);
                    break;
            }
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("selectedDay", selectedWorkingDay);
            form.AddField("workingHours", updateWorkingProgram);
            form.AddField("shopName", selectedShopToManage.name);

            WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/editshopworkinghours.php", form);
            yield return www;
            if (www.text[0] == '0')
            {
                switch (selectedLanguage)
                {
                    case 1:
                        errorTXT.text = "Program actualizat cu succes!";
                        errorObj.SetActive(true);
                        break;
                    case 2:
                        errorTXT.text = "Working program successfully updated!";
                        errorObj.SetActive(true);
                        break;
                }
            }
            else
            {
                Debug.Log(www.text);
            }
            www.Dispose();
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
                        if (selectedLanguage == 1)
                        {
                            mondayHours.text = "Luni: Inchis.";
                        }
                        else if (selectedLanguage == 2)
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
        loadingScreen.SetActive(false);
        www.Dispose();
    }
    public void nextShopImage()
    {
        if (shopImageNumber < 5)
        {
            shopImageNumber++;
            imageNumberTxt.text = shopImageNumber + "/5";
            loadingScreenImages.SetActive(true);
            noImageLoadedErrorObj.SetActive(false);
            if (shopImage[shopImageNumber] == null)
            {
                getShopImages();
            }
            else
            {
                setimagine();
            }
        }
    }
    public void previousShopImage()
    {
        if (shopImageNumber > 1)
        {
            shopImageNumber--;
            imageNumberTxt.text = shopImageNumber + "/5";
            loadingScreenImages.SetActive(true);
            noImageLoadedErrorObj.SetActive(false);
            if (shopImage[shopImageNumber] == null)
            {
                getShopImages();
            }
            else
            {
                setimagine();
            }
        }
    }
    public void setimagine()
    {
        if (!isFailedImage(shopImage[shopImageNumber]))
        {
            shopPhoto.sprite = Sprite.Create(shopImage[shopImageNumber], new Rect(0, 0, shopImage[shopImageNumber].width, shopImage[shopImageNumber].height), new Vector2(0, 0), 16, 0, SpriteMeshType.FullRect);
            loadingScreenImages.SetActive(false);
        }
    }
    public void getShopImages()
    {
        StartCoroutine(GetShopImages());
    }
    IEnumerator GetShopImages()
    {
        UnityWebRequest webjpg = new UnityWebRequest();
        shopImageUrlJpg = "http://mybarber.vlcapps.com/shop_photos/" + selectedShopName + "/image" + shopImageNumber + ".jpg";
        UnityWebRequest headjpg = UnityWebRequest.Head(shopImageUrlJpg); //check if the image exists on server
        yield return headjpg.SendWebRequest();
        if (headjpg.responseCode < 400) //if the image exists execute code below
        {
            webjpg = UnityWebRequestTexture.GetTexture(shopImageUrlJpg); //get the image texture from url
            yield return webjpg.SendWebRequest();
            shopImage[shopImageNumber] = DownloadHandlerTexture.GetContent(webjpg); //set the texture to a variable
            shopPhoto.sprite = Sprite.Create(shopImage[shopImageNumber], new Rect(0, 0, shopImage[shopImageNumber].width, shopImage[shopImageNumber].height), new Vector2(0, 0), 16, 0, SpriteMeshType.FullRect);
            webjpg.Dispose();
            loadingScreenImages.SetActive(false);
        }
        else
        {
            loadingScreenImages.SetActive(false);
            noImageLoadedErrorObj.SetActive(true);
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
    public void PickPhoneProfilePicture()
    {
        float width;
        float height;
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
                if (texture.width > 512)
                {
                    width = 512;
                }
                else
                {
                    width = texture.width;
                }
                if (texture.height > 256)
                {
                    height = 256;
                }
                else
                {
                    height = texture.height;
                }
                profilePicture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
                profilePictureTexture = texture; // poza de upload sa fie textura luata din telefon
                UploadProfilePicture();
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }
    public void GetProfilePicture()
    {
        StartCoroutine(GetProfilePictureEnum());
    }
    IEnumerator GetProfilePictureEnum()
    {
        UnityWebRequest webjpg = new UnityWebRequest();
        Texture2D logoTexture;
        shopImageUrlJpg = "http://mybarber.vlcapps.com/barber_photos/" + account.AccountUsername + "/profile_picture.jpg";
        UnityWebRequest headjpg = UnityWebRequest.Head(shopImageUrlJpg); //check if the image exists on server
        yield return headjpg.SendWebRequest();
        if (headjpg.responseCode < 400) //if the image exists execute code below
        {
            webjpg = UnityWebRequestTexture.GetTexture(shopImageUrlJpg); //get the image texture from url
            yield return webjpg.SendWebRequest();
            logoTexture = DownloadHandlerTexture.GetContent(webjpg); //set the texture to a variable
            profilePicture.sprite = Sprite.Create(logoTexture, new Rect(0, 0, logoTexture.width, logoTexture.height), new Vector2(0, 0));
            webjpg.Dispose();
        }
    }
    public void UploadProfilePicture()
    {
        StartCoroutine(StartUploadingProfileEnum());
    }
    IEnumerator StartUploadingProfileEnum()
    {
        WWWForm form = new WWWForm();
        byte[] textureBytes = null;

        //Get a copy of the texture, because we can't access original texure data directly. 
        Texture2D photoTexture = GetTextureCopy(profilePictureTexture);
        textureBytes = photoTexture.EncodeToJPG();
        form.AddBinaryData("myimage", textureBytes, "profile_picture.jpg", "logobro.jpg");
        form.AddField("username", account.AccountUsername);

        WWW w = new WWW("http://mybarber.vlcapps.com/appscripts/uploadprofilepicture.php", form);

        yield return w;
        Debug.Log(w.text);
        w.Dispose();
    }
    public void PickPhoneLogoImage()
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
                selectedLogoImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
                logoFromPhone = texture; // poza de upload sa fie textura luata din telefon
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }
    public void UploadLogoImage()
    {
        StartCoroutine(StartUploadingLogo());
    }
    IEnumerator StartUploadingLogo()
    {
        WWWForm form = new WWWForm();
        byte[] textureBytes = null;

        //Get a copy of the texture, because we can't access original texure data directly. 
        Texture2D photoTexture = GetTextureCopy(logoFromPhone);
        textureBytes = photoTexture.EncodeToJPG();
        form.AddBinaryData("myimage", textureBytes, "shop_logo.jpg", "logobro.jpg");
        form.AddField("shopName", selectedShopToManage.name);

        WWW w = new WWW("http://mybarber.vlcapps.com/appscripts/uploadimage.php", form);

        yield return w;
        Debug.Log(w.text);
        w.Dispose();
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
            case 1:
                uploadedImageNumber = 1;
                break;
            case 2:
                uploadedImageNumber = 2;
                break;
            case 3:
                uploadedImageNumber = 3;
                break;
            case 4:
                uploadedImageNumber = 4;
                break;
            case 5:
                uploadedImageNumber = 5;
                break;
        }
    }
    public void GetShopsOfUser()
    {
        StartCoroutine(GetShopsOfUserEnum());
    }
    IEnumerator GetShopsOfUserEnum()
    {
        for (int i = 1; i < 4; i++)
        {
            List<IMultipartFormSection> newform = new List<IMultipartFormSection>();
            newform.Add(new MultipartFormDataSection("username", account.AccountUsername));
            newform.Add(new MultipartFormDataSection("whatData", i.ToString()));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getshopsofuser.php", newform);
            yield return webreq.SendWebRequest();
            if (webreq.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webreq.error);
            }
            else
            {
                if (i == 1)
                {
                    shopsOfUser = webreq.downloadHandler.text.Split('\t');
                }
                else if (i == 2)
                {
                    shopsOfUserAddress = webreq.downloadHandler.text.Split('\t');
                }
                else if (i == 3)
                {
                    shopsOfUserCity = webreq.downloadHandler.text.Split('\t');
                }
            }
            webreq.Dispose();
        }
    }
    public void SelectShopToManage()
    {
        selectedShopToManage = EventSystem.current.currentSelectedGameObject.gameObject;
        GetShopServices();
        GetEmployeesWOCreate();
    }
    public void CreateShopsToManage()
    {
        if (shopsOfUserList.Count > 0)
        {
            foreach (GameObject obj in shopsOfUserList)
            {
                Destroy(obj);
            }
            shopsOfUserList.Clear();
        }
        for (int i = 0; i < shopsOfUser.Length - 1; i++)
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
                createShopCityDropdown.ClearOptions();
                if (selectedLanguage == 1)
                {
                    List<string> cityOltOptions = new List<string> { "Alege un oras", "Caracal", "Farcasele", "Slatina", "Corabia" };
                    createShopCityDropdown.AddOptions(cityOltOptions);
                }
                else if (selectedLanguage == 2)
                {
                    List<string> cityOltOptions = new List<string> { "Pick a city", "Caracal", "Farcasele", "Slatina", "Corabia" };
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
                    List<string> cityDoljOptions = new List<string> { "Pick a city", "Craiova", "Radovan", "Gogosu", "Gura Racului" };
                    createShopCityDropdown.AddOptions(cityDoljOptions);
                }
                break;
        }
    }
}
