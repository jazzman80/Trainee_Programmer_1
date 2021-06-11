using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignUpScreen : Screen
{
    [SerializeField] private UIManager uIManager;
    
    public TMP_InputField username;
    public TMP_InputField email;
    public TMP_InputField password;
    [SerializeField] private TMP_InputField confirm;

    public bool CheckError()
    {
        uIManager.errorScreen.HideScreen();
        
        if (username.text == "")
        {
            uIManager.errorScreen.Alert("Please, enter your name");
            return true;
        }
        else if (email.text == "")
        {
            uIManager.errorScreen.Alert("Please, enter your e-mail address");
            return true;
        }
        else if (password.text == "")
        {
            uIManager.errorScreen.Alert("Please, enter your password");
            return true;
        }
        else if (password.text != confirm.text)
        {
            uIManager.errorScreen.Alert("Please, confirm your password");
            return true;
        }
        else return false;
    }
}
