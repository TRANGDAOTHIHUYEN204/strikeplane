using UnityEngine;

public class HeartMove : MoveBase
{
    private readonly float _scale;

    public HeartMove(float scale = 0.15f) => _scale = scale;

    protected override void Move(float progress)
    {
        float angle = progress * Mathf.PI * 2f;

        float s = Mathf.Sin(angle);
        float x = 16f * s * s * s;
        float y = 13f * Mathf.Cos(angle) - 5f * Mathf.Cos(2f * angle) - 2f * Mathf.Cos(3f * angle) - Mathf.Cos(4f * angle) - 5f;

        _targetTransform.position = _startPosition + new Vector3(x, y, 0f) * _scale;
    }
}