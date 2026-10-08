using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy _enemy;
    private float _speed;
    private ScreenBoudaries _screenBoudaries;
    public void Init(Enemy enemy, float speed,  ScreenBoudaries screenBoudaries)
    {
        _enemy = enemy;
        _speed = speed;
        _screenBoudaries = screenBoudaries;
    }
    private void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        if ( transform.position.y < _screenBoudaries.MinY - 1f)
        {
            _enemy.Despawn();
            
        }

    }

}
