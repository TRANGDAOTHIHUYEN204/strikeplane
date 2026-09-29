using UnityEngine;

public class CharacterHealthSystem
{
    private CharacterData _characterData;
    private int _currentHp;
    private bool _isDead;

    public void TakeDamage(int damage)
    {
        if (damage <= 0 ) return;
        if ( _isDead ) return;
        _currentHp -= damage;
        if ( _currentHp <= 0)
        {
            _currentHp = 0;
            _isDead = true;
        }
    }
    public void Heal(int amount)
    {
        if (amount <= 0 ) return;
        if (_isDead ) return;
        _currentHp += amount;
        if ( _currentHp >= _characterData.MaxHp)
        {
            _currentHp = _characterData.MaxHp;
        }
    }
   
    }
