using UnityEngine;
public class Bullet : MonoBehaviour
{

    private BulletDataBase _bulletDataBase;
    public int Damage => _bulletDataBase.Damage;
    private BulletBehaviour _bulletBehaviour;

    private BulletInformation _infor;
    private BulletInteractWorld _bulletInteractWorld;
    private BulletFactory _bulletFactory;
    public float timeLifeCycle;
    public LayerMask TargetLayer => _bulletDataBase.LayerTarget;

    public bool HasAddLine => _bulletDataBase.AddLine;
    public void Init(BulletDataBase bulletDataBase, BulletFactory bulletFactory, Vector2 pos, Vector2 direction)
    {
        _bulletFactory = bulletFactory;
        _bulletInteractWorld = new BulletInteractWorld();
        _infor = new BulletInformation();
        _bulletDataBase = bulletDataBase;

        _infor.bullet = this;
        _infor.currentPos = pos;
        _infor.direction = direction;
        _infor.layerTarget = _bulletDataBase.LayerTarget;
        _infor.direction = direction;
        _infor.bulletAttackWorld = _bulletInteractWorld;
        _infor.currentTarget = null;
        _infor.speed = _bulletDataBase.SpeedMove;
        _infor.damage = _bulletDataBase.Damage;
        transform.position = pos;
        timeLifeCycle = bulletDataBase.LifeTime;
        _bulletBehaviour = CreateBulletBehaviour(bulletDataBase.TypeBullet);
    }
    private void Update()
    {
        if (_bulletBehaviour == null) return;
        _infor.deltaTime = Time.deltaTime;
        _bulletBehaviour.Move(_infor);
        transform.position = _infor.currentPos;
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
            _infor.currentTarget = target;
            if (_bulletBehaviour.OnHit(_infor))
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
