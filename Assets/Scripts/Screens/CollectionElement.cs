using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CollectionElement : MonoBehaviour
{
    [Header("Elements")]

    [SerializeField] private TextMeshProUGUI collectionCaption;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image collectionImage;

    public void SetData(Collection data)
    {
        collectionCaption.text = data.name;
        Davinci.get().load(data.backgroundImage).into(backgroundImage).start();
        Davinci.get().load(data.previewImage).into(collectionImage).start();
    }

}
