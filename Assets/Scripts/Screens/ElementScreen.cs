using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementScreen : Screen
{
    [SerializeField] private NetManager netManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Database database;
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private Image productImage;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI rating;
    [SerializeField] private Toggle favorite;
    private int index;
    private int collectionIndex;
    private int elementIndex;

    public void SetData(Collection collectionData, CollectionElement elementData, int _collectionIndex, int _elementIndex)
    {
        collectionIndex = _collectionIndex;
        elementIndex = _elementIndex;
        
        header.text = collectionData.name;

        Davinci.get().load(elementData.imagePath).into(productImage).start();
        productImage.preserveAspect = true;

        description.text = elementData.description;
        price.text = elementData.price;
        rating.text = elementData.rating;
        favorite.isOn = elementData.isInFavorites;
        index = elementData.index;
    }

    public void OnFavsClick(bool isInFavorites)
    {
        if (isInFavorites) netManager.FavoriteElement(index);
        else netManager.UnFavoriteElement(index);

        database.UpdateFav(collectionIndex, elementIndex, isInFavorites);
    }

    public void OnBackButtonClick()
    {
        uIManager.ClearCollectionScreen();
        spawnManager.BuildCollectionScreen(collectionIndex);
        uIManager.ActivateCollectionScreen();
    }
}
