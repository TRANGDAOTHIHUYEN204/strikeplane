using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    [SerializeField] private float _speedMove = 10f;
    private Rigidbody2D _rigidbody2D;
    private float maxY => ScreenBoudaries.MaxY + 1f;
    private Vector2 _startPos;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPos = _rigidbody2D.position;
    }
    private void FixedUpdate()
    {
        if (_rigidbody2D.position.y > maxY)
        {
            return;
        }
        Vector2 targetMove = new Vector2(_startPos.x, maxY);
        Vector2 newPos = Vector2.MoveTowards(_rigidbody2D.position, targetMove, _speedMove * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPos);
    }
}
