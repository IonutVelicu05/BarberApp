using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
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
    [SerializeField] private TextMeshProUGUI profileFirstName;
    [SerializeField] private TextMeshProUGUI profileLastName;
    [SerializeField] private TextMeshProUGUI profileUsername;
    [SerializeField] private TextMeshProUGUI profilePersonalCode;
    [SerializeField] private GameObject errorInfoObj;
    [SerializeField] private TextMeshProUGUI errorInfoTXT;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject loadingScreen;
    private string accountUsername;
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
    private AppManager appmanager;
    private Notifications notificationClass;
    private bool tempAccount = true;
    private string password = "";

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/userprofile.data");
        bf.Serialize(file, accountUsername);
        bf.Serialize(file, tempAccount);
        bf.Serialize(file, password);
        bf.Serialize(file, appmanager.SelectedLanguage);
        file.Close();
    }
    public void Logout()
    {
        isLogged = false;
        if(File.Exists(Application.persistentDataPath + "/userprofile.data"))
        {
            File.Delete(Application.persistentDataPath + "/userprofile.data");
        }
        int randomNumber = Mathf.RoundToInt(Random.Range(000000f, 999999f));
        accountUsername = "user" + randomNumber;
        if(appmanager.SelectedLanguage == 1)
        {
            profileUsername.text = "Nume utilizator: " + accountUsername;
            profileFirstName.text = "Prenume: ";
            profileLastName.text = "Nume: ";
        }
        else if(appmanager.SelectedLanguage == 2)
        {
            profileUsername.text = "Username: " + accountUsername;
            profileFirstName.text = "Firstname: ";
            profileLastName.text = "Lastname: ";
        }
        firstName = "";
        lastName = "";
        fiveStarReviews = 0;
        fourStarReviews = 0;
        threeStarReviews = 0;
        twoStarReviews = 0;
        oneStarReviews = 0;
        isBoss = false;
        isLogged = false;
        shopNameOfUser = "";
        isEmployed = false;
        personalCode = 0;
        timeToCut = 0;
        shopsCreated = 0; 
        shopsToCreate = 0;
        worksAt = "";
        appmanager.afterLogin();
        tempAccount = true;
    }
    public FileStream Load()
    {
        if (File.Exists(Application.persistentDataPath + "/userprofile.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userprofile.data", FileMode.Open);
            accountUsername = bf.Deserialize(file).ToString();
            tempAccount = bool.Parse(bf.Deserialize(file).ToString());
            password = bf.Deserialize(file).ToString();
            appmanager.SelectedLanguage = int.Parse(bf.Deserialize(file).ToString());
            appmanager.updateLanguageTexts();
            usernameField.text = accountUsername;
            passwordField.text = password;
            if (appmanager.SelectedLanguage == 1)
            {
                profileUsername.text = "Nume utilizator: " + accountUsername;
            }
            else if (appmanager.SelectedLanguage == 2)
            {
                profileUsername.text = "Username: " + accountUsername;
            }
            file.Close();
            return file;
        }
        else
        {
            return null;
        }
    }
    private void Start()
    {
        appmanager = gameObject.GetComponent<AppManager>();
        notificationClass = gameObject.GetComponent<Notifications>();
        Load();
        if (tempAccount == false)
        {
            usernameField.text = accountUsername;
            passwordField.text = password;
            LoginAccount();
        }
        else if (tempAccount == true)
        {
            if (accountUsername == null)
            {
                int randomNumber = Mathf.RoundToInt(Random.Range(000000f, 999999f));
                accountUsername = "user" + randomNumber;
                appmanager.SelectedLanguage = 1;
                if (appmanager.SelectedLanguage == 1)
                {
                    profileUsername.text = "Nume utilizator: " + accountUsername;
                }
                else if (appmanager.SelectedLanguage == 2)
                {
                    profileUsername.text = "Username: " + accountUsername;
                }
                Save();
                Debug.Log("second if");
            }
        }
    }
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
        set { shopsCreated = value; }
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
    public bool IsLogged
    {
        get { return isLogged; }
    }
    public bool IsEmployed
    {
        get { return isEmployed; }
    }
    public string GetProfileFirstName
    {
        get { return firstName; }
    }
    public string GetProfileLastName
    {
        get { return lastName; }
    }
    public string ProfileUsername
    {
        set { profileUsername.text = value; }
    }
    public string ProfileFirstName
    {
        set { profileFirstName.text = value; }
    }
    public string ProfileLastName
    {
        set { profileLastName.text = value; }
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
            if(appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Cerinte de înregistrare: \n -> nume utilizator > 7 caractere \n -> parola > 7 caractere \n -> un email real pentru a putea sa iti recuperezi contul.";
                errorInfoObj.SetActive(true);
            }
            else if(appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Register requirements: \n -> username > 7 characters \n -> password > 7 characters \n -> a real email so you can recover your account if lost";
                errorInfoObj.SetActive(true);
            }
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
    }
    public void Register()
    {
        StartCoroutine(RegisterAccount());
    }
    IEnumerator RegisterAccount()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("username", registerUsername.text));
        form.Add(new MultipartFormDataSection("password", registerPassword.text));
        form.Add(new MultipartFormDataSection("email", registerEmail.text));
        form.Add(new MultipartFormDataSection("firstName", registerFirstName.text));
        form.Add(new MultipartFormDataSection("lastName", registerLastName.text));
        if(notificationClass.NotificationToken != null || notificationClass.NotificationToken != "")
        {
            form.Add(new MultipartFormDataSection("notificationToken", notificationClass.NotificationToken));
        }
        UnityWebRequest webreq = UnityWebRequest.Post("http://mybarber.vlcapps.com/appscripts/register.php", form);
        yield return webreq.SendWebRequest();
        if (webreq.downloadHandler.text[0] == '0')
        {
            registerUsername.text = "";
            registerPassword.text = "";
            registerEmail.text = "";
            if (appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Account created successfully ! You can now login.";
            }
            else if (appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Contul a fost creat cu succes! Te poti conecta.";
            }
            errorInfoObj.SetActive(true);
        }
        else if (webreq.downloadHandler.text[0] == '1')
        {
            if (appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "There is a problem with the server's connection. Please try again. If the problem continues contact an administrator.";
            }
            else if (appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "A intervenit o problema cu conexiunea serverului. Te rugam sa incerci din nou. Daca problema persista contacteaza un administrator.";
            }
            errorInfoObj.SetActive(true);
        }
        else if (webreq.downloadHandler.text[0] == '3')
        {
            if (appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "There is already an account created with that name. Try another one.";
            }
            else if (appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Exista deja un cont creat cu acest nume. Incearca altul.";
            }
            errorInfoObj.SetActive(true);
        }
        else
        {
            if (appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Something went wrong. Please try again later. If the problem continues contact an administrator.";
            }
            else if (appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Ceva nu a functionat. Te rugam sa incerci din nou mai tarziu. Daca problema continua contacteaza un administrator.";
            }
            errorInfoObj.SetActive(true);
        }
        webreq.Dispose();
        appmanager.afterLogin();
    }
    public void OnApplicationQuit()
    {
        Save();
    }
    public void LoginAccount()
    {
        StartCoroutine(LoginEnumerator());
    }
    IEnumerator LoginEnumerator()
    {
        loadingScreen.SetActive(true);
        WWWForm form = new WWWForm();
        if(tempAccount == true)
        {
            accountUsername = usernameField.text;
            password = passwordField.text;
        }
        form.AddField("username", accountUsername);
        form.AddField("password", password);
        if (!string.IsNullOrEmpty(notificationClass.NotificationToken))
        {
            form.AddField("newToken", notificationClass.NotificationToken);
        }
        WWW www = new WWW("http://mybarber.vlcapps.com/appscripts/login.php", form);
        
        yield return www;
        if (www.text[0] == '0')
        {
            if (appmanager.SelectedLanguage == 2 && tempAccount == true)
            {
                errorInfoTXT.text = "Login succesful !";
                errorInfoObj.SetActive(true);
            }
            else if (appmanager.SelectedLanguage == 1 && tempAccount == true)
            {
                errorInfoTXT.text = "Conectare reusita !";
                errorInfoObj.SetActive(true);
            }
            appmanager.GetProfilePicture();
            accountUsername = www.text.Split('\t')[1];
            isLogged = true;
            bool.TryParse(www.text.Split('\t')[2], out isBoss);
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
            appmanager.afterLogin();
            if(appmanager.SelectedLanguage == 1)
            {
                profileUsername.text = "Nume utilizator: " + accountUsername;
                profileFirstName.text = "Prenume: " + firstName;
                profileLastName.text = "Nume: " + lastName;
                profilePersonalCode.text = "Cod personal: " + personalCode;
            }
            else if(appmanager.SelectedLanguage == 2)
            {
                profileUsername.text = "Username: " + accountUsername;
                profileFirstName.text = "First name: " + firstName;
                profileLastName.text = "Last name: " + lastName;
                profilePersonalCode.text = "Personal code: " + personalCode;
            }
            loadingScreen.SetActive(false);
            tempAccount = false;
            Save();
            Firebase.Messaging.FirebaseMessaging.SubscribeAsync("/barber/" + accountUsername);
        }
        else if(www.text[0] == '1')
        {
            if (appmanager.SelectedLanguage == 2 && tempAccount == true)
            {
                errorInfoTXT.text = "Login succesful !";
                errorInfoObj.SetActive(true);
            }
            else if (appmanager.SelectedLanguage == 1 && tempAccount == true)
            {
                errorInfoTXT.text = "Conectare reusita !";
                errorInfoObj.SetActive(true);
            }
            accountUsername = www.text.Split('\t')[1];
            isLogged = true;
            appmanager.GetProfilePicture();
            bool.TryParse(www.text.Split('\t')[2], out isBoss);
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
            if (appmanager.SelectedLanguage == 1)
            {
                profileUsername.text = "Nume utilizator: " + accountUsername;
                profileFirstName.text = "Prenume: " + firstName;
                profileLastName.text = "Nume: " + lastName;
                profilePersonalCode.text = "Cod personal: " + personalCode;
            }
            else if (appmanager.SelectedLanguage == 2)
            {
                profileUsername.text = "Username: " + accountUsername;
                profileFirstName.text = "First name: " + firstName;
                profileLastName.text = "Last name: " + lastName;
                profilePersonalCode.text = "Personal code: " + personalCode;
            }
            loadingScreen.SetActive(false);
            tempAccount = false;
            Save();
            Firebase.Messaging.FirebaseMessaging.SubscribeAsync("/topics/all");
        }
        else if(www.text[0] == '2')
        {
            if (appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Username or password incorrect !";
            }
            else if (appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Nume sau parola incorecte !";
            }
            errorInfoObj.SetActive(true);
            loadingScreen.SetActive(false);
        }
        else
        {
            if (appmanager.SelectedLanguage == 2)
            {
                errorInfoTXT.text = "Something went wrong! Please try again later. If the problem continues contact an administrator.";
            }
            else if (appmanager.SelectedLanguage == 1)
            {
                errorInfoTXT.text = "Ceva nu a functionat ! Te rugam sa incerci din nou. Daca problema persista contacteaza un administrator.";
                Debug.Log(www.text);
            }
            errorInfoObj.SetActive(true);
            loadingScreen.SetActive(false);
        }
        www.Dispose();
    }

}
