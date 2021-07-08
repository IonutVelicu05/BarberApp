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
    [SerializeField] private GameObject errorInfoObj;
    [SerializeField] private TextMeshProUGUI errorInfoTXT;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject loadingScreen;
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
    private bool isEmployed;
    private int personalCode;
    private int timeToCut;
    private int shopsCreated; //number of shops the user has already created
    private int shopsToCreate; //number of shops the user can still create
    private string worksAt;
    [SerializeField] private AppManager appmanager;

    public string WorksAt
    {
        get { return worksAt; }
    }
    public string BarberName
    {
        get { return firstName + lastName; }
    }
    public int PersonalCode
    {
        get { return personalCode; }
    }
    public bool CanCreateShops
    {
        get { return shopsToCreate - shopsCreated > 0; }
    }
    public int ShopsCreated
    {
        get { return shopsCreated; }
    }
    public int ShopsToCreate
    {
        get { return shopsToCreate; }
    }
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
    public bool IsEmployed
    {
        get { return isEmployed; }
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
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/register.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            registerUsername.text = "";
            registerPassword.text = "";
            registerEmail.text = "";
            errorInfoTXT.text = "Account created successfully ! You can now login.";
            errorInfoObj.SetActive(true);
        }
        else if(www.text[0] == '1')
        {
            errorInfoTXT.text = "There is a problem with the server's connection. Please try again. If the problem continues contact an administrator.";
            errorInfoObj.SetActive(true);
        }
        else if(www.text[0] == '2')
        {
            errorInfoTXT.text = "There is already an account created with that name. Try another one.";
            errorInfoObj.SetActive(true);
        }
        else
        {
            errorInfoTXT.text = "Something went wrong. Please try again later. If the problem continues contact an administrator.";
            errorInfoObj.SetActive(true);
        }
        appmanager.afterLogin();
    }
    public void LoginAccount()
    {
        StartCoroutine(LoginEnumerator());
    }
    IEnumerator LoginEnumerator()
    {
        loadingScreen.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/login.php", form);

        yield return www;
        if (www.text[0] == '0')
        {
            errorInfoTXT.text = "Login succesful !";
            errorInfoObj.SetActive(true);
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
            if(www.text.Split('\t')[11] != null && www.text.Split('\t')[11] != "")
            {
                isEmployed = true;
                worksAt = www.text.Split('\t')[10];
            }
            timeToCut = int.Parse(www.text.Split('\t')[12]);
            personalCode = int.Parse(www.text.Split('\t')[13]);
            shopsCreated = int.Parse(www.text.Split('\t')[14]);
            shopsToCreate = int.Parse(www.text.Split('\t')[15]);
            Debug.Log("shopsCreated = " + shopsCreated + " shopstocr = " + shopsToCreate);
            appmanager.afterLogin();
            profileUsername.text = accountUsername;
            profileFirstName.text = firstName;
            profileLastName.text = lastName;
            loadingScreen.SetActive(false);
        }
        else if(www.text[0] == '1')
        {
            errorInfoTXT.text = "Login succesful !";
            errorInfoObj.SetActive(true);
            accountUsername = www.text.Split('\t')[1];
            isLogged = true;
            bool.TryParse(www.text.Split('\t')[2], out isBoss);
            if (accountUsername == "test")
            {
                isAdmin = true;
            }
            int.TryParse(www.text.Split('\t')[3], out fiveStarReviews);
            int.TryParse(www.text.Split('\t')[4], out fourStarReviews);
            int.TryParse(www.text.Split('\t')[5], out threeStarReviews);
            int.TryParse(www.text.Split('\t')[6], out twoStarReviews);
            int.TryParse(www.text.Split('\t')[7], out oneStarReviews);
            firstName = www.text.Split('\t')[8];
            lastName = www.text.Split('\t')[9];
            if (www.text.Split('\t')[10] != null && www.text.Split('\t')[10] != "")
            {
                isEmployed = true;
                worksAt = www.text.Split('\t')[10];
            }
            timeToCut = int.Parse(www.text.Split('\t')[11]);
            personalCode = int.Parse(www.text.Split('\t')[12]);
            appmanager.afterLogin();
            profileUsername.text = accountUsername;
            profileFirstName.text = firstName;
            profileLastName.text = lastName;
            loadingScreen.SetActive(false);
        }
        else if(www.text[0] == '2')
        {
            
            errorInfoTXT.text = "Username or password incorrect !";
            errorInfoObj.SetActive(true);
            loadingScreen.SetActive(false);
        }
        else
        {
            errorInfoTXT.text = "Something went wrong! Please try again later. If the problem continues contact an administrator.";
            errorInfoObj.SetActive(true);
            loadingScreen.SetActive(false);
        }
    }
}
