using UnityEngine;
public class Bullet : MonoBehaviour
{

    private BulletDataBase _bulletDataBase;
    public int Damage => _bulletDataBase.Damage;
    private BulletBehaviour _bulletBehaviour;

    private BulletInformation infor;
    private BulletInteractWorld _bulletInteractWorld;
    private BulletFactory _bulletFactory;

    public float timeLifeCycle;
    public LayerMask TargetLayer => _bulletDataBase.LayerTarget;

    public void Init(BulletDataBase bulletDataBase, BulletFactory bulletFactory, Vector2 pos, Vector2 direction)
    {
        _bulletFactory = bulletFactory;
        _bulletInteractWorld = new BulletInteractWorld();
        infor = new BulletInformation();
        _bulletDataBase = bulletDataBase;

        infor.bullet = this;
        infor.currentPos = pos;
        infor.direction = direction;
        infor.layerTarget = _bulletDataBase.LayerTarget;
        infor.direction = direction;
        infor.bulletAttackWorld = _bulletInteractWorld;
        infor.currentTarget = null;
        infor.speed = _bulletDataBase.SpeedMove;
        infor.damage = _bulletDataBase.Damage;

        transform.position = pos;
        timeLifeCycle = bulletDataBase.LifeTime;
        _bulletBehaviour = CreateBulletBehaviour(bulletDataBase.TypeBullet);
    }
    private void Update()
    {
        if (_bulletBehaviour == null) return;
        infor.deltaTime = Time.deltaTime;
        _bulletBehaviour.Move(infor);
        transform.position = infor.currentPos;
        timeLifeCycle -= Time.deltaTime;
        if (timeLifeCycle <= 0f)
        {
            Despawn();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D colliderTarget)
    {
        if (_bulletBehaviour == null || !gameObject.activeSelf) return;
        if (((1<< colliderTarget.gameObject.layer) & _bulletDataBase.LayerTarget.value) == 0) return;
        if (colliderTarget.TryGetComponent(out Damageable target))
        {
            infor.currentTarget = target;
            if (_bulletBehaviour.OnHit(infor))
            {
                Despawn();
            }

        }
    }
    private void Despawn()
    {
        _bulletFactory.ReturnBullet(this);
    }

    private BulletBehaviour CreateBulletBehaviour(BulletType bulletType)
    {
        switch (bulletType)
        {
            case (BulletType.PiercingBullet):
                return new PiercingBulletBehaviour();
            case (BulletType.HomingBullet):
                return new HomingBulletBehaviour(8f, 5f);
            case (BulletType.Explosive):
                return new ExplosiveBulletBehaviour(1);
            default:
                return new NormalBulletBehaviour();
        }
    }
}
