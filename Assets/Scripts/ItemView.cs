using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _selectionButton;

    private ItemData _itemData;
    private TypesData _typesData;

    public event UnityAction<ItemData> ItemSelected;
    public event UnityAction<ItemView> ItemDisabled;
    public event UnityAction<TypesData> TypeSelected;
    public event UnityAction<ItemView> TypeDisabled;

    private void OnEnable()
    {
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        ItemDisabled?.Invoke(this);
        TypeDisabled?.Invoke(this);
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    private void OnSelectionButtonClick()
    {
        if (_itemData != null)
            ItemSelected?.Invoke(_itemData);
        else
            TypeSelected?.Invoke(_typesData);
    }

    public void Init(ItemData itemData)
    {
        _itemData = itemData;
        _image.sprite = itemData.Icon;
        _label.text = itemData.Label;
    }
    
    public void Init(TypesData typesData)
    {
        _typesData = typesData;
        _image.sprite = typesData.Icon;
        _label.text = typesData.Label;
    }
}