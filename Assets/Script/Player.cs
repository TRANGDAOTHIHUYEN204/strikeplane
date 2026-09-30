using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterHealthSystem _characterHealthSystem;
    public int CurrentHp => _characterHealthSystem.CurrentHp;
    public bool IsDead => _characterHealthSystem.IsDead;
    public void Initialized(CharacterData _characterData)
    {
        _characterHealthSystem = new PlayerHealthSystem(_characterData.MaxHp);
        
    }
    public void TakeDamage(int damage) => _characterHealthSystem.TakeDamage(damage);
    public void Heal(int amount) => _characterHealthSystem.Heal(amount);
}
