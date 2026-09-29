using Unity.VisualScripting;
using UnityEngine;

public abstract class FactoryEntity : MonoBehaviour
{
    public abstract GameObject CreateEntity(Vector2 posSpawn); 
}
