using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private BulletDataBase _bulletDataBase;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    private IReadOnlyList<Enemy> _enemyActive => _spawnEnemy._enemyActive;
    private Coroutine _coroutine;

    private void Awake()
    {
        if (_bulletDataBase == null)
        {
            Debug.LogError("BulletDataBase is missed in PlayerShoot");
            enabled = false;
            return;
        }
        if (_bulletFactory == null)
        {
            Debug.LogError("BulletFactory is missed in PlayerShoot");
            enabled = false;
            return;
        }

    }

    void OnEnable()
    {
        PowerUpPlayer.OnPowerUpCollected += ChangeBulletDataBase;
        StartCoroutine(DelayShoot());
    }
    private IEnumerator DelayShoot()
    {
        while (!LoseManager.isGameOver)
        {
            if (CanShoot())
            {
                _bulletFactory.CreateBullet(_bulletDataBase, playerPoint.position, Vector2.up);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return null;
            }
        }

    }
    private bool CanShoot()
    {
        var list = _enemyActive;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position.y <= 4f)
            {
                return true;
            }
            
        }
        return false;
    }

    private void OnDisable()
    {
        PowerUpPlayer.OnPowerUpCollected -= ChangeBulletDataBase;
    }
    public void ChangeBulletDataBase(BulletDataBase bulletDataBase, float duration)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

        }
        _coroutine = StartCoroutine(PowerUpRoutine(bulletDataBase, duration));
    }
    private IEnumerator PowerUpRoutine(BulletDataBase powerUpData, float duration)
    {
        BulletDataBase originalBulletData = _bulletDataBase;
        _bulletDataBase = powerUpData;
        yield return new WaitForSeconds(duration);
        _bulletDataBase = originalBulletData;
        _coroutine = null;
    }
}
