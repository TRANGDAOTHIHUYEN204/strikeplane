using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab ;
    private Stack<Bullet> _bulletPool = new Stack<Bullet>();
    private List<Bullet> _bulletActive = new List<Bullet>();
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
            bullet = Instantiate(bulletPrefab);
        }
        bullet.Init(bulletDatabase,this, pos, direction);
        _bulletActive.Add(bullet);
        return bullet;
    }
    public void ResetBullet()
    {
        while (_bulletActive.Count > 0)
        {
            Bullet bulletActive = _bulletActive[_bulletActive.Count - 1];
            ReturnBullet(bulletActive);
        }
    }
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletActive.Remove(bullet);
        _bulletPool.Push(bullet);
    }
}
