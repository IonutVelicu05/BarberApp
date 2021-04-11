using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private Toggle isBossToggle;
    [SerializeField] private TMP_InputField shopNameField;
    [SerializeField] private TMP_InputField shopAddressField;

    public void RegisterAccount()
    {
        StartCoroutine(RegisterAccountEnum());
    }
    IEnumerator RegisterAccountEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        form.AddField("isBoss", isBossToggle.isOn.ToString());
        form.AddField("shopName", shopNameField.text);
        form.AddField("shopAddress", shopAddressField.text);
        WWW www = new WWW("http://localhost/barberapp/register.php", form);
        return www;
        if(www.text == "0")
        {
            usernameField.text = "";
            passwordField.text = "";
            isBossToggle.isOn = false;
            shopAddressField.text = "";
            shopNameField.text = "";
        }
    }
}
