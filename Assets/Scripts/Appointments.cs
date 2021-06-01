using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Appointments : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentDateTXT; //current date textbox
    [SerializeField] private GameObject dayPrefab; //the object from which i instantiate all days in the calendar.
    [SerializeField] private TextMeshProUGUI dayNumber; //text of the daynumber textbox;
    [SerializeField] private TextMeshProUGUI dayName; //text of the dayname textbox;
    [SerializeField] private Account accountClass;
    [SerializeField] private Button createAppointmentBTN;
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
    private string[] appointmentInfo = new string[130]; //info given by the client when the appointment was created
    private List<GameObject> occupiedAppointments = new List<GameObject>();
    [SerializeField] private GameObject occupiedAppointmentPrefab;
    private TextMeshProUGUI appointmentClientName;
    private TextMeshProUGUI occupiedAppointmentHour;
    private TextMeshProUGUI occupiedAppointmentMinute;
    private TextMeshProUGUI occupiedAppointmentInfo;
    private int occupiedAppointmentCounter = 0; //increases for every appointment that has to be shown
    //------end-------
    //DELETE THE APPOINTMENT ~~BARBER~~
    private GameObject selectedAppointment;
    private string deleteAppointmentHour;
    private string deleteAppointmentMinute;

    //~~~ END ~~~

    public void CheckBarberAppointmentsDays()
    {
        StartCoroutine(CheckBarberAppointmentsEnum());
    }
    IEnumerator CheckBarberAppointmentsEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("barberName", accountClass.BarberName);
        form.AddField("currentMonth", currentMonth);
        form.AddField("currentYear", currentYear);
        WWW www = new WWW("http://localhost/barberapp/checkbarberappdays.php", form);
        yield return www;
        string[] days = new string[33];
        Debug.Log(www.text);
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
        for (int i = 0; i < 4; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("barberName", accountClass.BarberName);
            form.AddField("currentMonth", currentMonth);
            form.AddField("currentYear", currentYear);
            form.AddField("selectedDay", selectedAppointmentDay);
            form.AddField("wtp", WTP);
            WWW www = new WWW("http://localhost/barberapp/getoccupiedtime.php", form);
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
                    Debug.Log("textlkength = " + www.text.Split('\t').Length);
                    Debug.Log(www.text);
                    for (int j = 0; j < www.text.Split('\t').Length; j++)
                    {
                        barberOccupiedMinutes[j] = www.text.Split('\t')[j];
                        Debug.Log("occupiedMNS = " + barberOccupiedMinutes[j]);
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
        form.AddField("clientName", selectedAppointment.name);
        form.AddField("appHour", hour);
        form.AddField("appMinute", minute);
        WWW www = new WWW("http://localhost/barberapp/barberdeleteapp.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("deleted succesfully");
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
            appointment.name = clientName[i];
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
            occupiedAppointments.Add(appointment);
        }
        occupiedAppointmentCounter = 0;
        loadingScreen.SetActive(false);
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
        selectedDayString = date.DayOfWeek.ToString();
        UpdateCurrentDate();
        createAppointmentBTN.interactable = true;
    }
    public void UpdateCurrentDate()
    {
        currentDateString = selectedDayString + " / " + currentDate.Month + " / " + currentYear;
        currentDateTXT.text = currentDateString;
    }
    public void NextMonth()
    {
        currentMonth++;
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

    }
    public void SelectHour()
    {
        selectedHour = int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name);
        CheckForAvailableAppointment();
        CreateMinutes();
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
            WWW www = new WWW("http://localhost/barberapp/checkforappointment.php", form);
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
        WWW www = new WWW("http://localhost/barberapp/getttc.php", form);
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
        WWW www = new WWW("http://localhost/barberapp/updatetimetocut.php", form);
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
        WWWForm form = new WWWForm();
        form.AddField("shopname", appmanagerClass.GetSelectedShopName());
        form.AddField("selectedDay", selectedDayString);
        WWW www = new WWW("http://localhost/barberapp/getshopprogramapp.php", form);
        yield return www;

        if(www.text[0] == '0')
        {
            loadingScreen.SetActive(true);
            Debug.Log("success");
            string workingHours = www.text.Split('\t')[1];
            string openHM = workingHours.Split('-')[0];
            string closeHM = workingHours.Split('-')[1];
            openHour = int.Parse(openHM.Split(':')[0]);
            openMinute = int.Parse(openHM.Split(':')[1]);
            closeHour = int.Parse(closeHM.Split(':')[0]);
            closeMinute = int.Parse(closeHM.Split(':')[1]);
            newOpenMinutes = openMinute;
            CreateHours();
            loadingScreen.SetActive(false);
        }
        else
        {
            Debug.Log(www.text);
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
        WWWForm form = new WWWForm();
        form.AddField("barberName", barberFirstName + barberLastName);
        form.AddField("clientName", accountClass.AccountUsername);
        form.AddField("day", selectedDayObj.name);
        form.AddField("month", currentMonth);
        form.AddField("year", currentYear);
        form.AddField("hour", selectedHour);
        form.AddField("minute", selectedMinute);
        WWW www = new WWW("http://localhost/barberapp/createappointment.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log(":sucscessss");
            createAppointmentBTN.interactable = false;
        }
        else
        {
            Debug.Log(www.text);
        }
    }
}

