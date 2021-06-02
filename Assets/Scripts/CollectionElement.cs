using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CollectionElement : MonoBehaviour
{
    public string collectionName;
    public string backgroundImagePath;
    public string collectionImagePath;

    [Header("Elements")]

    [SerializeField] private TextMeshProUGUI collectionCaption;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image collectionImage;

    private void Start()
    {
        collectionCaption.text = collectionName;
        Davinci.get().load(backgroundImagePath).into(backgroundImage).start();
        Davinci.get().load(collectionImagePath).into(collectionImage).start();
    }

}
