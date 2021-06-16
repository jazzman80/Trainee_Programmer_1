using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionElementView : MonoBehaviour
{
    public UIManager uIManager;
    public SpawnManager spawnManager;
    
    [SerializeField] private Image elementImage;
    [SerializeField] private Toggle favorite;
    private int collectionIndex;
    private int elementIndex;

    public void SetData(CollectionElement data, int _collectionIndex, int _elementIndex, UIManager _uIManager, SpawnManager _spawnManager)
    {
        uIManager = _uIManager;
        spawnManager = _spawnManager;
        collectionIndex = _collectionIndex;
        elementIndex = _elementIndex;
        favorite.isOn = data.isInFavorites;

        Davinci.get().load(data.imagePath).into(elementImage).start();
        elementImage.preserveAspect = true;
    }

    public void OnClick()
    {
        spawnManager.BuildElementScreen(collectionIndex, elementIndex);
        uIManager.ActivateElementScreen();
    }
}
