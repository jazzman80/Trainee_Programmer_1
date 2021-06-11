using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignUpError : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI signUpErrorAlert;

    public void Alert(string error)
    {
        signUpErrorAlert.text = error;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
}
