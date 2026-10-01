using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "EnemyData", order = 1)]
public class EnemyData : CharacterData
{
    [SerializeField] private  int _attackSpeed;

    public int AttackSpeed => _attackSpeed;

}
