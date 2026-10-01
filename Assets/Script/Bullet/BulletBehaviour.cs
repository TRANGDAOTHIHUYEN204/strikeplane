
using UnityEngine;


public abstract class BulletBehaviour
{
    private float _speedMove = 10f;
    private Rigidbody2D _rigidbody2D;
    private float maxY => ScreenBoudaries.MaxY + 1f;
    private Vector2 _startPos;
    public void Begin(BulletInformation bulletInfor)
    {
        bulletInfor.currentPos = _startPos;
        bulletInfor.speed = 0;
        bulletInfor.direction = Vector2.zero;
    }
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
    public abstract bool OnHit(BulletInformation bulletInfor);
}
