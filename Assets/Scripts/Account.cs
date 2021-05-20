using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Account : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField; //login
    [SerializeField]
    private TMP_InputField passwordField; //login
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerFirstName;
    [SerializeField] private TMP_InputField registerLastName;
    [SerializeField] private GameObject firstFields;
    [SerializeField] private GameObject secondFields;
    [SerializeField] private GameObject registerButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject registerError;
    [SerializeField] private TextMeshProUGUI profileFirstName;
    [SerializeField] private TextMeshProUGUI profileLastName;
    [SerializeField] private TextMeshProUGUI profileUsername;
    private string accountUsername;
    private bool isAdmin;
    private string firstName;
    private string lastName;
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
    public void nextFields()
    {
        if(registerEmail.text.Length > 3 && registerUsername.text.Length > 7 && registerPassword.text.Length > 7)
        {
            firstFields.SetActive(false);
            secondFields.SetActive(true);
            nextButton.SetActive(false);
            registerButton.SetActive(true);
        }
        else
        {
            registerError.SetActive(true);
            firstFields.SetActive(false);
            secondFields.SetActive(false);
            nextButton.SetActive(false);
        }
    }
    public void resetRegister()
    {
        firstFields.SetActive(true);
        secondFields.SetActive(false);
        registerButton.SetActive(false);
        nextButton.SetActive(true);
        registerError.SetActive(false);
    }
    public void RegisterAccount()
    {
        StartCoroutine(RegisterAccountEnum());
    }
    IEnumerator RegisterAccountEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", registerUsername.text);
        form.AddField("password", registerPassword.text);
        form.AddField("email", registerEmail.text);
        form.AddField("firstName", registerFirstName.text);
        form.AddField("lastName", registerLastName.text);
        WWW www = new WWW("http://localhost/barberapp/register.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            registerUsername.text = "";
            registerPassword.text = "";
            registerEmail.text = "";
        }
        appmanager.backButton();
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
            isLogged = true;
            bool.TryParse(www.text.Split('\t')[2], out isBoss);
            if(accountUsername == "test")
            {
                isAdmin = true;
            }
            int.TryParse(www.text.Split('\t')[3], out fiveStarReviews);
            int.TryParse(www.text.Split('\t')[4], out fourStarReviews);
            int.TryParse(www.text.Split('\t')[5], out threeStarReviews);
            int.TryParse(www.text.Split('\t')[6], out twoStarReviews);
            int.TryParse(www.text.Split('\t')[7], out oneStarReviews);
            shopNameOfUser = www.text.Split('\t')[8];
            firstName = www.text.Split('\t')[9];
            lastName = www.text.Split('\t')[10];
            appmanager.backButton();
            profileUsername.text = accountUsername;
            profileFirstName.text = firstName;
            profileLastName.text = lastName;
        }
        else
        {
            Debug.Log(www.text);
        }
    }
}
