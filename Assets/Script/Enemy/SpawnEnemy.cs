using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private FactoryEnemy _factoryEnemy;
    public IReadOnlyList<Enemy> _enemyActive => _factoryEnemy.EnemyActive;
    private const float _spawnOffet = 2f;
    [SerializeField] private BulletFactory _bulletFactoryEnemy;
    [SerializeField] private BulletDataBase _bulletEnemyDataBase;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ScreenBoudaries _screenBoudaries;
    private void Awake()
    {
        _factoryEnemy = new FactoryEnemy();
    }

    public Enemy SpawnEnemyWave(Enemy enemyPrefab, EnemyData enemyData)
    {
        return SpawnEnemyWave(enemyPrefab, enemyData, PositionSpawnEnemy());
    }

    public Enemy SpawnEnemyWave(Enemy enemyPrefab, EnemyData enemyData, Vector2 position)
    {
        if (enemyPrefab == null) return null;

        Enemy currentEnemy = _factoryEnemy.CreateEntity(enemyPrefab, position);
        currentEnemy.Initiated(enemyData, _bulletFactoryEnemy, _bulletEnemyDataBase, _uiManager, _screenBoudaries);
        _factoryEnemy.Active(currentEnemy);
        return currentEnemy;
    }

    public Vector2 TopCenter(float offsetY = 0f)
    {
        float x = (_screenBoudaries.MinX + _screenBoudaries.MaxX) * 0.5f;
        return new Vector2(x, _screenBoudaries.MaxY + _spawnOffet + offsetY);
    }

    private Vector2 PositionSpawnEnemy()
    {
        float x = Random.Range(_screenBoudaries.MinX, _screenBoudaries.MaxX);
        float y = _screenBoudaries.MaxY + _spawnOffet;
        return new Vector2(x, y); 
    }
    public void ResetEnemy() => _factoryEnemy.ResetEnemy();

}
