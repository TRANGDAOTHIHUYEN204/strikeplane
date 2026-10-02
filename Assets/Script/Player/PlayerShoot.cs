using UnityEngine;
using System.Collections;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private BulletDataBase _bulletDataBase;
    [SerializeField] private Transform playerPoint;

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

    void Update()
    {

    }
    private IEnumerator DelayShoot()
    {
        
        _bulletFactory.CreateBullet(_bulletDataBase, playerPoint.position, Vector2.up);
        yield return new WaitForSeconds(0.1f);

    }
}
