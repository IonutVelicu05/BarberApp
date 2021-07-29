using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class Notifications : MonoBehaviour
{
    [SerializeField] private Appointments appointmentsClass;
    private AndroidNotification reminderNotification;
    private AndroidNotification reviewNotification;
    private DateTime dateOfNotification = new DateTime();
    int notificationMinute;
    int minutesBeforeAppointment = 2; //how many minutes before the appointment the notification is shown
    int minutesAfterAppointment; //after how many minutes the user get review notification
    // Start is called before the first frame update
    void Start()
    {
        minutesAfterAppointment = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 60f));
        //Create a channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "reminder_ch",
            Name = "Reminder channel",
            Importance = Importance.High,
            Description = "Reminder notifications",
        };
        var reviewChannel = new AndroidNotificationChannel()
        {
            Id = "review_ch",
            Name = "Review channel",
            Importance = Importance.High,
            Description = "Review notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        AndroidNotificationCenter.RegisterNotificationChannel(reviewChannel);
    }

    public void SendNotification(int year, int month, int day, int hour, int minute, int clientOrBarber)
    {
        //1 = client ;; 2 = barber
        if (clientOrBarber == 1)
        {
            reminderNotification.Title = "Hey! Did you forget?"; //title of the notification 
            reminderNotification.Text = "You have a saloon appointment soon !"; // text of notification, appears under the title
            Debug.Log("asta e minutu : " + minute);
            if(minute == 0)
            {
                notificationMinute = minutesBeforeAppointment;
            }
            dateOfNotification = new DateTime(year, month, day, hour, notificationMinute, 5); //the moment when the notification will show
            reminderNotification.FireTime = dateOfNotification; //set the notification to show at that date and time
            AndroidNotificationCenter.SendNotification(reminderNotification, "reminder_ch"); //send the notification
            reviewNotification.Title = "Hey! Hope you are doing great !";
            reviewNotification.Text = "Can you tell us how was your experience? Come and leave a review !";
            notificationMinute = minutesAfterAppointment;
            if(hour == 24)
            {
                hour = 2;
            }
            dateOfNotification = dateOfNotification = new DateTime(year, month, day, hour, notificationMinute, 5);
            reviewNotification.FireTime = dateOfNotification;
            AndroidNotificationCenter.SendNotification(reviewNotification, "review_ch");
        }
        else if(clientOrBarber == 2)
        {

        }
    }
}
