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
        StartCoroutine(DelayShoot());
    }
    private IEnumerator DelayShoot()
    {
        while (true)
        {
            if (CanShoot())
            {
                _bulletFactory.CreateBullet(_bulletDataBase, playerPoint.position, Vector2.up);
                yield return new WaitForSeconds(1f);
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
            if (list[i].transform.position.y <= 4.5f)
            {
                return true;
            }
            
        }
        return false;
    }
}
