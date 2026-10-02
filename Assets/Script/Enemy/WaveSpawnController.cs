using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{
    private SpawnEnemy _spawnEnemy;
    [SerializeField] private int _spawnCount;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Enemy _enemyPrefab;
    private Enemy _enemy;
    private int currentCountEnemy;
    [SerializeField] private float _timeSpawnEnemy;
    private float _currentTime;

    private void Awake()
    {
        _spawnEnemy = GetComponent<SpawnEnemy>();
        if ( _spawnEnemy == null)
        {
            Debug.LogError("SpawnEnemy is missed in WaveSpawnController");
            enabled = false;
            
        }
    }
    private void Update()
    {
        if (currentCountEnemy >= _spawnCount) return;
        _currentTime += Time.deltaTime;
        if (_currentTime >= _timeSpawnEnemy)
        {
            WaveSpawn();
            _currentTime = 0f;
        }
        
    }
    public void WaveSpawn()
    {
        Debug.Log("Spawn 1 enemy ");
        _enemy = _spawnEnemy.SpawnEnemyWave(_enemyPrefab, _enemyData);
        currentCountEnemy ++;
    }
}
