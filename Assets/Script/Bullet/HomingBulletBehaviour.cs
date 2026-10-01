using Unity.Multiplayer.PlayMode;
using UnityEngine;
using UnityEngine.Rendering;

public class HomingBulletBehaviour : BulletBehaviour
{

    private float _rotationSpeed;
    private float _searchRange;

    public HomingBulletBehaviour(float rotationSpeed, float searchRange)
    {
        _rotationSpeed = rotationSpeed;
        _searchRange = searchRange;
    }
    public override void Move(BulletInformation bulletInfor)
    {
        if (bulletInfor.currentTarget == null || !bulletInfor.currentTarget.gameObject.activeInHierarchy)
        {
            bulletInfor.bulletAttackWorld.FindNearest(bulletInfor.currentPos, _searchRange, bulletInfor.bullet.TargetLayer);
        }
        if (bulletInfor.currentTarget != null)
        {
            Vector2 wantPos = (bulletInfor.currentTarget.Position - bulletInfor.currentPos).normalized;
            bulletInfor.direction = Vector2.Lerp(bulletInfor.direction, wantPos, bulletInfor.deltaTime* _rotationSpeed).normalized;
        }
        base.Move(bulletInfor);
    }
    public override bool OnHit(BulletInformation bulletInfor)
    {
        bulletInfor.currentTarget.TakeDamage(bulletInfor.bullet.Damage);
        return true;
    }
}
