﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Account : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField passwordField;
    private string accountUsername;
    private bool isAdmin;
    private int fiveStarReviews;
    private int fourStarReviews;
    private int threeStarReviews;
    private int twoStarReviews;
    private int oneStarReviews;
    private bool isBoss;
    [SerializeField] private AppManager appmanager;

    public string AccountUsername
    {
        get { return accountUsername; }
    }
    public bool IsBoss
    {
        get { return isBoss; }
    }
    public bool IsAdmin
    {
        get { return isAdmin; }
    }
    public void LoginAccount()
    {
        StartCoroutine(LoginEnumerator());
    }
    IEnumerator LoginEnumerator()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/barberapp/login.php", form);

        yield return www;
        if (www.text[0] == '0')
        {
            Debug.Log("login suces");
            accountUsername = www.text.Split('\t')[1];
            bool.TryParse(www.text.Split('\t')[2], out isBoss);
            if(accountUsername == "test")
            {
                isAdmin = true;
            }
            appmanager.IsLoggedIn = true;
            appmanager.backButton();
            //gameObject.GetComponent<AppManager>().backButton();
        }
        else
        {
            Debug.Log(www.text);
        }
    }
}
