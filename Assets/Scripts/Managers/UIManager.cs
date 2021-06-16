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
    public ElementScreen elementScreen;

    [Header("Vars")]
    public List<CollectionView> collectionViews = new List<CollectionView>();
    public List<CollectionScreen> collectionScreens = new List<CollectionScreen>();
    public int collectionIndex;

    private void HideAllScreens()
    {
        signUpScreen.HideScreen();
        signInScreen.HideScreen();
        errorScreen.HideScreen();
        collectionListScreen.HideScreen();
        collectionScreen.HideScreen();
        elementScreen.HideScreen();
    }

    public void ClearCollectionScreen()
    {
        for(int i = 0; i < collectionScreen.collectionElementViews.Count; i++)
        {
            Destroy(collectionScreen.collectionElementViews[i]);
        }
        collectionScreen.collectionElementViews.Clear();
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

    public void ActivateCollectionListScreen()
    {
        HideAllScreens();
        ClearCollectionScreen();
        collectionListScreen.ShowScreen();
    }

    public void ActivateCollectionScreen()
    {
        HideAllScreens();

        collectionScreen.ShowScreen();
    }

    public void ActivateElementScreen()
    {
        HideAllScreens();

        elementScreen.ShowScreen();
    }

}
