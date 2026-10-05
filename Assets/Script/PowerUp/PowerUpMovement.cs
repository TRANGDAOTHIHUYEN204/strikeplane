using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    private PowerUpPlayer _powerup;
    private float _speed;

    public void Init(PowerUpPlayer powerup, float speed)
    {
        _powerup = powerup;
        _speed = speed;
    }
    private void Update()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        if (transform.position.y < ScreenBoudaries.MinY - 1f)
        {
            _powerup.Despawn();

        }

    }

}
