using UnityEngine;

public class Bullet : MonoBehaviour
{

    private BulletDataBase _bulletDataBase;
    public int Damage => _bulletDataBase.Damage;
    /*
    public Vector2 BulletPosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    */
                                                                                                                                                                                                                                                                                                    

    public LayerMask TargetLayer => _bulletDataBase.LayerTarget;

    public void Init(BulletDataBase bulletDataBase, BulletBehaviour bulletBehaviour)
    {
        bulletBehaviour = CreateBulletBehaviour();
    }

    private BulletBehaviour CreateBulletBehaviour()
    {
        if (_bulletDataBase.TypeBullet == BulletType.PiercingBullet)
        {
        }
        return new NormalBulletBehaviour();
    }
}
