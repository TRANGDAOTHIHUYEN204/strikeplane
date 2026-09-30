
using UnityEngine;

public interface IFactoryEntity<T> where T : Component
{
    public T CreateEntity(T prefabObject, Vector2 posSpawn ) ; 
}
