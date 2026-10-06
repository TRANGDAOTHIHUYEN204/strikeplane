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
    private Coroutine _shootCoroutine;
    private Coroutine _powerUpCoroutine;
    

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
    public void ResetBulletPlayer() => _bulletFactory.ResetBullet();
    void OnEnable()
    {
        PowerUpPlayer.OnPowerUpCollected += ChangeBulletDataBase;
        _shootCoroutine = StartCoroutine(DelayShoot());
    }
    private IEnumerator DelayShoot()
    {
        while (true)
        {
            if (LoseManager.isGameOver)
            {
                yield return null;
                continue;
            }
            
                if (CanShoot())
                {
                    _bulletFactory.CreateBullet(_bulletDataBase, playerPoint.position, Vector2.up);
                    yield return new WaitForSeconds(0.3f);
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
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
            _shootCoroutine = null;
        }

        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
            _powerUpCoroutine = null;
        }
    }
    public void ChangeBulletDataBase(BulletDataBase bulletDataBase, float duration)
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);

        }
        _powerUpCoroutine = StartCoroutine(PowerUpRoutine(bulletDataBase, duration));
    }
    private IEnumerator PowerUpRoutine(BulletDataBase powerUpData, float duration)
    {
        BulletDataBase originalBulletData = _bulletDataBase;
        _bulletDataBase = powerUpData;
        yield return new WaitForSeconds(duration);
        _bulletDataBase = originalBulletData;
        _powerUpCoroutine = null;
    }
}
