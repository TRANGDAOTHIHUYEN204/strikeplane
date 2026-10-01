using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "CharacterData", order = 0)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _maxHp;

    [SerializeField] private int _armor;

    public string Name => _name;
    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = Mathf.Max(1, value); }
    }
    public int Armor
    {
        get { return _armor; }
        set { _armor = Mathf.Max(0, value); }
    }
}
