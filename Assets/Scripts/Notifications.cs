using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI title;
    [SerializeField] private Appointments appointmentsClass;
    [SerializeField] private AppManager appmanagerClass;
    [SerializeField] private Account accountClass;
    private AndroidNotification reminderNotification;
    private AndroidNotification reviewNotification;
    private DateTime dateOfNotification = new DateTime();
    private int notificationMinute;
    private int minutesBeforeAppointment = 1; //how many minutes before the appointment the notification is shown
    private int minutesAfterAppointment;
    private int notificationHour;
    private string reviewBarberFirstName;
    private string reviewBarberLastName;
    private int barberRating = 0;
    [SerializeField] private GameObject errorObj;
    [SerializeField] private TMPro.TextMeshProUGUI errorTxt;
    [SerializeField] private TMPro.TextMeshProUGUI barberName;
    [SerializeField] private GameObject reviewMenu;
    [SerializeField] private InputField reviewInputField;
    private string notificationToken;

    public string NotificationToken
    {
        get { return notificationToken; }
    }
    public void SubmitReview()
    {
        StartCoroutine(SubmitReviewEnum());
    }
    IEnumerator SubmitReviewEnum()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("barberFirstName", reviewBarberFirstName));
        form.Add(new MultipartFormDataSection("barberLastName", reviewBarberLastName));
        form.Add(new MultipartFormDataSection("username", accountClass.AccountUsername));
        //form.Add(new MultipartFormDataSection("description", reviewInputField.text));
        form.Add(new MultipartFormDataSection("rating", barberRating.ToString()));
        UnityWebRequest web = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/submitreview.php", form);
        yield return web.SendWebRequest();
        if(web.downloadHandler.text[0] == '0')
        {
            if (appmanagerClass.SelectedLanguage == 1)
            {
                errorTxt.text = "Recenzie trimisa cu succes.";
            }
            else if (appmanagerClass.SelectedLanguage == 2)
            {
                errorTxt.text = "Review sent succesfully.";
            }
            errorObj.SetActive(true);
            reviewMenu.SetActive(false);
        }
        else
        {
            Debug.Log(web.error);
            if (appmanagerClass.SelectedLanguage == 1)
            {
                errorTxt.text = "Ceva nu a functionat, incearca din nou mai tarziu.";
            }
            else if (appmanagerClass.SelectedLanguage == 2)
            {
                errorTxt.text = "Something went wrong, please try again later.";
            }
            errorObj.SetActive(true);
        }
    }
    [SerializeField] TMPro.TextMeshProUGUI notificationTokenTxt;
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
        notificationToken = token.Token;
        notificationTokenTxt.text = notificationToken;
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
        notificationTokenTxt.text = "Mesaj: " + e.Message.Data;
    }
    void Start()
    {
        AndroidNotificationCenter.NotificationReceivedCallback receivedNotificationHandler =
            delegate (AndroidNotificationIntentData lastNotification)
            {
                if (lastNotification.Channel == "review_ch")
                {
                    errorTxt.text = lastNotification.Channel;
                    errorObj.SetActive(true);
                    reviewBarberFirstName = lastNotification.Notification.IntentData.Split('\t')[0];
                    reviewBarberLastName = lastNotification.Notification.IntentData.Split('\t')[1];
                    if (appmanagerClass.SelectedLanguage == 1)
                    {
                        barberName.text = "Nume frizer: " + lastNotification.Notification.IntentData.Split('\t')[0] + " " + lastNotification.Notification.IntentData.Split('\t')[1];
                        reviewMenu.SetActive(true);
                    }
                    else if (appmanagerClass.SelectedLanguage == 2)
                    {
                        barberName.text = "Barber name: " + lastNotification.Notification.IntentData.Split('\t')[0] + " " + lastNotification.Notification.IntentData.Split('\t')[1];
                        reviewMenu.SetActive(true);
                    }
                }
            };
        AndroidNotificationCenter.OnNotificationReceived += receivedNotificationHandler;
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        minutesAfterAppointment = Mathf.RoundToInt(UnityEngine.Random.Range(30f, 80f));
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
    public void SendNotification(int year, int month, int day, int hour, int minute, int clientOrBarber, string barberFirstname, string barberLastname)
    {
        reviewNotification.IntentData = barberFirstname + "\t" + barberLastname;
        notificationMinute = minute;
        notificationHour = hour;
        if (appmanagerClass.SelectedLanguage == 1)
        {
            reminderNotification.Title = "Ai o programare in curand !"; //title of the notification 
            if(minute < 10)
            {
                reminderNotification.Text = "Ai o programare la salon la " + hour + ":0" + minute; // text of notification, appears under the title
            }
            else
            {
                reminderNotification.Text = "Ai o programare la salon la " + hour + ":" + minute; // text of notification, appears under the title
            }
            if(minute < minutesBeforeAppointment) // daca e mai putin decat timpu pus de mn
            {
                notificationMinute = 60 + minute - minutesBeforeAppointment;//se face calcul sa dea notificarea cu o ora inapoi si minute
                notificationHour--; //se da notificarea cu o ora inapoi
            }
            else
            {
                notificationMinute -= minutesBeforeAppointment; //min la care vine notificarea e min ales - min setat de mine
            }
            dateOfNotification = new DateTime(year, month, day, notificationHour, notificationMinute, 5); //the moment when the notification will show
            reminderNotification.FireTime = dateOfNotification; //set the notification to show at that date and time
            AndroidNotificationCenter.SendNotification(reminderNotification, "reminder_ch"); //send the notification
            Debug.Log(reviewNotification.IntentData + "intent");
            notificationMinute = minute;
            notificationHour = hour;
            //REVIEW NOTIFICATION
            reviewNotification.Title = "Hey! Speram ca ti-a placut la salon.";
            reviewNotification.Text = "Cum a fost experienta ta la salon? Lasa un review !";
            notificationMinute += minutesAfterAppointment;
            if(notificationMinute > 60)
            {
                notificationMinute -= 60;
                notificationHour++;
            }
            dateOfNotification = dateOfNotification = new DateTime(year, month, day, notificationHour, notificationMinute, 5);
            reviewNotification.FireTime = dateOfNotification;
            AndroidNotificationCenter.SendNotification(reviewNotification, "review_ch");
        }
        else if(appmanagerClass.SelectedLanguage == 2)
        {
            reminderNotification.Title = "You have an appointment soon !"; //title of the notification 
            if (minute < 10)
            {
                reminderNotification.Text = "You have a salon appointment at " + hour + ":0" + minute; // text of notification, appears under the title
            }
            else
            {
                reminderNotification.Text = "You have a salon appointment at " + hour + ":" + minute; // text of notification, appears under the title
            }
            if (minute < minutesBeforeAppointment) // daca e mai putin decat timpu pus de mn
            {
                notificationMinute = 60 + minute - minutesBeforeAppointment;//se face calcul sa dea notificarea cu o ora inapoi si minute
                notificationHour--; //se da notificarea cu o ora inapoi
            }
            else
            {
                notificationMinute -= minutesBeforeAppointment; //min la care vine notificarea e min ales - min setat de mine
            }
            dateOfNotification = new DateTime(year, month, day, notificationHour, notificationMinute, 5); //the moment when the notification will show
            reminderNotification.FireTime = dateOfNotification; //set the notification to show at that date and time
            AndroidNotificationCenter.SendNotification(reminderNotification, "reminder_ch"); //send the notification
            Debug.Log(reviewNotification.IntentData + "intent");
            notificationMinute = minute;
            notificationHour = hour;
            //REVIEW NOTIFICATION
            reviewNotification.Title = "Hey! Hope you are doing great !";
            reviewNotification.Text = "How was your experience at the salon? Come and leave a review !";
            notificationMinute += minutesAfterAppointment;
            if (notificationMinute > 60)
            {
                notificationMinute -= 60;
                notificationHour++;
            }
            dateOfNotification = dateOfNotification = new DateTime(year, month, day, notificationHour, notificationMinute, 5);
            reviewNotification.FireTime = dateOfNotification;
            AndroidNotificationCenter.SendNotification(reviewNotification, "review_ch");
        }
    }
    public void OneStarRating()
    {
        barberRating = 1;
    }
    public void TwoStarRating()
    {
        barberRating = 2;
    }
    public void ThreeStarRating()
    {
        barberRating = 3;
    }
    public void FourStarRating()
    {
        barberRating = 4;
    }
    public void FiveStarRating()
    {
        barberRating = 5;
    }
}
