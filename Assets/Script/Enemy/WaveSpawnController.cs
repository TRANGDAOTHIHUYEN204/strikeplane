using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{
    private SpawnEnemy _spawnEnemy;
    [SerializeField] private int _spawnCount;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Enemy _enemyPrefab;

    private Enemy _enemyTesting;
    private void Awake()
    {
        _spawnEnemy = GetComponent<SpawnEnemy>();
        if ( _spawnEnemy == null)
        {
            Debug.LogError("SpawnEnemy is missed in WaveSpawnController");
            enabled = false;
            
        }
    }
    // testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            WaveSpawn();
        }
    }
    public void WaveSpawn()
    {
        Debug.Log("Spawn lần 1");
        _enemyTesting = _spawnEnemy.SpawnEnemyWave(_enemyPrefab, _enemyData);
        
    }
    private void TestingEnemyPoolingInvalid()
    {
        Debug.Log("Enemy takes damage: ");
        _enemyTesting.TakeDamage(90);
        Debug.Log($"Enemy current Hp: {_enemyTesting.CurrentHp}");
        
    }
}
