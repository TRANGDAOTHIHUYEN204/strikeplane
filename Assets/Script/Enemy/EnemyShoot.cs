using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private BulletDataBase _bulletDataBase;

    [SerializeField] private float timeSpawnBulletEnemy = 4.5f;
    [SerializeField] private float _shootRange = 4.5f;

    private BulletFactory _bulletFactory;
    private Coroutine _shootCoroutine;

    public void Init(BulletFactory bulletFactory)
    {
        _bulletFactory = bulletFactory;
        StartShooting();
    }

    private void OnEnable()
    {
        if (_bulletFactory != null)
            StartShooting();
    }

    private void OnDisable()
    {
        StopShooting();
    }
    public void StartShooting()
    {
        if (_bulletDataBase == null)
        {
            return;
        }

        StopShooting();
        _shootCoroutine = StartCoroutine(DelayShoot());
    }

    private void StopShooting()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
            _shootCoroutine = null;
        }
    }

    private IEnumerator DelayShoot()
    {
        while (true)
        {
            if (LoseManager.isGameOver || !CanShoot())
            {
                yield return null;
                continue;
            }

            _bulletFactory.CreateBullet(_bulletDataBase, transform.position, Vector2.down);
            yield return new WaitForSeconds(timeSpawnBulletEnemy);
        }
    }

    private bool CanShoot()
    {
        return Physics2D.OverlapCircle(transform.position, _shootRange, _bulletDataBase.LayerTarget) != null;
    }
}