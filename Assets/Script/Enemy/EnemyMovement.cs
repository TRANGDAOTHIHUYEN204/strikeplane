using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy _enemy;
    private float _speed;
    private ScreenBoudaries _screenBoudaries;
    private MoveBase _scriptedMove;

    public void Init(Enemy enemy, float speed, ScreenBoudaries screenBoudaries)
    {
        _enemy = enemy;
        _speed = speed;
        _screenBoudaries = screenBoudaries;
        _scriptedMove = null;
    }

    public void SetMove(MoveBase move, float duration)
    {
        _scriptedMove = move;
        _scriptedMove?.Begin(transform, duration);
    }

    private void Update()
    {
        if (_scriptedMove != null)
        {
            _scriptedMove.Tick(Time.deltaTime);
            if (_scriptedMove.IsFinished)
                _scriptedMove = null; // hết kịch bản -> rơi xuống như cũ
        }
        else
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }

        if (transform.position.y < _screenBoudaries.MinY - 1f)
        {
            _scriptedMove = null;
            _enemy.Despawn();
        }
    }
}