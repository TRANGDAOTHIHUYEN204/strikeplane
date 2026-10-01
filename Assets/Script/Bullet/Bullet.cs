using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private BulletDataBase _bulletDataBase;
    public int Damage => _bulletDataBase.Damage;
    private BulletBehaviour _bulletBehaviour;
    [SerializeField] private Transform player ;
    [SerializeField] private Enemy _enemy;
    /*
    public Vector2 BulletPosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    */
    public BulletInformation _bullet;
    public float deltaTime;
    public LayerMask TargetLayer => _bulletDataBase.LayerTarget;

    public void Init(BulletDataBase bulletDataBase)
    {
        _bulletDataBase = bulletDataBase;
        _bulletBehaviour = CreateBulletBehaviour();
    }
    private void Update()
    {
        //if (BulletInteractWorld.DistanceTarget(_player.Position, ))
        _bulletBehaviour.Move(_bullet);
    }
    private BulletBehaviour CreateBulletBehaviour()
    {
        return new NormalBulletBehaviour();
    }
}
