using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("System")]

    [SerializeField] private Spawner spawner;

    [Header("Application Screens")]

    [SerializeField] private GameObject signInScreen;
    [SerializeField] private GameObject signUpScreen;
    [SerializeField] private GameObject collectionListScreen;

    public void ActivateSignInScreen()
    {
        signInScreen.SetActive(true);
        signUpScreen.SetActive(false);
    }

    public void ActivateSignUpScreen()
    {
        signInScreen.SetActive(false);
        signUpScreen.SetActive(true);
    }

    public void ActivateCollectionsScreen()
    {
        spawner.LoadCollections();
        
        signInScreen.SetActive(false);
        signUpScreen.SetActive(false);
        collectionListScreen.SetActive(true);
    }

}
