using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float _speedMove = 2f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 movement;
    //private ScreenBoudaries _screenBoudaries;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0)
        {
            movement = new Vector2(inputX, 0);
        }
        else if (inputY != 0)
        {

            movement = new Vector2(0, inputY);
        }
        else
        {
            movement = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        Vector2 newPos = _rigidbody2D.position + movement * _speedMove * Time.fixedDeltaTime;
        newPos.x = Mathf.Clamp(newPos.x, ScreenBoudaries.MinX, ScreenBoudaries.MaxX);
        newPos.y = Mathf.Clamp(newPos.y, ScreenBoudaries.MinY, ScreenBoudaries.MaxY);
        _rigidbody2D.MovePosition(newPos);
    }
}
