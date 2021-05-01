using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField passwordField;
    private string[] stringmemory = new string[8];

    public string Stringmemory(int arrayIndex)
    {
        return stringmemory[arrayIndex];
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
        WWW www = new WWW("http://81.196.99.236/barberapp/login.php", form);
        
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("login suces");
            stringmemory[1] = www.text.Split('\t')[1];
            if(stringmemory[1] == "IonutVelicu2205")
            {
            }
        }
        else
        {
            Debug.Log(www.text);
        }
    }
}
