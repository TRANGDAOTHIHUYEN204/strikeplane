
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterHealthSystem _characterHealthSystem;

    public void Initiated(CharacterData _characterData)
    {
        _characterHealthSystem = new EnemyHealthSystem(_characterData.MaxHp);
    }
    public void TakeDamage(int damage) => _characterHealthSystem.TakeDamage(damage);
    public void Heal(int amount) => _characterHealthSystem.Heal(amount);

}
