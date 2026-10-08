using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "CharacterData", order = 0)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private int maxHp;

    [SerializeField] private int armor;

    public string Name => characterName;
    public int MaxHp
    {
        get { return maxHp; }
        private set { maxHp = Mathf.Max(1, value); }
    }
    public int Armor
    {
        get { return armor; }
        private set { armor = Mathf.Max(0, value); }
    }
}
