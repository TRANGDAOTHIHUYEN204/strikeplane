using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : IFactoryEntity<Enemy>
{
    private Stack<Enemy> _enemyPool = new Stack<Enemy>();
    private List<Enemy> _enemyActive = new List<Enemy>();

    public IReadOnlyList<Enemy> EnemyActive => _enemyActive;
    public Enemy CreateEntity(Enemy enemyPrefab, Vector2 posSpawn)
    {
        Enemy enemy;
        if (_enemyPool.Count > 0)
        {
            enemy = _enemyPool.Pop();
            enemy.transform.SetLocalPositionAndRotation(posSpawn, Quaternion.identity);

        }
        else
        {
            enemy = Enemy.Instantiate(enemyPrefab, posSpawn, Quaternion.identity);
            enemy.SetPool(this);
        }
        return enemy;
    }
    public void Active(Enemy enemy)
    {
        _enemyActive.Add(enemy);
    }
    public void Release(Enemy enemy)
    {
        if (!_enemyActive.Remove(enemy)) return;
        enemy.gameObject.SetActive(false);
        _enemyPool.Push(enemy);
    }
    public void ResetEnemy()
    {
        while (_enemyActive.Count > 0)
        {
            Enemy enemyActive = _enemyActive[_enemyActive.Count - 1];
            Release(enemyActive);
        }
    }
}
