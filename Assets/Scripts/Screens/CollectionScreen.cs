using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionScreen : Screen
{
    [SerializeField] private TextMeshProUGUI header;
    public List<GameObject> collectionElementViews = new List<GameObject>();

    public void SetData(string headerText)
    {
        header.text = headerText;
    }
}
