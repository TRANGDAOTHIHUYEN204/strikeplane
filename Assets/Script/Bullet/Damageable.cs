using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public abstract void TakeDamage(int damage);
    public Vector2 Position => transform.position;
}
