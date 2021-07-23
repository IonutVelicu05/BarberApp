using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Appointments : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentDateTXT; //current date textbox
    [SerializeField] private GameObject dayPrefab; //the object from which i instantiate all days in the calendar.
    [SerializeField] private TextMeshProUGUI dayNumber; //text of the daynumber textbox;
    [SerializeField] private TextMeshProUGUI dayName; //text of the dayname textbox;
    [SerializeField] private Account accountClass;
    [SerializeField] private Button createAppointmentHours;
    [SerializeField] private AppManager appmanagerClass;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private InputField timeToCutInput;
    private int WhatToPick = 1;
    //calendar
    private string currentDateString; // the date that is selected at the moment
    private string selectedDayString = DateTime.Now.Day.ToString(); //string of the name shown in the CurrentDate textbox
    private int currentYear = DateTime.Now.Year; //the current year
    private int currentMonth = DateTime.Now.Month; //the current month
    private List<GameObject> daysObjects = new List<GameObject>(); //list of days objects created
    private GameObject selectedDayObj; //object representing the day object that was pressed on
    private Image selectedDayImg; //image of the selected day
    private Image lastDaySelectedImg; //image of the last selected day
    private GameObject dayNumberObj; //object representing the text object that holds the number of the day
    private GameObject dayNameObj; //object representing the text object that holds the name of the day
    private DateTime currentDate;
    [SerializeField] private GameObject calendarBG;
    [SerializeField] private GameObject selectedDateBG;
    [SerializeField] private GameObject appointmentMenu; //child of full appointment menu
    [SerializeField] private GameObject fullAppointmentMenu; //the whole menu responsible for appointments
    private GameObject checkHoursBTN;
    private GameObject nextMonthDateBTN;
    private GameObject previousMonthDateBTN;
    private GameObject hoursScrollView;
    private GameObject minutesScrollView;
    private GameObject daysScrollView;
    private GameObject closeAppointmentBTN;
    private GameObject backFromHoursBTN;
    private GameObject nextToInsertClientNameBTN;
    private GameObject nextToServicesBTN;
    private GameObject servicesBG;
    private GameObject backToHoursBTN;
    //barber prices 
    private int barberHaircutPrice;
    private int barberBeardPrice;
    private int barberMustachePrice;
    private int barberColourPrice;
    private int barberEyebrowPrice;
    private bool haircutService = false;
    private bool beardService = false;
    private bool mustacheService = false;
    private bool colourService = false;
    private bool eyebrowService = false;
    [SerializeField] private TextMeshProUGUI barberHaircutPriceTXT;
    [SerializeField] private TextMeshProUGUI barberBeardPriceTXT;
    [SerializeField] private TextMeshProUGUI barberMustachePriceTXT;
    [SerializeField] private TextMeshProUGUI barberColourPriceTXT;
    [SerializeField] private TextMeshProUGUI barberEyebrowPriceTXT;
    [SerializeField] private TextMeshProUGUI totalServicesPrice;
    private GameObject haircutServiceButton;
    private GameObject beardServiceButton;
    private GameObject mustacheServiceButton;
    private GameObject colourServiceButton;
    private GameObject eyebrowServiceButton;
    private int totalPrice;

    //time selection
    [SerializeField] private GameObject hourPrefab;
    [SerializeField] private GameObject minutePrefab;
    private int openHour;
    private int openMinute;
    private int closeHour;
    private int closeMinute;
    private List<GameObject> hourObjects = new List<GameObject>();
    private List<GameObject> minuteObjects = new List<GameObject>();
    private int selectedHour;
    private int selectedMinute;
    private GameObject selectedMinuteObj;
    private GameObject selectedHourObj;
    private bool[] occupiedHours = new bool[25];
    private bool[] occupiedMinutes = new bool[60];
    private Image selectedHourImage;
    private Image lastSelectedHourImage;
    private Image selectedMinuteImage;
    private Image lastSelectedMinuteImage;

    //create appointment
    private GameObject selectedBarberObj; //selected barber object
    private string barberFirstName; //selected barber's first name
    private string barberLastName; //selected barber's last name
    private int newCloseMinutes;
    private int newOpenMinutes;
    private int timeToCut = 0;
    [SerializeField] private GameObject createAppointmentBTN;
    [SerializeField] private GameObject clientNameInsertObj;
    [SerializeField] private InputField clientNameInputField;
    [SerializeField] private GameObject clientMentionsObj;
    [SerializeField] private InputField clientMentionInputField;
    [SerializeField] private GameObject errorInfoObj;
    [SerializeField] private GameObject checkIfLoggedInBTN;
    [SerializeField] private TextMeshProUGUI errorInfoTXT;
    
    private string clientMentionsWrite;

    //------end-------

    //show appointment to the barber ~~~ DAYS ~~~~
    [SerializeField] private GameObject appointmentDayPrefab;
    private GameObject appointmentDayNameObj;
    private GameObject appointmentDayNumberObj;
    private GameObject selectedAppointmentDayObj;
    private List<GameObject> appointmentDayList = new List<GameObject>();
    private string selectedAppointmentDay;
    //show appointment to the barber ~~~ APPOINTMENT INFO ~~~
    private string[] barberOccupiedMinutes = new string[130];
    private string[] barberOccupiedHours = new string[130];
    private bool[] barberOccupiedDays = new bool[33];
    private string[] clientName = new string[130];
    private string[] appointmentPrice = new string[130];
    private string[] appointmentInfo = new string[130]; //info given by the client when the appointment was created
    private List<GameObject> occupiedAppointments = new List<GameObject>();
    [SerializeField] private GameObject occupiedAppointmentPrefab;
    private TextMeshProUGUI appointmentClientName;
    private TextMeshProUGUI occupiedAppointmentHour;
    private TextMeshProUGUI occupiedAppointmentMinute;
    private TextMeshProUGUI occupiedAppointmentPrice;
    [SerializeField] private TextMeshProUGUI occupiedAppointmentInfo;
    private int occupiedAppointmentCounter = 0; //increases for every appointment that has to be shown
    //------end-------
    //DELETE THE APPOINTMENT ~~BARBER~~
    private GameObject selectedAppointment;

    //~~~ END ~~~







    public void BackToCalendar()
    {
        backFromHoursBTN.SetActive(false);
        closeAppointmentBTN.SetActive(true);
        minutesScrollView.SetActive(false);
        hoursScrollView.SetActive(false);
        daysScrollView.SetActive(true);
        nextMonthDateBTN.SetActive(true);
        previousMonthDateBTN.SetActive(true);
        checkIfLoggedInBTN.SetActive(false);
        clientMentionsObj.SetActive(false);
        checkHoursBTN.SetActive(true);
        nextToServicesBTN.SetActive(false);

    }
    private void Start()
    {
        currentDateString = " / " + DateTime.Now.Month + " / " + DateTime.Now.Year;
        currentDateTXT.text = currentDateString;
        checkHoursBTN = appointmentMenu.transform.Find("SelectedDateBG").gameObject.transform.Find("CheckHoursBTN").gameObject;
        nextMonthDateBTN = selectedDateBG.transform.Find("NextPrevButtons").gameObject.transform.Find("NextMonthBTN").gameObject;
        previousMonthDateBTN = selectedDateBG.transform.Find("NextPrevButtons").gameObject.transform.Find("PreviousMonthBTN").gameObject;
        hoursScrollView = calendarBG.transform.Find("HoursScrollView").gameObject;
        minutesScrollView = calendarBG.transform.Find("MinutesScrollView").gameObject;
        daysScrollView = calendarBG.transform.Find("DaysScrollView").gameObject;
        closeAppointmentBTN = appointmentMenu.transform.Find("SelectedDateBG").gameObject.transform.Find("CloseAppointmentMenu").gameObject;
        backFromHoursBTN = appointmentMenu.transform.Find("SelectedDateBG").gameObject.transform.Find("BackBTN(Hours)").gameObject;
        nextToInsertClientNameBTN = selectedDateBG.transform.Find("CheckIfLoggedIn").gameObject;
        nextToServicesBTN = selectedDateBG.transform.Find("NextToServicesBTN").gameObject;
        servicesBG = selectedDateBG.transform.Find("BarberServicesBG").gameObject;
        backToHoursBTN = servicesBG.transform.Find("BackToHoursBTN").gameObject;
        haircutServiceButton = servicesBG.transform.Find("HaircutButton").gameObject;
        beardServiceButton = servicesBG.transform.Find("BeardButton").gameObject;
        mustacheServiceButton = servicesBG.transform.Find("MustacheButton").gameObject;
        colourServiceButton = servicesBG.transform.Find("ColourButton").gameObject;
        eyebrowServiceButton = servicesBG.transform.Find("EyebrowButton").gameObject;
    }

    public void CheckForSelectedTime()
    {
        string hour = selectedHour.ToString();
        string minute = selectedMinute.ToString();
        if (accountClass.IsLogged)
        {
            if (hour != null && minute != null)
            {
                createAppointmentBTN.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (hour != null && minute != null)
            {
                nextToInsertClientNameBTN.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void CheckIfUserIsLoggedIn()
    {
        if (accountClass.IsLogged)
        {
            Debug.Log("e logat");
            nextToInsertClientNameBTN.SetActive(false);
            createAppointmentBTN.SetActive(true);
        }
        else
        {
            Debug.Log("nu e logat");
            nextToInsertClientNameBTN.SetActive(true);
        }
    }
    public void NextToServices()
    {
        servicesBG.SetActive(true);
        nextToServicesBTN.SetActive(false);
        clientMentionsObj.SetActive(false);
        backFromHoursBTN.SetActive(false);
    }
    public void BackToHours()
    {
        servicesBG.SetActive(false);
        nextToServicesBTN.SetActive(true);
        backFromHoursBTN.SetActive(true);
        clientMentionsObj.SetActive(true);
        checkIfLoggedInBTN.SetActive(false);
        createAppointmentBTN.SetActive(false);
    }
    /* cand e butonul selectat se seteaza variabila bool sa fie inversul a valorii de era
    * daca este true, adica daca e butonu selectat se face culoarea lui mai gri sa para selectat
    * daca e false se face invers, se face alb sa para deselectat
    * daca este selectat butonul se adauga pretul serviciului respectiv la totalPrice si se updateaza textul afisat
    */
    public void selectHaircutService()
    {
        haircutService = !haircutService;
        if (haircutService)
        {
            haircutServiceButton.GetComponent<Image>().color = new Color(0.56f, 0.56f, 0.56f, 1f);
            totalPrice += barberHaircutPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        else
        {
            haircutServiceButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            totalPrice -= barberHaircutPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        CheckIfUserIsLoggedIn(); //cand e selectat unul dintre servicii sa verifice daca e useru logat sau nu mancamiar pl
    }
    public void selectBeardService()
    {
        beardService = !beardService;
        if (beardService)
        {
            beardServiceButton.GetComponent<Image>().color = new Color(0.56f, 0.56f, 0.56f, 1f);
            totalPrice += barberBeardPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        else
        {
            beardServiceButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            totalPrice -= barberBeardPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        CheckIfUserIsLoggedIn();
    }
    public void selectMustacheService()
    {
        mustacheService = !mustacheService;
        if (mustacheService)
        {
            mustacheServiceButton.GetComponent<Image>().color = new Color(0.56f, 0.56f, 0.56f, 1f);
            totalPrice += barberMustachePrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        else
        {
            mustacheServiceButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            totalPrice -= barberMustachePrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        CheckIfUserIsLoggedIn();
    }
    public void selectColourService()
    {
        colourService = !colourService;
        if (colourService)
        {
            colourServiceButton.GetComponent<Image>().color = new Color(0.56f, 0.56f, 0.56f, 1f);
            totalPrice += barberColourPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        else
        {
            colourServiceButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            totalPrice -= barberColourPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        CheckIfUserIsLoggedIn();
    }
    public void selectEyebrowService()
    {
        eyebrowService = !eyebrowService;
        if (eyebrowService)
        {
            eyebrowServiceButton.GetComponent<Image>().color = new Color(0.56f, 0.56f, 0.56f, 1f);
            totalPrice += barberEyebrowPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        else
        {
            eyebrowServiceButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            totalPrice -= barberEyebrowPrice;
            if (appmanagerClass.SelectedLanguage == 2)
            {
                totalServicesPrice.text = "Price: " + totalPrice;
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                totalServicesPrice.text = "Pret: " + totalPrice;
            }
        }
        CheckIfUserIsLoggedIn();
    }
    public void NextToInsertClientName() //after selecting the hour and minute click the button to insert client name if user is not logged in
    {
        clientNameInsertObj.SetActive(true);
        nextToInsertClientNameBTN.SetActive(false);
        createAppointmentBTN.SetActive(true);
    }
    public void CloseInsertClientName() //when clicking the close button to close the menu to insert a client name when user not logged in
    {
        minutesScrollView.SetActive(false);
        clientMentionsObj.SetActive(false);
        hoursScrollView.SetActive(false);
        daysScrollView.SetActive(true);
        nextToInsertClientNameBTN.SetActive(false);
        checkHoursBTN.SetActive(true);
        createAppointmentBTN.SetActive(false);
        clientNameInsertObj.SetActive(false);
        nextMonthDateBTN.SetActive(true);
        previousMonthDateBTN.SetActive(true);
        servicesBG.SetActive(false);
        closeAppointmentBTN.SetActive(true);
    }
    public void CheckBarberAppointmentsDays() //check which days are occupied
    {
        StartCoroutine(CheckBarberAppointmentsEnum());
    }
    IEnumerator CheckBarberAppointmentsEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("barberName", accountClass.BarberName);
        form.AddField("currentMonth", currentMonth);
        form.AddField("currentYear", currentYear);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/checkbarberappdays.php", form);
        yield return www;
        string[] days = new string[33];
        for (int i = 0; i < www.text.Split('\t').Length; i++)
        {
            days[i] = www.text.Split('\t')[i];
            for (int k = 0; k < days.Length; k++)
            {
                foreach (string str in days)
                {
                    if (str == k.ToString())
                    {
                        barberOccupiedDays[int.Parse(str)] = true;
                    }
                }
            }
        }
    }
    public void CheckBarberAppointmentsTime()
    {
        StartCoroutine(CheckBarberAppointmentsTimeEnum());
    }

    IEnumerator CheckBarberAppointmentsTimeEnum()
    {
        int WTP = 1;
        for (int i = 0; i < 5; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("barberName", accountClass.BarberName);
            form.AddField("currentMonth", currentMonth);
            form.AddField("currentYear", currentYear);
            form.AddField("selectedDay", selectedAppointmentDay);
            form.AddField("wtp", WTP);
            WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/getoccupiedtime.php", form);
            yield return www;
            switch (WTP)
            {
                case 1:
                    for(int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        barberOccupiedHours[j] = www.text.Split('\t')[j];
                        occupiedAppointmentCounter++;
                    }
                    WTP = 2;
                    break;
                case 2:
                    for (int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        barberOccupiedMinutes[j] = www.text.Split('\t')[j];
                    }
                    WTP = 3;
                    break;
                case 3:
                    for(int j = 0; j<www.text.Split('\t').Length; j++)
                    {
                        clientName[j] = www.text.Split('\t')[j];
                    }
                    WTP = 4;
                    break;
                case 4:
                    for (int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        appointmentInfo[j] = www.text.Split('\t')[j];
                    }
                    WTP = 5;
                    break;
                case 5:
                    for(int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        appointmentPrice[j] = www.text.Split('\t')[j];
                    }
                    WTP = 1;
                    break;
            }
        }
        CreateOccupiedAppointments();
    }
    public void BarberSelectAppointment()
    {
        selectedAppointment = EventSystem.current.currentSelectedGameObject;
    }
    public void BarberDeleteAppointment()
    {
        StartCoroutine(BarberDeleteAppointmentEnum());
    }
    IEnumerator BarberDeleteAppointmentEnum()
    {
        string hour = selectedAppointment.transform.Find("Hour").gameObject.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>().text;
        string minute = selectedAppointment.transform.Find("Minute").gameObject.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>().text;        
        WWWForm form = new WWWForm();
        form.AddField("clientName", selectedAppointment.transform.Find("ClientName").gameObject.GetComponent<TextMeshProUGUI>().text);
        form.AddField("appHour", hour);
        form.AddField("appMinute", minute);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/barberdeleteapp.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            if (appmanagerClass.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Appointment deleted succesfully !";
                errorInfoObj.SetActive(true);
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Programare stearsa cu succes !";
                errorInfoObj.SetActive(true);
            }
            Destroy(selectedAppointment);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void CreateOccupiedAppointments()
    {
        loadingScreen.SetActive(true);
        if(occupiedAppointments.Count > 0)
        {
            foreach(GameObject obj in occupiedAppointments)
            {
                Destroy(obj);
            }
            occupiedAppointments.Clear();
        }
        for(int i = 0; i < occupiedAppointmentCounter-1; i++)
        {
            GameObject appointment = Instantiate(occupiedAppointmentPrefab);
            appointment.transform.SetParent(occupiedAppointmentPrefab.transform.parent);
            appointment.SetActive(true);
            appointment.name = i.ToString();
            appointment.transform.localScale = new Vector3(1f, 1f, 1f);
            appointment.transform.localPosition = new Vector3(appointment.transform.position.x, appointment.transform.position.y, 0f);
            GameObject hour = appointment.transform.Find("Hour").gameObject;
            occupiedAppointmentHour = hour.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
            occupiedAppointmentHour.text = barberOccupiedHours[i];
            GameObject minute = appointment.transform.Find("Minute").gameObject;
            occupiedAppointmentMinute = minute.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
            occupiedAppointmentMinute.text = barberOccupiedMinutes[i];
            appointmentClientName = appointment.transform.Find("ClientName").gameObject.GetComponent<TextMeshProUGUI>();
            appointmentClientName.text = clientName[i];
            GameObject price = appointment.transform.Find("Price").gameObject;
            occupiedAppointmentPrice = price.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
            occupiedAppointmentPrice.text = appointmentPrice[i];
            occupiedAppointments.Add(appointment);
        }
        occupiedAppointmentCounter = 0;
        loadingScreen.SetActive(false);
    }
    public void ShowBarberAppointmentInfo() //show the appointment info to the barber based on the parent's name of the infoButton clicked.
    {
        occupiedAppointmentInfo.text = appointmentInfo[int.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.name)];
    }
    public void CreateAppointmentDays()
    {
        loadingScreen.SetActive(true);
        if(appointmentDayList.Count > 0)
        {
            foreach(GameObject obj in appointmentDayList)
            {
                Destroy(obj);
            }
            appointmentDayList.Clear();
        }
        for(int i = 1; i < DateTime.DaysInMonth(currentYear, currentMonth) + 1; i++)
        {
            GameObject day = Instantiate(appointmentDayPrefab);
            day.name = i.ToString();
            day.transform.SetParent(appointmentDayPrefab.transform.parent);
            day.SetActive(true);
            day.transform.localScale = new Vector3(1f, 1f, 1f);
            day.transform.localPosition = new Vector3(day.transform.position.x, day.transform.position.y, 0f);
            appointmentDayNumberObj = day.transform.Find("DayNumber").gameObject;
            appointmentDayNumberObj.GetComponent<TextMeshProUGUI>().text = i.ToString();
            appointmentDayNameObj = day.transform.Find("DayName").gameObject;
            currentDate = new DateTime(currentYear, currentMonth, i);
            appointmentDayNameObj.GetComponent<TextMeshProUGUI>().text = currentDate.DayOfWeek.ToString();
            appointmentDayList.Add(day);
            if (barberOccupiedDays[i] == true)
            {
                day.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
            }
        }
        loadingScreen.SetActive(false);
    }

    public void SelectAppointmentDay()
    {
        selectedAppointmentDayObj = EventSystem.current.currentSelectedGameObject;
        selectedAppointmentDay = selectedAppointmentDayObj.name;
        CheckBarberAppointmentsTime();
    }

    public void ResetDateTime()
    {
        currentMonth = DateTime.Now.Month;
        currentYear = DateTime.Now.Year;
        currentDateString = " / " + DateTime.Now.Month + " / " + DateTime.Now.Year;
        currentDateTXT.text = currentDateString;
    }
    public void CreateCalendar()
    {
        loadingScreen.SetActive(true);
        //if the list is not empty destroy every object in that list
        if (daysObjects.Count > 0)
        {
            foreach (GameObject dayobj in daysObjects)
            {
                Destroy(dayobj.gameObject);
            }
            daysObjects.Clear();
        }
        //loop to create the objects for each day in the selected month
        for (int i = 1; i < DateTime.DaysInMonth(currentYear, currentMonth) + 1; i++)
        {
            GameObject day = Instantiate(dayPrefab);//instantiate the object with the prefab
            day.transform.SetParent(dayPrefab.transform.parent);//set the new created object to have the same parent as the prefab
            day.SetActive(true); //set the object active
            day.name = i.ToString(); //set the name of the object to be the number of that day
            //for some reason the scale was altered after creating a new object so we set it to be the correct one
            day.transform.localScale = new Vector3(1f, 1f, 1f);
            //same goes for the position, it was set behind the camera and the object could not be seen so we set the Z pos to 0f
            day.transform.localPosition = new Vector3(day.transform.position.x, day.transform.position.y, 0f);
            //set the object variable to be the object that holds the day number text
            dayNumberObj = day.transform.Find("DayNumber").gameObject;
            dayNumberObj.GetComponent<TextMeshProUGUI>().text = i.ToString();
            //set the object variable to be the object that holds the day name text
            dayNameObj = day.transform.Find("DayName").gameObject;
            //set the date to be the current one with i being the last day created
            currentDate = new DateTime(currentYear, currentMonth, i);
            //set the text of the object to be the name of the last created day.
            dayNameObj.GetComponent<TextMeshProUGUI>().text = currentDate.DayOfWeek.ToString();
            //add the object to the dayObjects list
            daysObjects.Add(day);
        }
        loadingScreen.SetActive(false);
    }
    public void SelectDay()
    {
        //if the lastday image is not null (or an day has been selected already)
        if (lastDaySelectedImg != null)
        {
            //set the color of the image to be the default one
            lastDaySelectedImg.color = new Color(0f, 0.65f, 0f);
        }
        //set the object variable to be the current selected day object
        selectedDayObj = EventSystem.current.currentSelectedGameObject;
        //get the image component of that object
        selectedDayImg = selectedDayObj.GetComponent<Image>();
        //set the color to be a little bit darker so the object looks like its pressed
        selectedDayImg.color = new Color(0f, 0.40f, 0f);
        //set the lastday image to be the image of the current selected day object
        lastDaySelectedImg = selectedDayImg;
        //get the name of the selected day
        DateTime date = new DateTime(currentYear, currentMonth, int.Parse(selectedDayObj.name));
        //update the string of the current date textbox and
        selectedAppointmentDay = selectedDayObj.name;
        selectedDayString = date.DayOfWeek.ToString();
        UpdateCurrentDate();
        createAppointmentHours.interactable = true;
        
    }
    public string BarberFirstName
    {
        get { return barberFirstName; }
    }
    public string BarberLastName
    {
        get { return barberLastName; }
    }
    public void UpdateCurrentDate()
    {
        currentDateString = selectedAppointmentDay + " / " + currentDate.Month + " / " + currentYear;
        currentDateTXT.text = currentDateString;
    }
    public void NextMonth()
    {
        currentMonth++;
        selectedDayString = "";
        selectedAppointmentDay = "";
        if (currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }
        CreateCalendar();
        UpdateCurrentDate();
    }
    public void PreviousMonth()
    {
        currentMonth--;
        selectedDayString = "";
        selectedAppointmentDay = "";
        if (currentMonth < 1)
        {
            currentMonth = 12;
            currentYear--;
        }
        CreateCalendar();
        UpdateCurrentDate();
    }
    public void SelectBarber()
    {
        selectedBarberObj = EventSystem.current.currentSelectedGameObject;
        barberFirstName = selectedBarberObj.name.Split('\t')[0];
        barberLastName = selectedBarberObj.name.Split('\t')[1];
        GetTimeToCut(); //get the time needed for the barber to do a cut
        appointmentMenu.SetActive(true);
        GetBarberPrices();
        selectedAppointmentDay = DateTime.Now.Day.ToString();
    }
    public void GetBarberPrices()
    {
        StartCoroutine(GetBarberPricesEnum());
    }
    IEnumerator GetBarberPricesEnum()
    {
        loadingScreen.SetActive(true);
        List<IMultipartFormSection> newform = new List<IMultipartFormSection>();
        newform.Add(new MultipartFormDataSection("barberFirstName", barberFirstName));
        newform.Add(new MultipartFormDataSection("barberLastName", barberLastName));
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getbarberprices.php", newform);
        yield return webreq.SendWebRequest();
        if (webreq.isNetworkError || webreq.isHttpError)
        {
            Debug.Log(webreq.error);
        }
        else
        {
            barberHaircutPrice = int.Parse(webreq.downloadHandler.text.Split('\t')[1]);
            barberBeardPrice = int.Parse(webreq.downloadHandler.text.Split('\t')[2]);
            barberMustachePrice = int.Parse(webreq.downloadHandler.text.Split('\t')[3]);
            barberColourPrice = int.Parse(webreq.downloadHandler.text.Split('\t')[4]);
            barberEyebrowPrice = int.Parse(webreq.downloadHandler.text.Split('\t')[5]);
            barberHaircutPriceTXT.text = barberHaircutPrice.ToString();
            barberBeardPriceTXT.text = barberBeardPrice.ToString();
            barberMustachePriceTXT.text = barberMustachePrice.ToString();
            barberColourPriceTXT.text = barberColourPrice.ToString();
            barberEyebrowPriceTXT.text = barberEyebrowPrice.ToString();
            loadingScreen.SetActive(false);
        }
    }
    public void SelectHour()
    {
        selectedHour = int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name);
        CheckForAvailableAppointment();
        //if the lasthour image is not null (or an hour button has been selected already)
        if (lastSelectedHourImage != null)
        {
            //set the color of the image to be the default one
            lastSelectedHourImage.color = new Color(1f, 1f, 1f);
        }
        //set the object variable to be the current selected hour object
        selectedHourImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        //set the color to be a little bit darker so the object looks like its pressed
        selectedHourImage.color = new Color(0.62f, 0.62f, 0.62f);
        //set the lastday image to be the image of the current selected hour object
        lastSelectedHourImage = selectedHourImage;
    }
    public void SelectMinute()
    {
        selectedMinute = int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name);
        if (lastSelectedMinuteImage != null)
        {
            lastSelectedMinuteImage.color = new Color(1f, 1f, 1f);
        }
        selectedMinuteImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        selectedMinuteImage.color = new Color(0.62f, 0.62f, 0.62f);
        lastSelectedMinuteImage = selectedMinuteImage;
    }
    public void CheckForAvailableAppointment()
    {
        StartCoroutine(CheckForAvailableAppointmentEnum());
    }
    IEnumerator CheckForAvailableAppointmentEnum()
    {
        for(int j =0; j<2; j++)
        {
            WWWForm form = new WWWForm();
            form.AddField("barberName", barberFirstName + barberLastName);
            form.AddField("selectedDay", selectedDayObj.name);
            form.AddField("selectedMonth", currentMonth);
            form.AddField("selectedYear", currentYear);
            form.AddField("selectedHour", selectedHour);
            form.AddField("selectedMinute", selectedMinute);
            form.AddField("whatToPick", WhatToPick);
            form.AddField("firstname", barberFirstName);
            form.AddField("lastname", barberLastName);
            WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/checkforappointment.php", form);
            yield return www;
            string[] hours = new string[24];
            string[] minutes = new string[60];
            switch (WhatToPick)
            {
                case 1:
                    for(int i = 0; i < www.text.Split('\t').Length; i++)
                    {
                        hours[i] = www.text.Split('\t')[i];
                        for(int k=0; k< hours.Length; k++)
                        {
                            foreach(string str in hours)
                            {
                                if(str == k.ToString())
                                {
                                    occupiedHours[int.Parse(str)] = true;
                                }
                            }
                        }
                    }
                    WhatToPick = 2;
                    break;
                case 2:
                    for(int i = 0; i < www.text.Split('\t').Length; i++)
                    {
                        minutes[i] = www.text.Split('\t')[i];
                    }
                    for (int i = 0; i < minutes.Length; i++)
                    {
                        foreach (string str in minutes)
                        {
                            if (str == i.ToString())
                            {
                                occupiedMinutes[int.Parse(str)] = true;
                            }
                        }
                    }
                    WhatToPick = 1;
                    break;
            }
        }
        CreateMinutes();
    }
    public void GetTimeToCut()
    {
        StartCoroutine(GetTimeToCutEnum());
    }
    IEnumerator GetTimeToCutEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstname", barberFirstName);
        form.AddField("lastname", barberLastName);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/getttc.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            timeToCut = int.Parse(www.text.Split('\t')[1]);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void UpdateTimeToCut()
    {
        StartCoroutine(UpdateTimeToCutEnum());
    }
    IEnumerator UpdateTimeToCutEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("timetocut", timeToCutInput.text);
        form.AddField("username", accountClass.AccountUsername);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/updatetimetocut.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("update time cut");
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void CreateAppointmentHours()
    {
        StartCoroutine(CreateAppointmentHoursEnum());
    }
    IEnumerator CreateAppointmentHoursEnum()
    { 
        if(selectedDayString == "" || selectedDayString == null)
        {
            if (appmanagerClass.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "You must select a day before checking it's program.";
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Trebuie sa selectezi o zi inainte sa verifici programul acesteia.";
            }
            errorInfoObj.SetActive(true);
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("shopname", appmanagerClass.GetSelectedShopName());
            form.AddField("selectedDay", selectedDayString);
            WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/getshopprogramapp.php", form);
            yield return www;

            if (www.text[0] == '0')
            {
                loadingScreen.SetActive(true);
                Debug.Log("success");
                string workingHours = www.text.Split('\t')[1];
                if (workingHours == null || workingHours == "")
                {
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "This day is not a working day or the working program has not been updated yet.";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Aceasta zi nu are un program de munca sau nu a fost actualizat inca.";
                    }
                    errorInfoObj.SetActive(true);
                    loadingScreen.SetActive(false);
                }
                else
                {
                    string openHM = workingHours.Split('-')[0];
                    string closeHM = workingHours.Split('-')[1];
                    openHour = int.Parse(openHM.Split(':')[0]);
                    openMinute = int.Parse(openHM.Split(':')[1]);
                    closeHour = int.Parse(closeHM.Split(':')[0]);
                    closeMinute = int.Parse(closeHM.Split(':')[1]);
                    newOpenMinutes = openMinute;
                    CreateHours();
                }
            }
            else
            {
                Debug.Log(www.text);
                if (appmanagerClass.SelectedLanguage == 2)
                {
                    errorInfoTXT.text = "Something went wrong. If this error continues to happen please contact the developer.";
                }
                else if (appmanagerClass.SelectedLanguage == 1)
                {
                    errorInfoTXT.text = "Ceva nu a functionat ! Daca problema continua va rugam sa contactati un administrator.";
                }
                errorInfoObj.SetActive(true);
            }
        }
    }
    public void CreateHours() //function to create the hours buttons to create a new appointment
    {
        if(hourObjects.Count > 0) //if the list is not empty
        {
            foreach(GameObject obj in hourObjects)
            {
                Destroy(obj); //destroy the object
            }
            hourObjects.Clear(); //clear the list
        }
        for(int i = openHour; i<closeHour+1; i++)
        {
            GameObject hour = Instantiate(hourPrefab); //instantiate the new object
            hour.transform.SetParent(hourPrefab.transform.parent); //set the parent to be the same as the prefab's
            hour.name = i.ToString(); //set the name of the object to be the hour number
            hour.GetComponentInChildren<Text>().text = i.ToString(); //set the text to show the hour
            //set z position to 0 so it can be seen
            hour.transform.localPosition = new Vector3(hourPrefab.transform.position.x, hourPrefab.transform.position.y, 0f);
            hour.transform.localScale = new Vector3(1f, 1f, 1f);
            hour.SetActive(true); //set the object to be visible
            hourObjects.Add(hour); //add the object to the list
        }
        loadingScreen.SetActive(false);
        daysScrollView.SetActive(false);
        minutesScrollView.SetActive(true);
        hoursScrollView.SetActive(true);
        closeAppointmentBTN.SetActive(false);
        backFromHoursBTN.SetActive(true);
        nextMonthDateBTN.SetActive(false);
        previousMonthDateBTN.SetActive(false);
        //checkIfLoggedInBTN.SetActive(true);
        nextToServicesBTN.SetActive(true);
        checkHoursBTN.SetActive(false);
        clientMentionsObj.SetActive(true);
    }
    public void CreateMinutes()//function to create the minutes buttons to create a new appointment
    {
        if(minuteObjects.Count > 0)
        {
            foreach(GameObject obj in minuteObjects)
            {
                Destroy(obj);
            }
            minuteObjects.Clear();
        }

        //if true-> the loop starts from min 00 and stops at the minute taken from from the shop's working program
        if(selectedHour == closeHour)
        {
            newOpenMinutes = 0;
            newCloseMinutes = closeMinute;
        }
        //else it stops at 59;
        else
        {
            newCloseMinutes = 59;
        }

        //if true-> the loop starts from the minute taken from the shop's working program and stops at minute 59
        if(selectedHour == openHour)
        {
            newOpenMinutes = openMinute;
            newCloseMinutes = 59;
        }
        //else the loop starts at minute 00;
        else
        {
            newOpenMinutes = 0;
        }

        if(timeToCut != 0)
        {
            for (int i = newOpenMinutes; i < newCloseMinutes+1; i += timeToCut)
            {
                GameObject minute = Instantiate(minutePrefab);
                minute.transform.SetParent(minutePrefab.transform.parent);
                if (i < 10)
                {
                    minute.name = "0" + i.ToString();
                    minute.GetComponentInChildren<Text>().text = "0" + i.ToString();
                }
                else
                {
                    minute.name = i.ToString();
                    minute.GetComponentInChildren<Text>().text = i.ToString();
                }
                minute.transform.localPosition = new Vector3(minutePrefab.transform.position.x, minutePrefab.transform.position.y, 0f);
                minute.transform.localScale = new Vector3(1f, 1f, 1f);
                minute.SetActive(true);
                minuteObjects.Add(minute);
                //if the created minute button is occupied it becomes red and not interactable
                if(occupiedMinutes[i] == true && occupiedHours[selectedHour] == true) 
                {
                    minute.GetComponent<Button>().interactable = false;
                    minute.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
                }
            }
        }
        else
        {
            Debug.Log("rasamati timetocut nu e0");
        }
    }
    public void CreateAppointment()
    {
        StartCoroutine(CreateAppointmentEnumerator());
    }
    IEnumerator CreateAppointmentEnumerator()
    {
        if (accountClass.IsLogged)
        {
            {
                WWWForm form = new WWWForm();
                form.AddField("barberName", barberFirstName + barberLastName);
                form.AddField("clientName", accountClass.AccountUsername);
                form.AddField("day", selectedDayObj.name);
                form.AddField("month", currentMonth);
                form.AddField("year", currentYear);
                form.AddField("hour", selectedHour);
                form.AddField("minute", selectedMinute);
                form.AddField("mentions", clientMentionInputField.text);
                form.AddField("totalprice", totalPrice);
                WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/createappointment.php", form);
                yield return www;
                daysScrollView.SetActive(true);
                hoursScrollView.SetActive(false);
                minutesScrollView.SetActive(false);
                closeAppointmentBTN.SetActive(true);
                backFromHoursBTN.SetActive(false);
                nextMonthDateBTN.SetActive(true);
                previousMonthDateBTN.SetActive(true);
                checkHoursBTN.SetActive(true);
                createAppointmentBTN.SetActive(false);
                appointmentMenu.SetActive(false);
                clientMentionsObj.SetActive(false);
                fullAppointmentMenu.SetActive(false);
                if (www.text[0] == '0')
                {
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "Appointment successfully created !";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Programare creata cu succes !";
                    }
                    errorInfoObj.SetActive(true);
                }
                else if (www.text[0] == '3')
                {
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "There is already an appointment for created on this date and hour. Try selecting another hour/minute.";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Deja exista o programare creata pentru aceasta zi si ora. Incearca sa selectezi alta ora.";
                    }
                    errorInfoObj.SetActive(true);
                }
                else
                {
                    Debug.Log(www.text);
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "Something went wrong! Failed to create your appointment. Try again in a few minutes.";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Ceva nu a functionat. Programarea nu a fost creata. Incearca din nou in cateva minute.";
                    }
                    errorInfoObj.SetActive(true);
                }
            }
        }
        else
        {
            if (clientNameInputField.text == null || clientNameInputField.text == "")
            {
                if (appmanagerClass.SelectedLanguage == 2)
                {
                    errorInfoTXT.text = "Please insert a real name so the barber will know you";
                }
                else if (appmanagerClass.SelectedLanguage == 1)
                {
                    errorInfoTXT.text = "Introdu un nume real pentru ca frizerul sa te cunoasca.";
                }
                errorInfoObj.SetActive(true);
            }
            else
            {
                WWWForm form = new WWWForm();
                form.AddField("barberName", barberFirstName + barberLastName);
                form.AddField("clientName", clientNameInputField.text);
                form.AddField("day", selectedDayObj.name);
                form.AddField("month", currentMonth);
                form.AddField("year", currentYear);
                form.AddField("hour", selectedHour);
                form.AddField("minute", selectedMinute);
                form.AddField("mentions", clientMentionInputField.text);
                form.AddField("totalprice", totalPrice);
                WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/createappointment.php", form);
                yield return www;
                daysScrollView.SetActive(true);
                hoursScrollView.SetActive(false);
                minutesScrollView.SetActive(false);
                closeAppointmentBTN.SetActive(true);
                backFromHoursBTN.SetActive(false);
                nextMonthDateBTN.SetActive(true);
                previousMonthDateBTN.SetActive(true);
                checkHoursBTN.SetActive(true);
                createAppointmentBTN.SetActive(false);
                appointmentMenu.SetActive(false);
                clientMentionsObj.SetActive(false);
                fullAppointmentMenu.SetActive(false);
                clientNameInsertObj.SetActive(false);
                if (www.text[0] == '0')
                {
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "Appointment successfully created !";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Programare creata cu succes !";
                    }
                    errorInfoObj.SetActive(true);
                }
                else if (www.text[0] == '3')
                {
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "There is already an appointment for created on this date and hour. Try selecting another hour/minute.";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Deja exista o programare creata pentru aceasta zi si ora. Incearca sa selectezi alta ora.";
                    }
                    errorInfoObj.SetActive(true);
                }
                else
                {
                    Debug.Log(www.text);
                    if (appmanagerClass.SelectedLanguage == 2)
                    {
                        errorInfoTXT.text = "Something went wrong! Failed to create your appointment. Try again in a few minutes.";
                    }
                    else if (appmanagerClass.SelectedLanguage == 1)
                    {
                        errorInfoTXT.text = "Ceva nu a functionat. Programarea nu a fost creata. Incearca din nou in cateva minute.";
                    }
                    errorInfoObj.SetActive(true);
                }
            }
        }
    }
    public void ClearErrorInfo()
    {
        errorInfoTXT.text = "";
        errorInfoObj.SetActive(false);
    }
}

