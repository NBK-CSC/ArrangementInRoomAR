using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private ItemData[] _itemDatas;
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private GameObject _itemTemplate;
    [SerializeField] private Transform _container;
    
    private void Start()
    {
        for (int i = 0; i < _itemDatas.Length; i++)
            AddItemData(_itemDatas[i]);
    }

    private void AddItemData(ItemData itemData)
    {
        Instantiate(_itemTemplate, _container).TryGetComponent(out ItemView itemView);
        itemView.Init(itemData);
        itemView.ItemSelected += OnItemSelected;
        itemView.ItemDisabled += InItemDisabled;
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
}
