using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Khoảng cách ngoài camera để coi là đã rời màn hình
    private const float DespawnMargin = 1f;
    // Thời gian sống tối đa nếu enemy không bao giờ vào được màn hình
    private const float MaxLifeTime = 30f;

    private Enemy _enemy;
    private float _speed;
    private ScreenBoudaries _screenBoudaries;
    private MoveBase _scriptedMove;

    private bool _hasEnteredView;
    private float _lifeTimer;

    public void Init(Enemy enemy, float speed, ScreenBoudaries screenBoudaries)
    {
        _enemy = enemy;
        _speed = speed;
        _screenBoudaries = screenBoudaries;
        _scriptedMove = null;

        // Reset khi lấy lại từ pool
        _hasEnteredView = false;
        _lifeTimer = 0f;
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
                _scriptedMove = null;
        }
        else
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }

        CheckDespawn();
    }

    private void CheckDespawn()
    {
        _lifeTimer += Time.deltaTime;

        bool outside = _screenBoudaries.IsOutsideView(transform.position, DespawnMargin);

        if (!outside) _hasEnteredView = true;

        bool leftScreen = _hasEnteredView && outside;
        bool timeout = _lifeTimer > MaxLifeTime;

        if (leftScreen || timeout)
        {
            _scriptedMove = null;
            _enemy.Despawn();
        }
    }
}