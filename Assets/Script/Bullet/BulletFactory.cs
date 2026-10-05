using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab ;
    private Stack<Bullet> _bulletPool = new Stack<Bullet>();
    public Bullet CreateBullet(BulletDataBase bulletDatabase, Vector2 pos, Vector2 direction)
    {
        Bullet bullet;
        if (_bulletPool.Count > 0)
        {
            bullet = _bulletPool.Pop();
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = Instantiate(_bulletPrefab);
        }
        bullet.Init(bulletDatabase,this, pos, direction);

        return bullet;
    }
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletPool.Push(bullet);
    }
}
