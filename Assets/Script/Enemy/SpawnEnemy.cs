using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private FactoryEnemy _factoryEnemy;
    public IReadOnlyList<Enemy> _enemyActive => _factoryEnemy.EnemyActive;
    private const float _spawnOffet = 2f;
    
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
        currentEnemy.Initiated(enemyData);
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
