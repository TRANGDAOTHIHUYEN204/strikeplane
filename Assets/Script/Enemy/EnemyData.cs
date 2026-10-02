using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "EnemyData", order = 1)]
public class EnemyData : CharacterData
{
    [SerializeField] private  int _attackSpeed;
    [SerializeField] private int _speedMove;
    public int AttackSpeed => _attackSpeed;
    public int SpeedMove => _speedMove;

}
