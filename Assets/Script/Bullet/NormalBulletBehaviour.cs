using UnityEngine;

public class NormalBulletBehaviour : BulletBehaviour
{
    
    public override bool OnHit(BulletInformation bulletInfor)
    {
        bulletInfor.currentTarget.TakeDamage(bulletInfor.damage);
        return true;
    }
}
