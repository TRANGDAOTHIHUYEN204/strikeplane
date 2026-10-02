using Unity.VisualScripting;
using UnityEngine;

public class PiercingBulletBehaviour : BulletBehaviour
{
    private int _hitCount;
    public int HitCount => _hitCount;
    public PiercingBulletBehaviour()
    {
        _hitCount = 0;

    }
    public override bool OnHit(BulletInformation bulletInfor)
    {
        bulletInfor.currentTarget.TakeDamage(bulletInfor.damage);
        _hitCount++;
        return false;
    }
}
