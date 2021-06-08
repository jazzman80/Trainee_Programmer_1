using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] NetManager netManager;
    [SerializeField] CollectionElement collectionPrefab;
    [SerializeField] Transform collectionsPanel;

    public void LoadCollections()
    {
        for (int i = 0; i < netManager.collectionList.Count; i++)
        {
            CollectionElement collectionElement = Instantiate(collectionPrefab, collectionsPanel);
            collectionElement.SetData(netManager.collectionList[i]);
        }
    }
}
