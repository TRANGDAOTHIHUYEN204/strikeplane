using UnityEngine;

public class ExplosiveBulletBehaviour : BulletBehaviour
{
    private int _radius;
    public ExplosiveBulletBehaviour(int radius)
    {
        _radius = radius;
    }
    public override bool OnHit(BulletInformation bulletInfor)
    {
        bulletInfor.bulletAttackWorld.AttackArea(bulletInfor.currentPos, _radius, bulletInfor.damage , bulletInfor.bullet.TargetLayer);
        return true;
    }
}
