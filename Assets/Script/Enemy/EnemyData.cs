using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "EnemyData", order = 1)]
public class EnemyData : CharacterData
{
    [SerializeField] private  int _attackSpeed;
    [SerializeField] private int _speedMove;
    [SerializeField] private int _damageInteract;
    public int AttackSpeed => _attackSpeed;
    public int SpeedMove => _speedMove;
    public int DamageInteract => _damageInteract;

}
