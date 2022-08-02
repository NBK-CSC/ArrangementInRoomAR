using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData",order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private GameObject _prefab;

    public Sprite Icon => _icon;
    public string Label => _label;
    public GameObject Prefab => _prefab;
}
