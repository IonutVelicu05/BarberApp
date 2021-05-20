using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarberShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopName; // name of the shop 
    [SerializeField] private TextMeshProUGUI shopAddress; //address of the shop
    [SerializeField] private TextMeshProUGUI currentDateTXT; //current date textbox
    [SerializeField] private GameObject dayPrefab; //the object from which i instantiate all days in the calendar.
    [SerializeField] private TextMeshProUGUI dayNumber; //text of the daynumber textbox;
    [SerializeField] private TextMeshProUGUI dayName; //text of the dayname textbox;
    private int WhatToPick; // var to check what to pick from sql
    public string ShopName
    {
        set { shopName.text = value; }
        get { return shopName.text; }
    }
    public string ShopAddress
    {
        set { shopAddress.text = value; }
    }

    //calendar
    private string currentDateString; // the date that is selected at the moment
    private string selectedDayString = "~~"; //string of the name shown in the CurrentDate textbox
    private int currentYear = DateTime.Now.Year; //the current year
    private int currentMonth = DateTime.Now.Month; //the current month

    private List<GameObject> daysObjects = new List<GameObject>(); //list of days objects created
    private GameObject selectedDayObj; //object representing the day object that was pressed on
    private Image selectedDayImg; //image of the selected day
    private Image lastDaySelectedImg; //image of the last selected day
    private GameObject dayNumberObj; //object representing the text object that holds the number of the day
    private GameObject dayNameObj; //object representing the text object that holds the name of the day
    private DateTime currentDate;
    


    private void Start()
    {
        testing();
        UpdateCurrentDate(); //update the current date selected textbox
    }
    public void testing()
    {
        //if the list is not empty destroy every object in that list
        if (daysObjects.Count > 0)
        {
            foreach(GameObject dayobj in daysObjects)
            {
                Destroy(dayobj.gameObject);
            }
            daysObjects.Clear();
        }
        //loop to create the objects for each day in the selected month
        for (int i = 1; i < DateTime.DaysInMonth(currentYear, currentMonth)+1; i++)
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
    }
    public void SelectDay()
    {
        //if the lastday image is not null (or no object has been selected yet)
        if(lastDaySelectedImg != null)
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
    }
    public void UpdateCurrentDate()
    {
        currentDateString = selectedDayString + " / " + currentDate.Month + " / " + currentYear;
        currentDateTXT.text = currentDateString;
    }
    public void NextMonth()
    {
        currentMonth++;
        if(currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }
        testing();
        UpdateCurrentDate();
    }
    public void PreviousMonth()
    {
        currentMonth--;
        if(currentMonth < 1)
        {
            currentMonth = 12;
            currentYear--;
        }
        testing();
        UpdateCurrentDate();
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
