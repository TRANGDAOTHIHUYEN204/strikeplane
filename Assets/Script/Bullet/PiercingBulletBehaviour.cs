using UnityEngine;

public class PiercingBulletBehaviour : BulletBehaviour
{
    private int _countPiercing;
    public PiercingBulletBehaviour(int count)
    {
        count = _countPiercing;
    }
    public override bool OnHit(BulletInformation bulletInfor)
    {
        bulletInfor.currentTarget.TakeDamage(bulletInfor.bullet.Damage);
        return _countPiercing-- <= 0;
    }
}
