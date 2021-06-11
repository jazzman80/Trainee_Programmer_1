using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorScreen : Screen
{
    public TextMeshProUGUI error;

    public void Alert(string errorText)
    {
        error.text = errorText;
        ShowScreen();
    }
}
