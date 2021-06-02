using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("System")]

    [SerializeField] private NetManager netManager;

    [Header("Application Screens")]

    [SerializeField] private GameObject signInScreen;
    [SerializeField] private GameObject signUpScreen;
    [SerializeField] private GameObject collectionListScreen;
    [SerializeField] private GameObject collectionsPanel;

    [Header("Prefabs")]

    [SerializeField] private GameObject collectionPrefab;

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
        for(int i = 0; i < netManager.collectionList.Count; i++)
        {
            GameObject newCollection = Instantiate(collectionPrefab, collectionsPanel.transform);
            CollectionElement collectionElement = newCollection.GetComponent<CollectionElement>();
            collectionElement.collectionName = netManager.collectionList[i].name;
            collectionElement.backgroundImagePath = netManager.collectionList[i].backgroundImage;
            collectionElement.collectionImagePath = netManager.collectionList[i].previewImage;
        }
        
        signInScreen.SetActive(false);
        signUpScreen.SetActive(false);
        collectionListScreen.SetActive(true);
    }

}
