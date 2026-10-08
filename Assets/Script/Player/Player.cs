using System;
using UnityEngine;
using UnityEngine.Events;
public class Player : Damageable
{
    private CharacterHealthSystem _characterHealthSystem;
    public int CurrentHp => _characterHealthSystem.CurrentHp;
    public int MaxHp => _characterHealthSystem.MaxHp;
    public bool IsDead => _characterHealthSystem.IsDead;
    public UnityEvent OnDie;
    [SerializeField] private HealthPlayerManager healthPlayerManager;
    public void Initialized(CharacterData _characterData)
    {
        _characterHealthSystem = new PlayerHealthSystem(_characterData.MaxHp);
        _characterHealthSystem.DieAction += Die;
        DisplayHealthSystem();
    }

    public override void TakeDamage(int damage)
    {
        _characterHealthSystem.TakeDamage(damage);
        DisplayHealthSystem();
    }
    public void Heal(int amount)
    { 
        _characterHealthSystem.Heal(amount);
        DisplayHealthSystem();
    } 
    public void Die()
    {
        Debug.Log("Player is dead");
        OnDie?.Invoke();
    }

    private void DisplayHealthSystem()
    {
        healthPlayerManager.ApplyCurrentHealth(CurrentHp);
        healthPlayerManager.ApplyMaxHealth(MaxHp);
    }
    public void ResetPlayer()
    {
        _characterHealthSystem.ResetHpPlayer();
        DisplayHealthSystem();
    }
    
}