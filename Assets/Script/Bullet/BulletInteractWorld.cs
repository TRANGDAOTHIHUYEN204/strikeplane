using System.Linq.Expressions;
using UnityEngine;

public class BulletInteractWorld
{
    public Damageable FindNearest(Vector2 posObject, float range, LayerMask layerTarget)
    {

        Damageable nearestObject = null;
        float min = float.MaxValue;
        foreach (var target in Physics2D.OverlapCircleAll(posObject, range, layerTarget))
        {
            if (!target.TryGetComponent(out Damageable targetDamage))
                continue;
            float distance = (targetDamage.Position - posObject).sqrMagnitude;
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
        foreach (var target in Physics2D.OverlapCircleAll(posObject, radius, layerTarget))
        {
            if (!target.TryGetComponent(out Damageable targetDamage))
                targetDamage.TakeDamage(damage);
        }
    }
}
