using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _itemTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private TypesData[] _furnituresData;
    [SerializeField] private TypesData _back;
    
    private List<GameObject> _itemOnScrollView;

    private void Start()
    {
        _itemOnScrollView = new List<GameObject>();
        for (int i = 0; i < _furnituresData.Length; i++)
            AddData(_furnituresData[i]);
    }

    private void AddData(ItemData itemData)
    {
        var item = Instantiate(_itemTemplate, _container);
        _itemOnScrollView.Add(item);
        if (item.TryGetComponent(out ItemView itemView))
        {
            itemView.Init(itemData);
            itemView.ItemSelected += OnItemSelected;
            itemView.ItemDisabled += InItemDisabled;
        }
    }
    
    private void AddData(TypesData typesData)
    {
        var item = Instantiate(_itemTemplate, _container);
        _itemOnScrollView.Add(item);
        if (item.TryGetComponent(out ItemView itemView))
        {
            itemView.Init(typesData);
            itemView.TypeSelected += OnTypeSelected;
            itemView.TypeDisabled += InTypeDisabled;
        }
    }

    private void OnItemSelected(ItemData itemData)
    {
        _objectPlacer.SetInstallObject(itemData);
    }

    private void InItemDisabled(ItemView itemView)
    {
        itemView.ItemSelected -= OnItemSelected;
        itemView.ItemDisabled -= InItemDisabled;
    }
    
    private void OnTypeSelected(TypesData typesData)
    {
        ClearSelectionPanel();
        _back.TypesFurniture = _furnituresData;
        if (_furnituresData!=typesData.TypesFurniture)AddData(_back);
        for (int i = 0; i < typesData.TypesFurniture.Length; i++)
            AddData(typesData.TypesFurniture[i]);
        for (int i = 0; i < typesData.Furnitures.Length; i++)
            AddData(typesData.Furnitures[i]);
    }

    private void InTypeDisabled(ItemView itemView)
    {
        itemView.TypeSelected -= OnTypeSelected;
        itemView.TypeDisabled -= InTypeDisabled;
    }

    private void ClearSelectionPanel()
    {
        foreach (var item in _itemOnScrollView)
            Destroy(item);
        _itemOnScrollView.Clear();
    }
}
