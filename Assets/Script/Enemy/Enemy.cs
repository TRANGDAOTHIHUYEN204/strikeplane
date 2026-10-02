using UnityEngine;

public class Enemy : Damageable
{
    private CharacterHealthSystem _characterHealthSystem;
    private EnemyData _enemyData;
    private FactoryEnemy _enemyPool;
    private EnemyMovement _enemyMovement;
    public int CurrentHp => _characterHealthSystem.CurrentHp;
    public bool IsDead => _characterHealthSystem.IsDead;
    public int AttackSpeed => _enemyData.AttackSpeed;
    
    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }
    public void Initiated(EnemyData _characterData)
    {
        _enemyData = _characterData;
        _characterHealthSystem = new EnemyHealthSystem(_characterData.MaxHp);
        _characterHealthSystem.DieAction += OnDie;
        gameObject.SetActive(true);
        _enemyMovement.Init(this, _characterData.SpeedMove);

    }
    private bool isReady()
    {
        if (_characterHealthSystem == null)
        {
            Debug.Log("CharacterHealthSystem is missed");
            return false;
            
        }
        return true;
    }
    public override void TakeDamage(int damage)
    {
        if (!isReady()) return;
        _characterHealthSystem?.TakeDamage(damage);
    }
    public void Heal(int amount) => _characterHealthSystem.Heal(amount);
    public void SetPool(FactoryEnemy enemyPool)
    {
        _enemyPool = enemyPool;
    }
    private void OnDie()
    {
        Debug.Log("Enemy is dead");
        _enemyPool.Release(this);
    }
    public void Despawn()
    {
        _enemyPool.Release(this);
    }
}
