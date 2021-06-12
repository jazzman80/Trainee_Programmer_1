using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]

    public SignInScreen signInScreen;
    public SignUpScreen signUpScreen;
    public ErrorScreen errorScreen;
    public CollectionListScreen collectionListScreen;
    public CollectionScreen collectionScreen;

    [Header("Sign Up Screen")]
    [SerializeField] private TextMeshProUGUI signUpError;

    private void HideAllScreens()
    {
        signUpScreen.HideScreen();
        signInScreen.HideScreen();
        errorScreen.HideScreen();
        collectionListScreen.HideScreen();
        collectionScreen.HideScreen();
    }

    public void SuccessRegistration()
    {
        HideAllScreens();
        errorScreen.Alert("A verify mail has been sent to your address.Please confirm your email to login");
        signInScreen.ShowScreen();
    }
    
    public void ActivateSignInScreen()
    {
        HideAllScreens();

        signInScreen.ShowScreen();
    }

    public void ActivateSignUpScreen()
    {
        HideAllScreens();

        signUpScreen.ShowScreen();
    }

    public void ActivateCollectionsScreen()
    {
        HideAllScreens();
        if(!collectionListScreen.isCollectionsLoaded) collectionListScreen.ShowCollections();
        collectionListScreen.ShowScreen();
    }

    public void ActivateCollectionScreen()
    {
        HideAllScreens();
        collectionScreen.ShowCollectionElements();
        collectionScreen.ShowScreen();
    }

}
