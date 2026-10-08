using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletDataBase _bulletDataBase;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    private int _currentLine = 1;
    private int _maxLine = 3;
    private IReadOnlyList<Enemy> _enemyActive => _spawnEnemy._enemyActive;
    private Coroutine _shootCoroutine;
    private Coroutine _powerUpCoroutine;
    [SerializeField] private BulletFactory bulletFactory;
    
    
    private void AddShootLine(int countLine)
    {
        float spacing = 0.5f;

        for (int i = 0; i < countLine; i++)
        {
            float offset = (i - (countLine - 1) / 2f) * spacing;

            Vector2 spawnPosition = (Vector2)playerPoint.position + Vector2.right * offset;

            bulletFactory.CreateBullet( _bulletDataBase,spawnPosition,Vector2.up);
        }
    }
    public void ResetBulletPlayer() => bulletFactory.ResetBullet();
    void OnEnable()
    {
        PowerUpPlayer.OnPowerUpCollected += ChangeBulletDataBase;
        _shootCoroutine = StartCoroutine(DelayShoot());
    }
    public int AddLine()
    {
        if (_currentLine >= _maxLine)
        {
            return _maxLine;

        }
         return ++_currentLine;
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
                AddShootLine(_currentLine);
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
        if (bulletDataBase.AddLine)
            AddLine();
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
        _currentLine = 1;
        _powerUpCoroutine = null;
    }
}
