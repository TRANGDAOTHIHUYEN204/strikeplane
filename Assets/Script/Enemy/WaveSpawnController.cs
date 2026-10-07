using UnityEngine;
using System.Collections;

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
    [SerializeField] private LoseManager _loseManager;
    private Coroutine _winCoroutine;
    
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
    private IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1f);

        while (_spawnEnemy._enemyActive.Count > 0)
        {
            if (LoseManager.isGameOver) yield break;
            yield return new WaitForSeconds(0.5f);
        }
        if (!LoseManager.isGameOver)
            ShowWin();
    }
    private void ShowWin()
    {
        _loseManager.SetActiveWinPanel();
    }
    public void WaveSpawn()
    {
        _enemy = _spawnEnemy.SpawnEnemyWave(_enemyPrefab, _enemyData);
        _currentCountEnemy ++;

        if (_currentCountEnemy >= _spawnCount)
            _winCoroutine = StartCoroutine(WaitForWin());

    }
    public void ResetSpawn()
    {
        if (_winCoroutine != null)
        {
            StopCoroutine(_winCoroutine);
            _winCoroutine = null;
        }
        _currentCountEnemy = 0;
        _currentTime = 0f;
        _spawnEnemy.ResetEnemy();
    }
}
