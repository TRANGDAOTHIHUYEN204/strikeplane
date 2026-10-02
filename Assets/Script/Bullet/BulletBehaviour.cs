
using UnityEngine;


public abstract class BulletBehaviour
{
    public virtual void Move(BulletInformation infor)
    {
        infor.currentPos += infor.direction * infor.speed * infor.deltaTime; 
    }
    public abstract bool OnHit(BulletInformation infor);
}
