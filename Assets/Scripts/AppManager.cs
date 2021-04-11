using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    [SerializeField] private GameObject shopPrefab;
    private int whatToPick = 1;
    private string[] shopName;
    private string[] shopAddress;
    private bool isLoggedIn;
    private bool isBoss;
    private List<GameObject> shopList;

    public bool IsLoggedIn
    {
        set { isLoggedIn = value; }
    }
    public void backButton()
    {
        //adminButton.SetActive(isLoggedIn);
        loginButton.SetActive(!isLoggedIn);
        manageShopButton.SetActive(account.IsBoss);
        adminButton.SetActive(account.IsAdmin);
        loginMenu.SetActive(false);
        adminMenu.SetActive(false);
        backBTN.SetActive(false);
    }
    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex == 0)
        {
            ShowShops();
        }

        if (isLoggedIn && scene.buildIndex == 0)
        {
            loginButton.SetActive(!isLoggedIn);
            adminButton.SetActive(isLoggedIn);
            backButton();
        }
    }
    public void SelectSalon()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMainWindow()
    {
        SceneManager.LoadScene(0);
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

            WWW www = new WWW("http://localhost/barberapp/showShops.php", form);
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
            for(int i=0; i<shopName.Length; i++)
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
}
