using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class Notifications : MonoBehaviour
{
    [SerializeField] private Appointments appointmentsClass;
    private AndroidNotification notification;
    private DateTime dateOfNotification = new DateTime();
    // Start is called before the first frame update
    void Start()
    {
        //Create a channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.High,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(int year, int month, int day, int hour, int minute, int clientOrBarber)
    {
        //1 = client ;; 2 = barber
        if(clientOrBarber == 1)
        {
            notification.Title = "Hey! Did you forget?"; //title of the notification 
            notification.Text = "You have a saloon appointment soon !"; // text of notification, appears under the title
            dateOfNotification = new DateTime(year, month, day, hour, minute - 2, 5); //the moment when the notification will show
            notification.FireTime = dateOfNotification; //set the notification to show at that date and time
            AndroidNotificationCenter.SendNotification(notification, "channel_id"); //send the notification
        }
        else if(clientOrBarber == 2)
        {

        }
    }
}
