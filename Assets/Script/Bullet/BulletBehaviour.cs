
using UnityEngine;


public abstract class BulletBehaviour
{
    private float _speedMove = 10f;
    /*
    public virtual void Move(BulletInformation bulletInfor)
    {
        if (_rigidbody2D.position.y > maxY)
        {
            return;
        }
        Vector2 targetMove = new Vector2(_startPos.x, maxY);
        Vector2 newPos = Vector2.MoveTowards(_rigidbody2D.position, targetMove, _speedMove * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPos);
    }
    */

    public virtual void Move(BulletInformation bulletInformation)
    {
        bulletInformation.currentPos += Vector2.MoveTowards(bulletInformation.currentPos, bulletInformation.currentTarget.Position, _speedMove * Time.deltaTime); 
    }
    public abstract bool OnHit(BulletInformation bulletInfor);
}
