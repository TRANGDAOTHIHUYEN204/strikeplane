using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{
    private SpawnEnemy _spawnEnemy;
    [SerializeField] private int _spawnCount;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Enemy _enemyPrefab;
    private Enemy _enemy;
    private int _currentCountEnemy;
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
    private void Start()
    {
        ScoreManager.Instance.SetTotalKill(_spawnCount);
    }
    private void Update()
    {
        if (LoseManager.isGameOver) return;
        if (_currentCountEnemy >= _spawnCount) return;
        _currentTime += Time.deltaTime;
        if (_currentTime >= _timeSpawnEnemy)
        {
            WaveSpawn();
            _currentTime = 0f;
        }
        
    }
    public void WaveSpawn()
    {
        _enemy = _spawnEnemy.SpawnEnemyWave(_enemyPrefab, _enemyData);
        _currentCountEnemy ++;
    }
    public void ResetSpawn()
    {
        _currentCountEnemy = 0;
        _currentTime = 0f;
        _spawnEnemy.ResetEnemy();
    }

}
