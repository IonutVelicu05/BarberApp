using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject adminButton;
    [SerializeField] private GameObject manageShopButton;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject manageShopMenu;
    [SerializeField] private GameObject adminMenu;
    [SerializeField] private GameObject backBTN;
    [SerializeField] private Account account;
    private BarberShop barberShopClass;
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private TextMeshProUGUI shopDescription;
    [SerializeField] private InputField ShopDescriptionField;
    [SerializeField] private GameObject openWorkingHourPrefab;
    [SerializeField] private GameObject openWorkingMinutePrefab;
    [SerializeField] private GameObject closeWorkingHourPrefab;
    [SerializeField] private GameObject closeWorkingMinutePrefab;
    [SerializeField] private TextMeshProUGUI mondayHours;
    [SerializeField] private TextMeshProUGUI tuesdayHours;
    [SerializeField] private TextMeshProUGUI wednesdayHours;
    [SerializeField] private TextMeshProUGUI thursdayHours;
    [SerializeField] private TextMeshProUGUI fridayHours;
    [SerializeField] private TextMeshProUGUI saturdayHours;
    [SerializeField] private TextMeshProUGUI sundayHours;
    [SerializeField] private Image shopPhoto;
    [SerializeField] private InputField imageFromPhoneName;
    [SerializeField] private Dropdown createShopCityDropdown;
    [SerializeField] private Dropdown createShopCountyDropdown;
    private int shopImageNumber = 1;
    private bool anotherShopSelected = false; //when selecting a shop it sets it to true and if its true when selecting it shows the first image of
    //the shop; sets shopImageNumber to 1;
    private Texture2D imageFromPhone;

    private Image tempOpenHourImage;
    private Image tempOpenMinuteImage;
    private Image tempCloseHourImage;
    private Image tempCloseMinuteImage;
    private string selectedOpenWorkingMinute;
    private string selectedOpenWorkingHour;
    private string selectedCloseWorkingHour;
    private string selectedCloseWorkingMinute;
    private string updateWorkingProgram;
    private int selectedWorkingDay; //selected day to edit its working hours
    private int whatToPick = 1;
    private string[] shopName;
    private string[] shopAddress;
    private string selectedShopName;
    private List<GameObject> shopList = new List<GameObject>();
    private string[] shopWorkingHours = new string[8];



    //select day for changing its working hours
    public void Monday()
    {
        selectedWorkingDay = 1;
    }
    public void Tuesday()
    {
        selectedWorkingDay = 2;
    }
    public void Wednesday()
    {
        selectedWorkingDay = 3;
    }
    public void Thursday()
    {
        selectedWorkingDay = 4;
    }
    public void Friday()
    {
        selectedWorkingDay = 5;
    }
    public void Saturday()
    {
        selectedWorkingDay = 6;
    }
    public void Sunday()
    {
        selectedWorkingDay = 7;
    }

    public void backButton()
    {
        loginButton.SetActive(!account.IsLogged);
        manageShopButton.SetActive(account.IsBoss);
        adminButton.SetActive(account.IsAdmin);
        loginMenu.SetActive(false);
        manageShopMenu.SetActive(false);
        adminMenu.SetActive(false);
        backBTN.SetActive(false);
    }
    public void Start()
    {
        ShowShops();
        backButton();
        createShopCountyDropdown.onValueChanged.AddListener(delegate { CreateShopCountyPicked(createShopCountyDropdown); });
        
    }
    public void SelectSalon()
    {
        shopImageNumber = 1;
        setimagine();
        selectedShopName = EventSystem.current.currentSelectedGameObject.name;
        ShowShopDescription();
    }
    public void ShowShops()
    {
        StartCoroutine(ShowShopsEnumerator());
    }
    IEnumerator ShowShopsEnumerator()
    {
        for(int i=0; i<2; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("whatToPick", whatToPick);

            WWW www = new WWW("http://81.196.99.232/barberapp/showShops.php", form);
            yield return www;

            switch (whatToPick)
            {
                case 1: shopName = www.text.Split('\t');
                    whatToPick = 2;
                    break;
                case 2: shopAddress = www.text.Split('\t');
                    whatToPick = 1;
                    break;
            }
        }
        if(shopName.Length > 0)
        {
            for(int i=0; i<shopName.Length-1; i++)
            {
                GameObject barberShop = Instantiate(shopPrefab);
                barberShop.name = shopName[i];
                barberShop.SetActive(true);
                barberShop.GetComponent<BarberShop>().ShopName = shopName[i];
                barberShop.GetComponent<BarberShop>().ShopAddress = shopAddress[i];
                barberShop.transform.SetParent(shopPrefab.transform.parent, false);
                shopList.Add(barberShop);
            }
        }
    }

    public void EditShopDescription()
    {
        StartCoroutine(EditShopDescriptionEnum());
    }
    IEnumerator EditShopDescriptionEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("description", ShopDescriptionField.text);
        form.AddField("username", account.AccountUsername);
        WWW www = new WWW("http://81.196.99.232/barberapp/editshopdescription.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("descruiption changed");
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void ShowShopDescription()
    {
        StartCoroutine(ShowShopDescriptionEnum());
    }
    IEnumerator ShowShopDescriptionEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("shopname", selectedShopName);

        WWW www = new WWW("http://81.196.99.232/barberapp/showdescription.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            shopDescription.text = www.text.Split('\t')[1];
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void SelectOpenWorkingHour()
    {
        selectedOpenWorkingHour = EventSystem.current.currentSelectedGameObject.name;
        if (tempOpenHourImage != null)
        {
            tempOpenHourImage.color = Color.white;
        }
        tempOpenHourImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        tempOpenHourImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }
    public void SelectOpenWorkingMinute()
    {
        selectedOpenWorkingMinute = EventSystem.current.currentSelectedGameObject.name;
        if (tempOpenMinuteImage != null)
        {
            tempOpenMinuteImage.color = Color.white;
        }
        tempOpenMinuteImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        tempOpenMinuteImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }
    public void SelectCloseWorkingHour()
    {
        selectedCloseWorkingHour = EventSystem.current.currentSelectedGameObject.name;
        if (tempCloseHourImage != null)
        {
            tempCloseHourImage.color = Color.white;
        }
        tempCloseHourImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        tempCloseHourImage.color = new Color(0.53f, 0.53f, 0.53f, 1f);
    }
    public void SelectCloseWorkingMinute()
    {
        selectedCloseWorkingMinute = EventSystem.current.currentSelectedGameObject.name;
        if (tempCloseMinuteImage != null) //daca deja este un buton selectat il deselecteaza/face culoare alb
        {
            tempCloseMinuteImage.color = Color.white;
        }
        tempCloseMinuteImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>(); //schimba valoarea la tempimage
        tempCloseMinuteImage.color = new Color(0.53f, 0.53f, 0.53f, 1f); //schimba culoarea la imagine / selecteaza butonu
    }
    public void CreateHoursButtons() //creating buttons for every hour after selecting the day to modify the program for
    {
        for(int i = 0; i < 25; i ++)
        {
            GameObject openWorkingHourBTN = Instantiate(openWorkingHourPrefab, openWorkingHourPrefab.transform.position, openWorkingHourPrefab.transform.rotation, openWorkingHourPrefab.transform.parent);
            GameObject closeWorkingHourBTN = Instantiate(closeWorkingHourPrefab, closeWorkingHourPrefab.transform.position, closeWorkingHourPrefab.transform.rotation, closeWorkingHourPrefab.transform.parent);
            closeWorkingHourBTN.SetActive(true);
            openWorkingHourBTN.SetActive(true);
            if (i < 10)
            {
                closeWorkingHourBTN.name = "0" + i.ToString();
                closeWorkingHourBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
                openWorkingHourBTN.name = "0" + i.ToString();
                openWorkingHourBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
            }
            else
            {
                closeWorkingHourBTN.name = i.ToString();
                closeWorkingHourBTN.GetComponentInChildren<Text>().text = i.ToString();
                openWorkingHourBTN.name = i.ToString();
                openWorkingHourBTN.GetComponentInChildren<Text>().text = i.ToString();
            }

        }
    }
    public void CreateMinuteButtons() //creating buttons for every minute after selecting the day to modify the program for
    {
        for(int i =0; i<60; i++)
        {
            GameObject openWorkingMinuteBTN = Instantiate(openWorkingMinutePrefab, openWorkingMinutePrefab.transform.position, openWorkingMinutePrefab.transform.rotation, openWorkingMinutePrefab.transform.parent);
            GameObject closeWorkingMinuteBTN = Instantiate(closeWorkingMinutePrefab, closeWorkingMinutePrefab.transform.position, closeWorkingMinutePrefab.transform.rotation, closeWorkingMinutePrefab.transform.parent);

            closeWorkingMinuteBTN.SetActive(true);
            openWorkingMinuteBTN.SetActive(true);
            if (i < 10)
            {
                openWorkingMinuteBTN.name = "0" + i.ToString();
                openWorkingMinuteBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
                closeWorkingMinuteBTN.name = "0" + i.ToString();
                closeWorkingMinuteBTN.GetComponentInChildren<Text>().text = "0" + i.ToString();
            }
            else
            {
                openWorkingMinuteBTN.name = i.ToString();
                openWorkingMinuteBTN.GetComponentInChildren<Text>().text = i.ToString();
                closeWorkingMinuteBTN.name = i.ToString();
                closeWorkingMinuteBTN.GetComponentInChildren<Text>().text = i.ToString();
            }
        }
    }
    public void UpdateShopWorkingHours()
    {
        StartCoroutine(UpdateShopWH());
    }
    IEnumerator UpdateShopWH()
    {
        if(selectedOpenWorkingMinute == null || selectedOpenWorkingMinute == "") //in caz ca nu alege niciun minut se pune 00
        {
            selectedOpenWorkingMinute = "00";
        }
        if(selectedCloseWorkingMinute == null || selectedCloseWorkingMinute == "") //in caz ca nu alege niciun minut se pune 00
        {
            selectedCloseWorkingMinute = "00";
        }
        updateWorkingProgram = selectedOpenWorkingHour + ":" + selectedOpenWorkingMinute + "-" 
            + selectedCloseWorkingHour + ":" + selectedCloseWorkingMinute;

        WWWForm form = new WWWForm();
        form.AddField("selectedDay", selectedWorkingDay);
        form.AddField("workingHours", updateWorkingProgram);
        form.AddField("bossName", account.AccountUsername);

        WWW www = new WWW("http://81.196.99.232/barberapp/editshopworkinghours.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("updated");
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    public void GetShopWorkingHours()
    {
        StartCoroutine(ShopWorkingHoursEnum());
    }
    IEnumerator ShopWorkingHoursEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("shopName", selectedShopName);
        WWW www = new WWW("http://81.196.99.232/barberapp/getworkinghours.php", form);
        yield return www;
        
        if (www.text[0] != '0')
        {
            Debug.Log(www.text);
        }
        foreach(string str in www.text.Split('\t'))
        {
            if (str == null || str == "")
            {
                
            }
        }

        mondayHours.text = "Monday: " + www.text.Split('\t')[1];
        tuesdayHours.text = "Tuesday: " + www.text.Split('\t')[2];
        wednesdayHours.text = "Wednesday: " + www.text.Split('\t')[3];
        thursdayHours.text = "Thursday: " + www.text.Split('\t')[4];
        fridayHours.text = "Friday: " + www.text.Split('\t')[5];
        saturdayHours.text = "Saturday: " + www.text.Split('\t')[6];
        sundayHours.text = "Sunday: " + www.text.Split('\t')[7];
        for (int i = 0; i < 8; i++) // daca nu e nicio ora setata nu se lucreaza in ziua aia
        {
            if (www.text.Split('\t')[i] == null || www.text.Split('\t')[i] == "")
            {
                switch (i)
                {
                    case 1:
                        mondayHours.text = "Monday: Not working";
                        break;
                    case 2:
                        tuesdayHours.text = "Tuesday: Not working";
                        break;
                    case 3:
                        wednesdayHours.text = "Wednesday: Not working";
                        break;
                    case 4:
                        thursdayHours.text = "Thursday: Not working";
                        break;
                    case 5:
                        fridayHours.text = "Friday: Not working";
                        break;
                    case 6:
                        saturdayHours.text = "Saturday: Not working";
                        break;
                    case 7:
                        sundayHours.text = "Sunday: Not working";
                        break;
                }
            }
        }
    }
    public void nextShopImage()
    {
        if (shopImageNumber < 5)
        {
        shopImageNumber++;
        }
        setimagine();
    }
    public void previousShopImage()
    {
        if(shopImageNumber > 1)
        {
        shopImageNumber--;
        }
        setimagine();
    }
    public void setimagine()
    {
        StartCoroutine(DownloadImage("http://81.196.99.232/barberapp/shops/" + selectedShopName + "/image" + shopImageNumber.ToString())); //balanced parens CAS
    }
    private bool isFailedImage(Texture tex) // to check if the downloaded image is equal to the question mark image(when it fails loading)
    {
        if (!tex) return true;

        byte[] png1 = (tex as Texture2D).EncodeToPNG(); // encode the downloaded texture to bytes
        // bytes representation of the question mark image
        byte[] questionMarkPNG = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 2, 0, 0, 0, 75, 109, 41, 220, 0, 0, 0, 65, 73, 68, 65, 84, 8, 29, 85, 142, 81, 10, 0, 48, 8, 66, 107, 236, 254, 87, 110, 106, 35, 172, 143, 74, 243, 65, 89, 85, 129, 202, 100, 239, 146, 115, 184, 183, 11, 109, 33, 29, 126, 114, 141, 75, 213, 65, 44, 131, 70, 24, 97, 46, 50, 34, 72, 25, 39, 181, 9, 251, 205, 14, 10, 78, 123, 43, 35, 17, 17, 228, 109, 164, 219, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130, };

        return Equivalent(png1, questionMarkPNG); //return if they are the same or not
    }
    public bool Equivalent(byte[] bytes1, byte[] bytes2) // check 2 arrays of bytes to see if they are the same or not
    {
        if (bytes1.Length != bytes2.Length) return false;
        for (int i = 0; i < bytes1.Length; i++)
            if (!bytes1[i].Equals(bytes2[i])) return false;
        return true;
    }
    IEnumerator DownloadImage(string url)
    {
        WWW www = new WWW(url + ".jpg");
        WWW w = new WWW(url + ".png");
        yield return www;
        yield return w;
        if (isFailedImage(www.texture))
        {
            shopPhoto.sprite = Sprite.Create(w.texture, new Rect(0, 0, w.texture.width, w.texture.height), new Vector2(0, 0));
        }
        else
        {
            shopPhoto.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }

    }
    public void uploadImage()
    {
        StartCoroutine(StartUploading());
    }
    IEnumerator StartUploading()
    {
        WWWForm form = new WWWForm();
        byte[] textureBytes = null;

        //Get a copy of the texture, because we can't access original texure data directly. 
        Texture2D photoTexture = GetTextureCopy(imageFromPhone);
        textureBytes = photoTexture.EncodeToJPG();
        string imageName = "image" + shopImageNumber + ".jpg";
        form.AddBinaryData("myimage", textureBytes, imageName, "imagebro.jpg");
        form.AddField("shopName", account.ShopNameOfUser);

        WWW w = new WWW("http://81.196.99.232/barberapp/uploadimage.php", form);

        yield return w;
        Debug.Log(w.text);
        w.Dispose();
    }

    Texture2D GetTextureCopy(Texture2D source)
    {
        //Create a RenderTexture
        RenderTexture rt = RenderTexture.GetTemporary(
                               source.width,
                               source.height,
                               0,
                               RenderTextureFormat.Default,
                               RenderTextureReadWrite.Linear
                           );

        //Copy source texture to the new render (RenderTexture) 
        Graphics.Blit(source, rt);

        //Store the active RenderTexture & activate new created one (rt)
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        //Create new Texture2D and fill its pixels from rt and apply changes.
        Texture2D readableTexture = new Texture2D(source.width, source.height);
        readableTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readableTexture.Apply();

        //activate the (previous) RenderTexture and release texture created with (GetTemporary( ) ..)
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return readableTexture;
    }
    public void PickPhoneImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image

                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                imageFromPhone = texture; // poza de upload sa fie textura luata din telefon
                //imageFromPhoneName.text = path.Split('/')[path.Split('/').Length - 1]; //seteaza inputFieldu sa aibe numele pozei selectate
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }
    public void CreateShopCountyPicked(Dropdown createShopCounty)
    {
        switch (createShopCounty.value)
        {
            case 1:
                createShopCityDropdown.interactable = true;
                List<string> cityOltOptions = new List<string> { "Pick a city", "Caracal", "Farcasele", "Slatina", "Corabia" };
                createShopCityDropdown.ClearOptions();
                createShopCityDropdown.AddOptions(cityOltOptions);
                break;
            case 2:
                createShopCityDropdown.interactable = true;
                createShopCityDropdown.ClearOptions();
                List<string> cityDoljOptions = new List<string> { "Pick a city","Craiova", "Radovan", "Gogosu", "Gura Racului" };
                createShopCityDropdown.AddOptions(cityDoljOptions);
                break;
        }
    }
}
