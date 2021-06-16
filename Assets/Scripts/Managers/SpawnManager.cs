using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private Database database;
    [SerializeField] private UIManager uIManager;

    [Header("Collection List Screen")]
    [SerializeField] private CollectionView collectionViewPrefab;
    [SerializeField] private Transform collectionListScreenBody;

    [Header("Collection Screen")]
    [SerializeField] private CollectionScreen collectionScreen;
    [SerializeField] private CollectionElementView elementPrefab;
    [SerializeField] private Transform collectionScreenBody;

    [Header("Element Screen")]
    [SerializeField] private ElementScreen elementScreen;


    public void BuildCollectionListScreen()
    {
        for(int i = 0; i < database.collections.Count; i++)
        {
            CollectionView newCollectionView = Instantiate(collectionViewPrefab, collectionListScreenBody);
            newCollectionView.SetData(i, database.collections[i], uIManager, this);
        }
    }

    public void BuildCollectionScreen(int collectionIndex)
    {
        collectionScreen.SetData(database.collections[collectionIndex].name);

        for(int i = 0; i < database.collections[collectionIndex].elements.Count; i++)
        {
            CollectionElementView newCollectionElementView = Instantiate(elementPrefab, collectionScreenBody);
            newCollectionElementView.SetData(database.collections[collectionIndex].elements[i], collectionIndex, i, uIManager, this);
            collectionScreen.collectionElementViews.Add(newCollectionElementView.gameObject); 

        }
    }

    public void BuildElementScreen(int collectionIndex, int elementIndex)
    {
        elementScreen.SetData(database.collections[collectionIndex], database.collections[collectionIndex].elements[elementIndex], collectionIndex, elementIndex);
    }
}
