using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionListScreen : Screen
{
    [SerializeField] NetManager netManager;
    [SerializeField] CollectionElement collectionElementPrefab;
    [SerializeField] Transform collectionList;
    public bool isCollectionsLoaded = false;

    public void ShowCollections()
    {
        for(int i = 0; i < netManager.collectionList.Count; i++)
        {
            CollectionElement collectionElement = Instantiate(collectionElementPrefab, collectionList);
            collectionElement.SetData(netManager.collectionList[i]);
        }

        isCollectionsLoaded = true;
    }
}
