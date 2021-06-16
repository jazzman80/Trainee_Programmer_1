using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectionView : MonoBehaviour
{
    private UIManager uIManager;
    private SpawnManager spawnManager;
    private int index;
    [SerializeField] private TextMeshProUGUI collectionCaption;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image collectionImage;

    public void SetData(int _index, Collection data, UIManager _uIManager, SpawnManager _spawnManager)
    {
        uIManager = _uIManager;
        spawnManager = _spawnManager;
        index = _index;
        collectionCaption.text = data.name;
        Davinci.get().load(data.backgroundImage).into(backgroundImage).start();
        Davinci.get().load(data.previewImage).into(collectionImage).start();
    }

    public void OnClick()
    {
        spawnManager.BuildCollectionScreen(index);
        uIManager.ActivateCollectionScreen();
    }

}
