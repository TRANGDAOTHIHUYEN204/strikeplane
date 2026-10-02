using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy _enemy;
    private float _speed;

    public void Init(Enemy enemy, float speed)
    {
        _enemy = enemy;
        _speed = speed;
    }
    private void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        if ( transform.position.y < ScreenBoudaries.MinY - 1f)
        {
            _enemy.Despawn();
            
        }

    }

}
