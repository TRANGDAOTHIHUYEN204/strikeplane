using UnityEngine;

public class RocketBulletBehaviour : BulletBehaviour
{
    private int _radius;
    private readonly Vector2 _fallbackPoint;   

    private const float WaveFrequency = 10f;
    private const float WaveAmplitude = 4f;
    private const float FadeDistance  = 2f;
    private const float ReachDistance = 0.5f;

    private float _elapsed;
    
    public RocketBulletBehaviour(int radius, float maxX, float aimY)
    {
        _radius = radius;
        _fallbackPoint = new Vector2(maxX * 0.5f, aimY);   
    }

    public override void Move(BulletInformation infor)
    {
        _elapsed += infor.deltaTime;

        Vector2 forward = infor.direction;
        float fade = 1f;

        if (infor.currentTarget != null)
        {
            Vector2 toTarget = (Vector2)infor.currentTarget.transform.position - infor.currentPos;
            float dist = toTarget.magnitude;

            if (dist > 0.0001f)
                forward = toTarget / dist;

            fade = Mathf.Clamp01(dist / FadeDistance);
        }
        else
        {
            Vector2 toMid = _fallbackPoint - infor.currentPos;
            if (toMid.magnitude > ReachDistance)  
                forward = toMid.normalized;
        }

        Vector2 side = new Vector2(-forward.y, forward.x);

        float sway = Mathf.Cos(_elapsed * WaveFrequency) * WaveAmplitude * fade;
        Vector2 velocity = forward * infor.speed + side * sway;

        infor.currentPos += velocity * infor.deltaTime;
        infor.bullet.transform.position = infor.currentPos;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        infor.bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public override bool OnHit(BulletInformation bulletInfor)
    {
        bulletInfor.bulletAttackWorld.AttackArea(bulletInfor.currentPos, _radius, bulletInfor.damage, bulletInfor.bullet.TargetLayer);
        return true;
    }
}