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

    public event UnityAction<ItemData> ItemSelected;
    public event UnityAction<ItemView> ItemDisabled;

    private void OnEnable()
    {
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        ItemDisabled?.Invoke(this);
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    private void OnSelectionButtonClick()
    {
        ItemSelected?.Invoke(_itemData);
    }

    public void Init(ItemData itemData)
    {
        _itemData = itemData;
        _image.sprite = itemData.Icon;
        _label.text = itemData.Label;
    }
}
