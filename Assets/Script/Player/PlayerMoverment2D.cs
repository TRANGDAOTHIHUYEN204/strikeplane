using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float _speedMove = 2f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 movement;

    private Vector2 targetPos;
    private bool hasTarget;
    private Camera _cam;
    private Vector2 _startPos;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _startPos = transform.position;
    }

    private void Update()
    {
        if (VirtualJoystick.IsHeld)
        {
            hasTarget = false;
            movement = VirtualJoystick.Direction;
            return;
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0))
        {
            hasTarget = true;
            targetPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (inputX != 0)
        {
            hasTarget = false;
            movement = new Vector2(inputX, 0);
        }
        else if (inputY != 0)
        {
            hasTarget = false;
            movement = new Vector2(0, inputY);
        }
        else if (hasTarget)
        {
            Vector2 direction = targetPos - _rigidbody2D.position;
            if (direction.sqrMagnitude < 0.05f)
            {
                movement = Vector2.zero;
                hasTarget = false;
            }
            else
            {
                movement = direction.normalized;
            }
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (LoseManager.isGameOver) return;
        Vector2 step = movement * _speedMove * Time.fixedDeltaTime;

        if (hasTarget && step.magnitude > (targetPos - _rigidbody2D.position).magnitude)
            step = targetPos - _rigidbody2D.position;

        Vector2 newPos = _rigidbody2D.position + step;

        newPos.x = Mathf.Clamp(newPos.x, ScreenBoudaries.MinX, ScreenBoudaries.MaxX);
        newPos.y = Mathf.Clamp(newPos.y, ScreenBoudaries.MinY, ScreenBoudaries.MaxY);
        _rigidbody2D.MovePosition(newPos);
    }
    public void ResetMovement()
    {
        movement = Vector2.zero;
        hasTarget = false;
        _rigidbody2D.position = _startPos;
        transform.position = _startPos;
    }
}