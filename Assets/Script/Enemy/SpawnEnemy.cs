using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private FactoryEnemy _factoryEnemy;
    public IReadOnlyList<Enemy> _enemyActive => _factoryEnemy.EnemyActive;

    [SerializeField] private BulletFactory _bulletFactoryEnemy;
    [SerializeField] private BulletDataBase _bulletEnemyDataBase;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ScreenBoudaries _screenBoudaries;
    [SerializeField] private int _maxEnemy = 10;
    [SerializeField, Range(0f, 1f)] private float _leftSpawnHeight = 0.8f;
    [SerializeField] private float _spawnOffset = 0f;
    [SerializeField] private bool _spawnJustOutside = false;

    public bool CanSpawn => _enemyActive.Count < _maxEnemy;

    private void Awake()
    {
        _factoryEnemy = new FactoryEnemy();
    }

    public Enemy SpawnEnemyWave(Enemy enemyPrefab, EnemyData enemyData)
    {
        return SpawnEnemyWave(enemyPrefab, enemyData, TopLeft(_leftSpawnHeight, enemyPrefab));
    }

    public Enemy SpawnEnemyWave(Enemy enemyPrefab, EnemyData enemyData, Vector2 position)
    {
        if (enemyPrefab == null) return null;
        if (!CanSpawn) return null;

        Enemy currentEnemy = _factoryEnemy.CreateEntity(enemyPrefab, position);
        currentEnemy.Initiated(enemyData, _bulletFactoryEnemy, _bulletEnemyDataBase, _uiManager, _screenBoudaries);
        _factoryEnemy.Active(currentEnemy);
        return currentEnemy;
    }

    public Coroutine SpawnOneByOne(Enemy prefab, EnemyData data, int count, float delay)
    {
        return StartCoroutine(SpawnOneByOneRoutine(prefab, data, count, delay));
    }

    private IEnumerator SpawnOneByOneRoutine(Enemy prefab, EnemyData data, int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemyWave(prefab, data, TopLeft(_leftSpawnHeight, prefab));
            yield return new WaitForSeconds(delay);
        }
    }
    
    private Vector2 HalfSize(Enemy prefab)
    {
        if (!_spawnJustOutside || prefab == null) return Vector2.zero;

        var sr = prefab.GetComponentInChildren<SpriteRenderer>();
        if (sr == null) return Vector2.zero;

        return sr.bounds.extents; // extents = nửa size
    }
    
    public Vector2 TopLeft(float tY = 0.8f, Enemy prefab = null)
    {
        float x = _screenBoudaries.ViewMinX - _spawnOffset - HalfSize(prefab).x;
        float y = Mathf.Lerp(_screenBoudaries.ViewMinY, _screenBoudaries.ViewMaxY, tY);
        return new Vector2(x, y);
    }
    public Vector2 TopCenter(float offsetY = 0f, float t = 0.75f, Enemy prefab = null)
    {
        float x = Mathf.Lerp(_screenBoudaries.ViewMinX, _screenBoudaries.ViewMaxX, t);
        float y = _screenBoudaries.ViewMaxY + _spawnOffset + HalfSize(prefab).y + offsetY;
        return new Vector2(x, y);
    }

    public void ResetEnemy() => _factoryEnemy.ResetEnemy();
}