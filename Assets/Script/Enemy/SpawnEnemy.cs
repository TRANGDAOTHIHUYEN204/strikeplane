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
    private void Awake()
    {
        _factoryEnemy = new FactoryEnemy();
    }

    public Enemy SpawnEnemyWave(Enemy enemyPrefab, EnemyData enemyData)
    {
        if (enemyPrefab == null )
        {
            return null;
        }
        Enemy currentEnemy = _factoryEnemy.CreateEntity(enemyPrefab, PositionSpawnEnemy());
        currentEnemy.Initiated(enemyData, _bulletFactoryEnemy, _bulletEnemyDataBase, _uiManager);
        _factoryEnemy.Active(currentEnemy);
        return currentEnemy;
     }

    private Vector2 PositionSpawnEnemy()
    {
        float x = Random.Range(ScreenBoudaries.MinX, ScreenBoudaries.MaxX);
        float y = ScreenBoudaries.MaxY + _spawnOffet;
        return new Vector2(x, y); 
    }
    public void ResetEnemy() => _factoryEnemy.ResetEnemy();

}
