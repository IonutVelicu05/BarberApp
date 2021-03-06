using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.Notifications.Android;

public class Appointments : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentDateTXT; //current date textbox
    [SerializeField] private GameObject dayPrefab; //the object from which i instantiate all days in the calendar.
    [SerializeField] private TextMeshProUGUI dayNumber; //text of the daynumber textbox;
    [SerializeField] private TextMeshProUGUI dayName; //text of the dayname textbox;
    [SerializeField] private Account accountClass;
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
    private GameObject nextMonthDateBTN;
    private GameObject previousMonthDateBTN;
    private GameObject hoursScrollView;
    private GameObject minutesScrollView;
    private GameObject daysScrollView;
    private GameObject closeAppointmentBTN;
    private GameObject backFromHoursBTN;
    private GameObject nextToServicesBTN;
    private GameObject servicesBG;
    private GameObject backToHoursBTN;

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
    [SerializeField] private GameObject errorInfoObj;
    [SerializeField] private TextMeshProUGUI errorInfoTXT;
    [SerializeField] private GameObject servicePrefab;
    [SerializeField] private TextMeshProUGUI selectDateInfoTxt;
    private List<GameObject> serviceObjectsList = new List<GameObject>();
    private string[] serviceNameListRO;
    private string[] serviceNameListEN;
    private string[] servicePriceList;
    private string[] serviceCoinNameList;
    [SerializeField] private TextMeshProUGUI totalServicesPrice;
    private int totalPrice;
    private string appointmentSelectedServices;
    private List<string> appointmentSelectedServicesList = new List<string>();
    private Color serviceButtonNormalColor = new Color(0.88f, 0.88f, 0.37f, 1f);
    private Color serviceButtonSelectedColor = new Color(0.68f, 0.68f, 0.20f, 1f);

    //------end-------

    //barber create appointments
    [SerializeField] private TextMeshProUGUI MonthAndYear;
    [SerializeField] private TextMeshProUGUI MondayLetter;
    [SerializeField] private TextMeshProUGUI TuesdayLetter;
    [SerializeField] private TextMeshProUGUI WednesdayLetter;
    [SerializeField] private TextMeshProUGUI ThursdayLetter;
    [SerializeField] private TextMeshProUGUI FridayLetter;
    [SerializeField] private TextMeshProUGUI SaturdayLetter;
    [SerializeField] private TextMeshProUGUI SundayLetter;
    [SerializeField] private GameObject calendarDayPrefab;
    private List<GameObject> calendarDaysList = new List<GameObject>();
    private int[] appointmentsInDay = new int[42];

    //notifications
    [SerializeField] private Notifications notificationsClass;
    private AndroidNotification notification;
    private DateTime notificationDate;


    //

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
    [SerializeField] private TextMeshProUGUI barberMenuCurrentMonth;
    private int barberMenuCurrentMonthNr = DateTime.Now.Month;
    private int barberMenuCurrentYearNr = DateTime.Now.Year;
    private int occupiedAppointmentCounter = 0; //increases for every appointment that has to be shown
    //------end-------
    //DELETE THE APPOINTMENT ~~BARBER~~
    private GameObject selectedAppointment;

    //~~~ END ~~~

    // BARBER's Notification
    private string barberNotificationTitleEN = "You just got a new appointment !";
    private string barberNotificationBodyEN = "Check your barber menu to see more details.";
    private string barberNotificationTitleRO = "Ai o noua programare !";
    private string barberNotificationBodyRO = "Verifica meniul frizerului pentru mai multe detalii.";
    private string reviewNotificationTitleRO = "Hey! Cum a fost experienta ta la salonul ";
    private string reviewNotificationBodyRO = "Apasa aici pentru a lasa un review.";
    private string reviewNotificationTitleEN = "Hey! How was your experience at ";
    private string reviewNotificationBodyEN = "Click here to leave a review.";





    public void BackToCalendar()
    {
        backFromHoursBTN.SetActive(false);
        closeAppointmentBTN.SetActive(true);
        minutesScrollView.SetActive(false);
        hoursScrollView.SetActive(false);
        daysScrollView.SetActive(true);
        nextMonthDateBTN.SetActive(true);
        previousMonthDateBTN.SetActive(true);
        nextToServicesBTN.SetActive(false);
        createAppointmentBTN.SetActive(false);
        foreach(GameObject obj in minuteObjects)
        {
            Destroy(obj);
        }
        minuteObjects.Clear();
        if (appmanagerClass.SelectedLanguage == 1)
        {
            selectDateInfoTxt.text = "Alege o data pentru programare";
        }
        else
        {
            selectDateInfoTxt.text = "Select a date for your appointment";
        }
    }
    private void Start()
    {
        barberMenuUpdateMonth();
        currentDateString = " / " + DateTime.Now.Month + " / " + DateTime.Now.Year;
        currentDateTXT.text = currentDateString;
        nextMonthDateBTN = selectedDateBG.transform.Find("NextPrevButtons").gameObject.transform.Find("NextMonthBTN").gameObject;
        previousMonthDateBTN = selectedDateBG.transform.Find("NextPrevButtons").gameObject.transform.Find("PreviousMonthBTN").gameObject;
        hoursScrollView = calendarBG.transform.Find("Hours&Minutes").gameObject.transform.Find("HoursScrollView").gameObject;
        minutesScrollView = calendarBG.transform.Find("Hours&Minutes").gameObject.transform.Find("MinutesScrollView").gameObject;
        daysScrollView = calendarBG.transform.Find("DaysScrollView").gameObject;
        closeAppointmentBTN = appointmentMenu.transform.Find("CloseAppointmentMenu").gameObject;
        backFromHoursBTN = appointmentMenu.transform.Find("BackBTN(Hours)").gameObject;
        nextToServicesBTN = appointmentMenu.transform.Find("NextToServicesBTN").gameObject;
        servicesBG = calendarBG.transform.Find("ServicesBG").gameObject;
        backToHoursBTN = appointmentMenu.transform.Find("BackToHoursBTN").gameObject;
    }

    public void NextToServices()
    {
        GetShopServices();
        servicesBG.SetActive(true);
        nextToServicesBTN.SetActive(false);
        backFromHoursBTN.SetActive(false);
        backToHoursBTN.SetActive(true);
        if(appmanagerClass.SelectedLanguage == 1)
        {
            selectDateInfoTxt.text = "Alege ce servicii doresti";
        }
        else
        {
            selectDateInfoTxt.text = "Select what services do you want.";
        }
    }
    public void barberMenuNextMonth()
    {
        if(barberMenuCurrentMonthNr + 1 > 12)
        {
            barberMenuCurrentYearNr++;
            barberMenuCurrentMonthNr = 1;
        }
        else
        {
            barberMenuCurrentMonthNr++;
        }
        barberMenuUpdateMonth();
        CheckBarberAppointmentsDays();
    }
    public void barberMenuPreviousMonth()
    {
        if(barberMenuCurrentMonthNr - 1 < 1)
        {
            barberMenuCurrentMonthNr = 12;
            barberMenuCurrentYearNr--;
        }
        else
        {
            barberMenuCurrentMonthNr--;
        }
        barberMenuUpdateMonth();
        CheckBarberAppointmentsDays();
    }
    public void barberMenuUpdateMonth()
    {
        switch (barberMenuCurrentMonthNr)
        {
            case 1:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Ianuarie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "January";
                }
                break;
            case 2:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Februarie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "February";
                }
                break;
            case 3:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Martie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "March";
                }
                break;
            case 4:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Aprilie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "April";
                }
                break;
            case 5:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Mai";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "May";
                }
                break;
            case 6:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Iunie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "June";
                }
                break;
            case 7:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Iulie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "July";
                }
                break;
            case 8:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "August";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "August";
                }
                break;
            case 9:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Septembrie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "September";
                }
                break;
            case 10:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Octombrie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "October";
                }
                break;
            case 11:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Noiembrie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "November";
                }
                break;
            case 12:
                if (appmanagerClass.SelectedLanguage == 1)
                {
                    barberMenuCurrentMonth.text = "Decembrie";
                }
                else if (appmanagerClass.SelectedLanguage == 2)
                {
                    barberMenuCurrentMonth.text = "December";
                }
                break;
        }
    }
    public void BackToHours()
    {
        CreateAppointmentHours();
        createAppointmentBTN.SetActive(false);
        servicesBG.SetActive(false);
        nextToServicesBTN.SetActive(false);
        backFromHoursBTN.SetActive(true);
        backToHoursBTN.SetActive(false);
        totalPrice = 0;
        if (minuteObjects.Count > 0)
        {
            foreach (GameObject obj in minuteObjects)
            {
                Destroy(obj);
            }
            minuteObjects.Clear();
        }
        //totalServicesPrice.text = totalPrice.ToString();

        if (appmanagerClass.SelectedLanguage == 1)
        {
            selectDateInfoTxt.text = "Alege o ora pentru programare.";
        }
        else if(appmanagerClass.SelectedLanguage == 2)
        {
            selectDateInfoTxt.text = "Select an hour for your appointment.";
        }
    }
    public void CheckBarberAppointmentsDays() //check which days are occupied
    {
        StartCoroutine(CheckBarberAppointmentsEnum());
    }
    IEnumerator CheckBarberAppointmentsEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("barberName", accountClass.BarberName);
        form.AddField("currentMonth", barberMenuCurrentMonthNr);
        form.AddField("currentYear", currentYear);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/checkbarberappdays.php", form);
        yield return www;
        string[] days = new string[36];
        for(int i = 0; i < barberOccupiedDays.Length; i++)
        {
            barberOccupiedDays[i] = false;
        }
        for (int i = 0; i < www.text.Split('\t').Length; i++)
        {
            for(int j = 0; j < days.Length; j++)
            {
                if(days[j] == www.text.Split('\t')[i])
                {

                }
                else
                {
                    days[i] = www.text.Split('\t')[i];

                }
            }
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
        CreateAppointmentDays();
        www.Dispose();
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
            form.AddField("currentMonth", barberMenuCurrentMonthNr);
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
                    www.Dispose();
                    WTP = 2;
                    break;
                case 2:
                    for (int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        barberOccupiedMinutes[j] = www.text.Split('\t')[j];
                    }
                    www.Dispose();
                    WTP = 3;
                    break;
                case 3:
                    for(int j = 0; j<www.text.Split('\t').Length; j++)
                    {
                        clientName[j] = www.text.Split('\t')[j];
                    }
                    www.Dispose();
                    WTP = 4;
                    break;
                case 4:
                    for (int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        appointmentInfo[j] = www.text.Split('\t')[j];
                    }
                    www.Dispose();
                    WTP = 5;
                    break;
                case 5:
                    for(int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        appointmentPrice[j] = www.text.Split('\t')[j];
                    }
                    www.Dispose();
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
            occupiedAppointments.Remove(selectedAppointment);
            if (occupiedAppointments.Count < 1)
            {
                barberOccupiedDays[int.Parse(selectedAppointmentDay)] = false;
                CreateAppointmentDays();
            }
        }
        else
        {
            Debug.Log(www.text);
        }
        www.Dispose();
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
            //appointmentClientName.text = clientName[i];
            GameObject price = appointment.transform.Find("Price").gameObject;
            occupiedAppointmentPrice = price.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
            occupiedAppointmentPrice.text = appointmentPrice[i];
            if (appmanagerClass.SelectedLanguage == 1)
            {
                hour.GetComponent<TextMeshProUGUI>().text = "Ora : " + barberOccupiedHours[i] + " : " + barberOccupiedMinutes[i];
                appointmentClientName.text = "Nume: " + clientName[i];
                price.GetComponent<TextMeshProUGUI>().text = "Pret: ";
            }
            else if (appmanagerClass.SelectedLanguage == 2)
            {
                hour.GetComponent<TextMeshProUGUI>().text = "Hour : " + barberOccupiedHours[i] + " : " + barberOccupiedMinutes[i];
                appointmentClientName.text = "Name: " + clientName[i];
                price.GetComponent<TextMeshProUGUI>().text = "Price: ";
            }
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
        for(int i = 1; i < DateTime.DaysInMonth(currentYear, barberMenuCurrentMonthNr) + 1; i++)
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
            currentDate = new DateTime(currentYear, barberMenuCurrentMonthNr, i);
            appointmentDayNameObj.GetComponent<TextMeshProUGUI>().text = currentDate.DayOfWeek.ToString();
            appointmentDayList.Add(day);
            if (barberOccupiedDays[i] == true)
            {
                day.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
            }
            else if(barberOccupiedDays[i] == false)
            {
                day.GetComponent<Image>().color = new Color(0f, 0.65f, 0f, 1f);
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
        if (appmanagerClass.SelectedLanguage == 2)
        {
            selectDateInfoTxt.text = "Select a date for your appointment.";
        }
        else if (appmanagerClass.SelectedLanguage == 1)
        {
            selectDateInfoTxt.text = "Alege o data pentru programare.";
        }
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
        loadingScreen.SetActive(true);
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
        CreateAppointmentHours();
        
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
        barberFirstName = selectedBarberObj.name.Split('\t')[1];
        barberLastName = selectedBarberObj.name.Split('\t')[2];
        GetTimeToCut(); //get the time needed for the barber to do a cut
        appointmentMenu.SetActive(true);
        selectedAppointmentDay = DateTime.Now.Day.ToString();
        CreateCalendar();
        ResetDateTime();
    }
    public void SelectService()
    {
        string selectedService = EventSystem.current.currentSelectedGameObject.name; //store the name of selected service in a string variable
        for (int i = 0; i < serviceObjectsList.Count; i++)
        {
            if(selectedService == serviceObjectsList[i].name) //check if the selected service is in the serviceObjects list;
            {
                if (serviceObjectsList[i].GetComponent<Image>().color == serviceButtonNormalColor) //check if the button is selected already or not
                {
                    serviceObjectsList[i].GetComponent<Image>().color = serviceButtonSelectedColor; //set the colour of the button
                    appointmentSelectedServicesList.Add(serviceObjectsList[i].name); //add the selected service to a list from where we can send it to the database.
                    totalPrice += int.Parse(servicePriceList[i]);
                    Debug.Log("Pret =  " + totalPrice);
                }
                else
                {
                    serviceObjectsList[i].GetComponent<Image>().color = serviceButtonNormalColor; //set the colour of the button
                    totalPrice -= int.Parse(servicePriceList[i]);
                    Debug.Log("Pret =  " + totalPrice);
                    for (int j = 0; j < appointmentSelectedServicesList.Count; j++) 
                    {
                        if(appointmentSelectedServicesList[j] == selectedService) //check if the selected service is already in the selectedServices list
                        {
                            appointmentSelectedServicesList.RemoveAt(j); //remove service from the list
                        }
                    }
                }
            }
        }
        Debug.Log(appointmentSelectedServicesList.Count + " = numaru de servicii");
        if(appointmentSelectedServicesList.Count > 0) //if no service selected -> createAppointment button shall not be visible
        {
            createAppointmentBTN.SetActive(true);
        }
        else
        {
            createAppointmentBTN.SetActive(false);
        }
    }
    public void GetShopServices()
    {
        StartCoroutine(GetShopServicesEnum());
    }
    IEnumerator GetShopServicesEnum()
    {
        int whattopick = 1;
        loadingScreen.SetActive(true);
        for(int i = 0; i < 4; i++)
        {
            List<IMultipartFormSection> newform = new List<IMultipartFormSection>();
            newform.Add(new MultipartFormDataSection("selectedShop", appmanagerClass.GetSelectedShopName()));
            newform.Add(new MultipartFormDataSection("whattopick", whattopick.ToString()));
            UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/getshopservices.php", newform);
            yield return webreq.SendWebRequest();
            if (webreq.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webreq.error);
            }
            else
            {
                switch (whattopick)
                {
                    case 1:
                        serviceNameListRO = webreq.downloadHandler.text.Split('\t');
                        whattopick = 2;
                        webreq.Dispose();
                        break;
                    case 2:
                        servicePriceList = webreq.downloadHandler.text.Split('\t');
                        whattopick = 3;
                        webreq.Dispose();
                        break;
                    case 3:
                        serviceNameListEN = webreq.downloadHandler.text.Split('\t');
                        whattopick = 4;
                        webreq.Dispose();
                        break;
                    case 4:
                        serviceCoinNameList = webreq.downloadHandler.text.Split('\t');
                        whattopick = 1;
                        webreq.Dispose();
                        break;
                }
            }
        }
        CreateServices();
    }
    public void CreateServices()
    {
        if (serviceObjectsList.Count > 0) //if the list is not empty
        {
            foreach (GameObject obj in serviceObjectsList)
            {
                Destroy(obj); //destroy the object
            }
            serviceObjectsList.Clear(); //clear the list
        }
        for (int i = 0; i < servicePriceList.Length - 1; i++)
        {
            if (appmanagerClass.SelectedLanguage == 1)
            {
                if (serviceNameListRO[i].Length < 1)
                {
                    GameObject service = Instantiate(servicePrefab, servicePrefab.transform.position, servicePrefab.transform.rotation, servicePrefab.transform.parent);
                    errorInfoTXT.text = "Unul sau mai multe servicii nu au numele in romana configurate. Am tradus numele lor in urmatoarea limba gasita.";
                    errorInfoObj.SetActive(true);
                    service.name = serviceNameListEN[i];
                    service.transform.Find("ServiceName").gameObject.GetComponent<TextMeshProUGUI>().text = serviceNameListEN[i];
                    service.transform.SetParent(servicePrefab.transform.parent); //set the parent to be the same as the prefab's
                    if (appmanagerClass.SelectedLanguage == 1)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Pret: ";
                    }
                    else if (appmanagerClass.SelectedLanguage == 2)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Price: ";
                    }
                    service.transform.Find("PriceTXT").gameObject.transform.Find("ServicePrice").gameObject.GetComponent<TextMeshProUGUI>().text = servicePriceList[i] + " " + serviceCoinNameList[i];
                    //set z position to 0 so it can be seen
                    service.transform.localPosition = new Vector3(servicePrefab.transform.position.x, servicePrefab.transform.position.y, 0f);
                    service.transform.localScale = new Vector3(1f, 1f, 1f);
                    service.SetActive(true); //set the object to be visible
                    serviceObjectsList.Add(service); //add the object to the list
                }
                else
                {
                    GameObject service = Instantiate(servicePrefab, servicePrefab.transform.position, servicePrefab.transform.rotation, servicePrefab.transform.parent);
                    service.name = serviceNameListRO[i];
                    service.transform.Find("ServiceName").gameObject.GetComponent<TextMeshProUGUI>().text = serviceNameListRO[i];
                    service.transform.SetParent(servicePrefab.transform.parent); //set the parent to be the same as the prefab's
                    if (appmanagerClass.SelectedLanguage == 1)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Pret: ";
                    }
                    else if (appmanagerClass.SelectedLanguage == 2)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Price: ";
                    }
                    service.transform.Find("PriceTXT").gameObject.transform.Find("ServicePrice").gameObject.GetComponent<TextMeshProUGUI>().text = servicePriceList[i] + " " + serviceCoinNameList[i];
                    //set z position to 0 so it can be seen
                    service.transform.localPosition = new Vector3(servicePrefab.transform.position.x, servicePrefab.transform.position.y, 0f);
                    service.transform.localScale = new Vector3(1f, 1f, 1f);
                    service.SetActive(true); //set the object to be visible
                    serviceObjectsList.Add(service); //add the object to the list
                }
            }
            else if (appmanagerClass.SelectedLanguage == 2)
            {
                if (serviceNameListEN[i].Length < 1)
                {
                    GameObject service = Instantiate(servicePrefab, servicePrefab.transform.position, servicePrefab.transform.rotation, servicePrefab.transform.parent);
                    errorInfoTXT.text = "Some services have no english name configured. We've translated their names in the next found language.";
                    errorInfoObj.SetActive(true);
                    service.name = serviceNameListRO[i];
                    service.transform.Find("ServiceName").gameObject.GetComponent<TextMeshProUGUI>().text = serviceNameListRO[i];
                    service.transform.SetParent(servicePrefab.transform.parent); //set the parent to be the same as the prefab's
                    if (appmanagerClass.SelectedLanguage == 1)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Pret: ";
                    }
                    else if (appmanagerClass.SelectedLanguage == 2)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Price: ";
                    }
                    service.transform.Find("PriceTXT").gameObject.transform.Find("ServicePrice").gameObject.GetComponent<TextMeshProUGUI>().text = servicePriceList[i] + " " + serviceCoinNameList[i];
                    //set z position to 0 so it can be seen
                    service.transform.localPosition = new Vector3(servicePrefab.transform.position.x, servicePrefab.transform.position.y, 0f);
                    service.transform.localScale = new Vector3(1f, 1f, 1f);
                    service.SetActive(true); //set the object to be visible
                    serviceObjectsList.Add(service); //add the object to the list
                }
                else
                {
                    GameObject service = Instantiate(servicePrefab, servicePrefab.transform.position, servicePrefab.transform.rotation, servicePrefab.transform.parent);
                    service.name = serviceNameListEN[i];
                    service.transform.Find("ServiceName").gameObject.GetComponent<TextMeshProUGUI>().text = serviceNameListEN[i];
                    service.transform.SetParent(servicePrefab.transform.parent); //set the parent to be the same as the prefab's
                    if (appmanagerClass.SelectedLanguage == 1)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Pret: ";
                    }
                    else if (appmanagerClass.SelectedLanguage == 2)
                    {
                        service.transform.Find("PriceTXT").gameObject.GetComponent<TextMeshProUGUI>().text = "Price: ";
                    }
                    service.transform.Find("PriceTXT").gameObject.transform.Find("ServicePrice").gameObject.GetComponent<TextMeshProUGUI>().text = servicePriceList[i] + " " + serviceCoinNameList[i];
                    //set z position to 0 so it can be seen
                    service.transform.localPosition = new Vector3(servicePrefab.transform.position.x, servicePrefab.transform.position.y, 0f);
                    service.transform.localScale = new Vector3(1f, 1f, 1f);
                    service.SetActive(true); //set the object to be visible
                    serviceObjectsList.Add(service); //add the object to the list
                }
            }
        }
        loadingScreen.SetActive(false);
    }
    public void SelectHour()
    {
        occupiedMinutes = new bool[60];
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
        nextToServicesBTN.SetActive(true);
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
        WWWForm form = new WWWForm();
        form.AddField("barberName", barberFirstName + barberLastName);
        form.AddField("selectedDay", selectedDayObj.name);
        form.AddField("selectedMonth", currentMonth);
        form.AddField("selectedYear", currentYear);
        form.AddField("selectedHour", selectedHour);
        form.AddField("whatToPick", WhatToPick);
        form.AddField("firstname", barberFirstName);
        form.AddField("lastname", barberLastName);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/checkforappointment.php", form);
        yield return www;
        string[] minutes = new string[60];
        for (int i = 0; i < www.text.Split('\t').Length; i++)
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

        www.Dispose();
        CreateMinutes();
    }
    public void BarberCreateAppointment()
    {
        StartCoroutine(BarberCreateAppointmentEnum());
    }
    IEnumerator BarberCreateAppointmentEnum()
    {
        UnityWebRequest web = new UnityWebRequest("http://mybarber.vlcapps.com/appscripts/barbercreateappointment.php");
        //logic;
        yield return web;
    }

    public void BarberCheckCalendar()
    {
        StartCoroutine(BarberCheckCalendarEnum());
    }
    IEnumerator BarberCheckCalendarEnum()
    {
        loadingScreen.SetActive(true);
        for (int i = 1; i < DateTime.DaysInMonth(currentYear, barberMenuCurrentMonthNr+1); i++) // do the php script for each day in the current selected month
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>(); //create a form to pass to the php script and add the needed variables
            form.Add(new MultipartFormDataSection("barberName", accountClass.GetProfileFirstName + accountClass.GetProfileLastName));
            form.Add(new MultipartFormDataSection("selectedDay", i.ToString()));
            form.Add(new MultipartFormDataSection("selectedMonth", barberMenuCurrentMonthNr.ToString()));
            form.Add(new MultipartFormDataSection("selectedYear", currentYear.ToString()));
            UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/barbercheckcalendar.php", form);
            yield return web.SendWebRequest(); //execute the script
            appointmentsInDay[i] = int.Parse(web.downloadHandler.text); //set the int list array of each day to be the number of appointments that the barber has in that specific day
            web.Dispose();
        }

        BarberCreateAppointmentCalendar(); //create calendar with data collected
    }
    public void BarberCreateAppointmentCalendar()
    {
        DateTime date = new DateTime(currentYear, barberMenuCurrentMonthNr, 1);
        MonthAndYear.text = date.ToString("MMM yyyy");
        if (calendarDaysList.Count > 0) //check the list if there are objects already created and deletes them
        {
            foreach (GameObject obj in calendarDaysList)
            {
                Destroy(obj);
            }
            calendarDaysList.Clear();
        }
        int dayAfterMonthEnd = 0; //how many days objects should appear.
        int nextMonthDay = 1; // number to set the next month's day's name 
        switch(DateTime.DaysInMonth(currentYear, barberMenuCurrentMonthNr)) //set how many days objects can be created to fill the calendar
        {
            case 28:
                dayAfterMonthEnd = 7;
                break;
            case 29:
                dayAfterMonthEnd = 6;
                break;
            case 30:
                dayAfterMonthEnd = 5;
                break;
            case 31:
                dayAfterMonthEnd = 4;
                break;
            case 32:
                dayAfterMonthEnd = 3;
                break;
        }
        for (int i = 1; i < DateTime.DaysInMonth(currentYear, barberMenuCurrentMonthNr) + dayAfterMonthEnd + 1; i++)
        {
            GameObject day = Instantiate(calendarDayPrefab); //create the object
            day.transform.SetParent(calendarDayPrefab.transform.parent); //set the parent to be the parent of the prefab
            day.SetActive(true); //set the obvject to be visible
            day.transform.localPosition = new Vector3(day.transform.position.x, day.transform.position.y, 0f); //set the z axis to 0 so the object can be seen
            day.transform.localScale = new Vector3(1, 1, 1); // set the scale to 1 
            GameObject dayNumber = day.transform.Find("DayInside").gameObject.transform.Find("DayNumber").gameObject; //set the object that holds the day number text component
            GameObject dayAppointments = day.transform.Find("DayInside").gameObject.transform.Find("AppointmentsNumber").gameObject; //set the object that holds the appointments number text component
            if (i > DateTime.DaysInMonth(currentYear, barberMenuCurrentMonthNr) + 1)
            {
                day.name = nextMonthDay + "\t" + (barberMenuCurrentMonthNr + 1); //set the name of the object to be [dayNumber + monthNumber]
                dayNumber.GetComponent<TextMeshProUGUI>().text = nextMonthDay.ToString(); //set the text number of the day
                day.GetComponent<CanvasGroup>().alpha = 1 - (float)nextMonthDay / 10; //set the visibility for every day from the next month
                dayAppointments.GetComponent<TextMeshProUGUI>().text = "........."; //set the appointments from that day
                nextMonthDay++;
            }
            else
            {
                day.name = i + "\t" + barberMenuCurrentMonthNr; //set the name of the object to be [dayNumber + monthNumber]
                dayNumber.GetComponent<TextMeshProUGUI>().text = i.ToString(); //set the text number of the day
                //set the number of appointments for each day of this month.
                switch (appmanagerClass.SelectedLanguage)
                {
                    case 1:
                        dayAppointments.GetComponent<TextMeshProUGUI>().text = appointmentsInDay[i] + " programari"; //set the appointments from that day
                        break;
                    case 2:
                        dayAppointments.GetComponent<TextMeshProUGUI>().text = appointmentsInDay[i] + " appointments"; //set the appointments from that day
                        break;
                }
            }
            calendarDaysList.Add(day); //add the object to the list so it can be deleted if month changed.
        }
        loadingScreen.SetActive(false); //loading screen off
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
            if(!int.TryParse(www.text.Split('\t')[1], out timeToCut))
            {
                if(appmanagerClass.SelectedLanguage == 1)
                {
                    errorInfoTXT.text = "Acest frizer nu si-a configurat profilul inca.";
                    errorInfoObj.SetActive(true);
                }
                else if(appmanagerClass.SelectedLanguage == 2)
                {
                    errorInfoTXT.text = "This barber has not configured his profile yet.";
                    errorInfoObj.SetActive(true);
                }
            }
            timeToCut = int.Parse(www.text.Split('\t')[1]);
        }
        else
        {
            Debug.Log(www.text);
        }
        www.Dispose();
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
            if(appmanagerClass.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Timpul a fost actualizat cu succes!";
                errorInfoObj.SetActive(true);
            }
            else if(appmanagerClass.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "The time has been succesfully updated.";
                errorInfoObj.SetActive(true);
            }
        }
        else
        {
            Debug.Log(www.text);
        }
        www.Dispose();
    }
    public void CreateAppointmentHours()
    {
        StartCoroutine(CreateAppointmentHoursEnum());
    }
    IEnumerator CreateAppointmentHoursEnum()
    {
        if(appmanagerClass.SelectedLanguage == 1)
        {
            selectDateInfoTxt.text = "Alege o ora pentru programare.";
        }
        else if(appmanagerClass.SelectedLanguage == 2)
        {
            selectDateInfoTxt.text = "Select an hour for your appointment.";
        }
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
            www.Dispose();
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
        nextToServicesBTN.SetActive(true);

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
                if(occupiedMinutes[i] == true) 
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
    public void SendNotificationToBarber(string barberFirstname, string barberLastname)
    {
        StartCoroutine(SendNotificationToBarberEnum(barberFirstname, barberLastname));
    }
    IEnumerator SendNotificationToBarberEnum(string barberFirstName, string barberLastName)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("firstName", barberFirstName));
        form.Add(new MultipartFormDataSection("lastName", barberLastName));
        if(appmanagerClass.SelectedLanguage == 1)
        {
            form.Add(new MultipartFormDataSection("notificationTitle", barberNotificationTitleRO));
            form.Add(new MultipartFormDataSection("notificationBody", barberNotificationBodyRO));
        }
        else if(appmanagerClass.SelectedLanguage == 2)
        {
            form.Add(new MultipartFormDataSection("notificationTitle", barberNotificationTitleEN));
            form.Add(new MultipartFormDataSection("notificationBody", barberNotificationBodyEN));
        }
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/sendnotificationtobarber.php", form);
        yield return web.SendWebRequest();
        if (web.downloadHandler.text == "0")
        {
            Debug.Log("notificare la frizer trimisa");
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
        }
        web.Dispose();
    }
    public void CreateAppointment()
    {
        StartCoroutine(CreateAppointmentEnumerator());
    }
    IEnumerator CreateAppointmentEnumerator()
    {
        string www0index;
        WWWForm form = new WWWForm();
        for(int i = 0; i < appointmentSelectedServicesList.Count; i++)
        {
            appointmentSelectedServices += appointmentSelectedServicesList[i] + "\t";
        }
        form.AddField("barberName", barberFirstName + barberLastName);
        form.AddField("clientName", accountClass.AccountUsername);
        form.AddField("day", selectedDayObj.name);
        form.AddField("month", currentMonth);
        form.AddField("year", currentYear);
        form.AddField("hour", selectedHour);
        form.AddField("minute", selectedMinute);
        form.AddField("services", appointmentSelectedServices);
        form.AddField("totalprice", totalPrice);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/createappointment.php", form);
        yield return www;
        www0index = www.text[0].ToString();
        BackToHours();
        BackToCalendar();
        appointmentSelectedServicesList.Clear();
        appointmentSelectedServices = "";
        www.Dispose();
        if (www0index == "0")
        {
            if (appmanagerClass.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Appointment successfully created !";
                errorInfoObj.SetActive(true);
                notificationsClass.SendNotification(currentYear, currentMonth, int.Parse(selectedDayObj.name), selectedHour, selectedMinute, 1, barberFirstName, barberLastName);
                Debug.Log("appointment luna: " + currentMonth + " ziua: " + selectedDayObj.name + " ora: " + selectedHour + "minute: " + selectedMinute);
                SendNotificationToBarber(barberFirstName, barberLastName);
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Programare creata cu succes !";
                errorInfoObj.SetActive(true);
                notificationsClass.SendNotification(currentYear, currentMonth, int.Parse(selectedDayObj.name), selectedHour, selectedMinute, 1, barberFirstName, barberLastName);
                Debug.Log("notificare trimisea");
                SendNotificationToBarber(barberFirstName, barberLastName);
            }
        }
        else if (www0index == "3")
        {
            if (appmanagerClass.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "There is already an appointment for created on this date and hour. Try selecting another hour/minute.";
                errorInfoObj.SetActive(true);
            }
            else if (appmanagerClass.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Deja exista o programare creata pentru aceasta zi si ora. Incearca sa selectezi alta ora.";
                errorInfoObj.SetActive(true);
            }
            errorInfoObj.SetActive(true);
        }
        else
        {
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
    public void ClearErrorInfo()
    {
        errorInfoTXT.text = "";
        errorInfoObj.SetActive(false);
    }
}

