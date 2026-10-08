using UnityEngine;

public class Enemy : Damageable
{
    private CharacterHealthSystem _characterHealthSystem;
    private EnemyData _enemyData;
    private FactoryEnemy _enemyPool;
    [SerializeField] private EnemyMovement enemyMovement;
    public int CurrentHp => _characterHealthSystem.CurrentHp;
    public bool IsDead => _characterHealthSystem.IsDead;
    public int AttackSpeed => _enemyData.AttackSpeed;
    public int DamageInteract => _enemyData.DamageInteract;
    public float ScoreEnemy => _enemyData.ScoreBaseEnemy;
    [SerializeField] private EnemyShoot enemyShoot;
    private UIManager _uiManager;
    
    public void Initiated(EnemyData characterData, BulletFactory bulletFactory, BulletDataBase bulletDataBase, UIManager uiManager, ScreenBoudaries screenBoudaries)
    {
        _enemyData = characterData;
        _characterHealthSystem = new EnemyHealthSystem(characterData.MaxHp);
        _characterHealthSystem.DieAction -= OnDie;
        _characterHealthSystem.DieAction += OnDie;
        gameObject.SetActive(true);
        enemyMovement.Init(this, characterData.SpeedMove, screenBoudaries);
        enemyShoot.Init(bulletFactory);
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
    
    public void SetMove(MoveBase move, float duration)
    {
        enemyMovement.SetMove(move, duration);
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
