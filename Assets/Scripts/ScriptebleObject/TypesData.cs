using UnityEngine;

[CreateAssetMenu(fileName = "New TypesData", menuName = "TypesData",order = 51)]
public class TypesData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private ItemData[] _furnitures;
    [SerializeField] private TypesData[] _typesFurniture;
    
    public Sprite Icon => _icon;
    public string Label => _label;
    virtual public ItemData[] Furnitures => _furnitures;
    virtual public TypesData[] TypesFurniture     {
        get => _typesFurniture; 
        set => _typesFurniture = value;
    }
}
