using System;
using UnityEngine;
using UnityEngine.Events;
public class Player : Damageable
{
    private CharacterHealthSystem _characterHealthSystem;
    public int CurrentHp => _characterHealthSystem.CurrentHp;
    public bool IsDead => _characterHealthSystem.IsDead;
    public UnityEvent OnDie;

    public void Initialized(CharacterData _characterData)
    {
        _characterHealthSystem = new PlayerHealthSystem(_characterData.MaxHp);
        _characterHealthSystem.DieAction += Die;
        _characterHealthSystem.DieAction += Die;
    }
    public override void TakeDamage(int damage) => _characterHealthSystem.TakeDamage(damage);
    public void Heal(int amount) => _characterHealthSystem.Heal(amount);
    public void Die()
    {
        Debug.Log("Player is dead");
        OnDie?.Invoke();
    }

}
