using UnityEngine;

public class BulletInteractWorld
{
    public static float DistanceTarget(Vector2 startPos, Vector2 targetPos )
    {
        return (startPos - targetPos).sqrMagnitude;
    }
    public Damageable FindNearest(Vector2 posObject, float range, LayerMask layerTarget)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(posObject, range, layerTarget);
        Damageable nearestObject = null;
        float min = float.MaxValue;
        foreach (Collider2D target in hits)
        {
            if (!target.TryGetComponent(out Damageable targetDamage))
            {
                continue;
            }
            float distance = DistanceTarget(targetDamage.Position, posObject);
            if (distance < min)
            {
                min = distance;
                nearestObject = targetDamage;
            }
        }
        return nearestObject;
    }
    public void AttackArea(Vector2 posObject, float radius, int damage, LayerMask layerTarget)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(posObject, radius, layerTarget);
        foreach (Collider2D target in hits)
        {
            if (!target.TryGetComponent(out Damageable targetDamage))
            {
                continue;
            }
            targetDamage.TakeDamage(damage);
        }
    }
}
