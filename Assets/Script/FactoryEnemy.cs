using UnityEngine;

public class FactoryEnemy : FactoryEntity
{
    [SerializeField] private GameObject _enemyPrefab;

    public override GameObject CreateEntity(Vector2 posSpawn)
    {
        return Instantiate(_enemyPrefab, posSpawn, Quaternion.identity);
    }

}
