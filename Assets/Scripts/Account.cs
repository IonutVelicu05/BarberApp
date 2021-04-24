using System.Collections;
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
    private bool isLogged;
    private string shopNameOfUser;
    [SerializeField] private AppManager appmanager;

    public string AccountUsername
    {
        get { return accountUsername; }
    }
    public string ShopNameOfUser
    {
        get { return shopNameOfUser; }
    }
    public bool IsBoss
    {
        get { return isBoss; }
    }
    public bool IsAdmin
    {
        get { return isAdmin; }
    }
    public bool IsLogged
    {
        get { return isLogged; }
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
        WWW www = new WWW("http://81.196.99.232/barberapp/login.php", form);

        yield return www;
        if (www.text[0] == '0')
        {
            Debug.Log("login suces");
            accountUsername = www.text.Split('\t')[1];
            isLogged = true;
            bool.TryParse(www.text.Split('\t')[2], out isBoss);
            if(accountUsername == "test")
            {
                isAdmin = true;
            }
            shopNameOfUser = www.text.Split('\t')[8];
            appmanager.backButton();
        }
        else
        {
            Debug.Log(www.text);
        }
    }
}
