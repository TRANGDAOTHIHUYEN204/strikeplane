using UnityEngine;

public class CharacterHealthSystem
{
    private int _currentHp;
    private bool _isDead;
    private int _maxHp;

    public int CurrentHp
    {
        get { return _currentHp; }
        private set { _currentHp = value; }
    }

    public bool IsDead => _isDead;
    public int MaxHp => _maxHp;

    public CharacterHealthSystem(int maxHp)
    {
        _maxHp = maxHp;
        _currentHp = maxHp;
    }

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
        if ( _currentHp >= MaxHp)
        {
            _currentHp = MaxHp;
        }
    }
   
}
