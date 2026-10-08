using System;
using System.Security;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private ScreenBoudaries screenBoudaries;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movement;

    private Vector2 _targetPos;
    private bool _hasTarget;
    private Camera _cam;
    private Vector2 _startPos;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _startPos = transform.position;
        
    }

    private void Start()
    {
        if (speedMove == 0f)
        {
            Debug.LogWarning("Speed Move chưa được gán");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (LoseManager.isGameOver) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        bool hasKeyInput = Mathf.Abs(inputX) > 0.01f || Mathf.Abs(inputY) > 0.01f;
        
        if (hasKeyInput)
        {
            _hasTarget = false;
            _movement = new Vector2(inputX, inputY).normalized;
        }
        else if (Input.GetMouseButton(0))
        {
            _hasTarget = true;
            Vector3 mouseWorld = _cam.ScreenToWorldPoint(Input.mousePosition);
            _targetPos = new Vector2(mouseWorld.x, mouseWorld.y);
        }
        
        if (_hasTarget)
        {
            Vector2 direction = _targetPos - (Vector2)transform.position;

            if (direction.sqrMagnitude < 0.001f)
            {
                _movement = Vector2.zero;
                _hasTarget = false;
            }
            else
            {
                _movement = direction.normalized;
            }
        }
        else if (!hasKeyInput)
        {
            _movement = Vector2.zero;
        }
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_movement == Vector2.zero) return;

        Vector3 step = (Vector3)_movement * speedMove * Time.deltaTime;

        if (_hasTarget)
        {
            Vector2 toTarget = _targetPos - (Vector2)transform.position;
            if (step.sqrMagnitude > toTarget.sqrMagnitude)
                step = toTarget;
        }

        transform.Translate(step, Space.World);
        
        if (screenBoudaries != null)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, screenBoudaries.MinX, screenBoudaries.MaxX);
            pos.y = Mathf.Clamp(pos.y, screenBoudaries.MinY, screenBoudaries.MaxY);
            transform.position = pos;
        }
    }
    public void ResetMovement()
    {
        _movement = Vector2.zero;
        _hasTarget = false;
        _rigidbody2D.position = _startPos;
        transform.position = _startPos;
    }
}