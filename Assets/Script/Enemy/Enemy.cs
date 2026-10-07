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
    public int DamageInteract => _enemyData.DamageInteract;
    public float ScoreEnemy => _enemyData.ScoreBaseEnemy;
    private EnemyShoot _enemyShoot;
    private UIManager _uiManager;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyShoot = GetComponent<EnemyShoot>();
    }
    public void Initiated(EnemyData _characterData, BulletFactory bulletFactory, BulletDataBase bulletDataBase, UIManager uiManager)
    {
        _enemyData = _characterData;
        _characterHealthSystem = new EnemyHealthSystem(_characterData.MaxHp);
        _characterHealthSystem.DieAction -= OnDie;
        _characterHealthSystem.DieAction += OnDie;
        gameObject.SetActive(true);
        _enemyMovement.Init(this, _characterData.SpeedMove);
        _enemyShoot.Init(bulletFactory);
        _uiManager = uiManager;
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
        _uiManager.ShowDamageText(damage, Position);
    }
    public void Heal(int amount) => _characterHealthSystem.Heal(amount);
    public void SetPool(FactoryEnemy enemyPool)
    {
        _enemyPool = enemyPool;
    }
    private void OnDie()
    {
        Debug.Log("Enemy is dead");
        ScoreManager.Instance.AddScore(ScoreEnemy);
        ScoreManager.Instance.AddKill();
        _enemyPool.Release(this);
    }
    public void Despawn()
    {
        _enemyPool.Release(this);
    }
}
