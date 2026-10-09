using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletDataBase _bulletDataBase;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private BulletFactory bulletFactory;

    private BulletDataBase _defaultBulletData;
    private const int DefaultLine = 1;
    private int _currentLine = DefaultLine;
    private int _maxLine = 3;

    private IReadOnlyList<Enemy> _enemyActive => _spawnEnemy._enemyActive;
    private Coroutine _shootCoroutine;
    private Coroutine _powerUpCoroutine;

    private void Awake()
    {

        _defaultBulletData = _bulletDataBase;
    }

    private void OnEnable()
    {
        ResetToDefault();
        PowerUpPlayer.OnPowerUpCollected += ChangeBulletDataBase;
        _shootCoroutine = StartCoroutine(DelayShoot());
    }

    private void OnDisable()
    {
        PowerUpPlayer.OnPowerUpCollected -= ChangeBulletDataBase;
        StopAllCoroutines();
        _shootCoroutine = null;
        _powerUpCoroutine = null;
    }


    public void ResetToDefault()
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
            _powerUpCoroutine = null;
        }

        _bulletDataBase = _defaultBulletData;
        _currentLine = DefaultLine;
    }

    public void ResetBulletPlayer()
    {
        ResetToDefault();
        bulletFactory.ResetBullet();
    }

    private void AddShootLine(int countLine)
    {
        float spacing = 0.5f;
        
        for (int i = 0; i < countLine; i++)
        {
            float offset = (i - (countLine - 1) / 2f) * spacing;
            Vector2 spawnPosition = (Vector2)playerPoint.position + Vector2.right * offset;
            bulletFactory.CreateBullet(_bulletDataBase, spawnPosition, Vector2.up);
        }
    }

    public int AddLine()
    {
        if (_currentLine >= _maxLine) return _maxLine;
        return ++_currentLine;
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

            AddShootLine(_currentLine);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private bool CanShoot()
    {
        var list = _enemyActive;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position.y <= 4f)
                return true;
        }
        return false;
    }

    public void ChangeBulletDataBase(BulletDataBase bulletDataBase, float duration)
    {
        if (bulletDataBase.TypeBullet == BulletType.RocketBullet)
        {
            FireSingleRocket(bulletDataBase);
            return;
        }

        if (bulletDataBase.AddLine)
            AddLine();

        if (_powerUpCoroutine != null)
            StopCoroutine(_powerUpCoroutine);

        _powerUpCoroutine = StartCoroutine(PowerUpRoutine(bulletDataBase, duration));
    }

    private void FireSingleRocket(BulletDataBase rocketData)
    {
        if (LoseManager.isGameOver) return;
        bulletFactory.CreateBullet(rocketData, playerPoint.position, Vector2.up);
    }

    private IEnumerator PowerUpRoutine(BulletDataBase powerUpData, float duration)
    {
        _bulletDataBase = powerUpData;
        yield return new WaitForSeconds(duration);
        _bulletDataBase = _defaultBulletData;
        _currentLine = DefaultLine;
        _powerUpCoroutine = null;
    }
}