using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignInScreen : Screen
{
    [SerializeField] UIManager uIManager;
    
    public TMP_InputField username;
    public TMP_InputField password;

    public bool CheckError()
    {
        uIManager.errorScreen.HideScreen();

        if (username.text == "")
        {
            uIManager.errorScreen.Alert("Please, enter your name");
            return true;
        }
        else if (password.text == "")
        {
            uIManager.errorScreen.Alert("Please, enter your password");
            return true;
        }
        else return false;
    }
}
