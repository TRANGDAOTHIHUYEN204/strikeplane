using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    private PowerUpPlayer _powerup;
    private float _speed;
    private ScreenBoudaries _screenBoudaries;
    public void Init(PowerUpPlayer powerup, float speed, ScreenBoudaries screenBoudaries)
    {
        _powerup = powerup;
        _speed = speed;
        _screenBoudaries = screenBoudaries;
    }
    private void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        if (transform.position.y < _screenBoudaries.MinY - 1f)
        {
            _powerup.Despawn();

        }

    }

}
