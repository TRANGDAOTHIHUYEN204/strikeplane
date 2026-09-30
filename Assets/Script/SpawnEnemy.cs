using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private IFactoryEntity<Enemy> _factoryEntity;
    private const float _spawnOffet = 2f;
    
    private void Awake()
    {
        _factoryEntity = new FactoryEnemy();
    }

    public Enemy SpawnEnemyWave(Enemy enemyPrefab, EnemyData enemyData)
    {
        if (enemyPrefab == null )
        {
            return null;
        }
        Enemy currentEnemy = _factoryEntity.CreateEntity(enemyPrefab, PositionSpawnEnemy());
        currentEnemy.Initiated(enemyData);
        return currentEnemy;
     }
    private Vector2 PositionSpawnEnemy()
    {
        float x = Random.Range(ScreenBoudaries.MinX, ScreenBoudaries.MaxX);
        float y = ScreenBoudaries.MaxY + _spawnOffet;
        return new Vector2(x, y);
    }

}
