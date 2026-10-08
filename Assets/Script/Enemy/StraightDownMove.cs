using UnityEngine;

public class StraightDownMove : MoveBase
{
    private readonly float _distance;

    public StraightDownMove(float distance = 8f) => _distance = distance;

    protected override void Move(float progress)
    {
        _targetTransform.position = _startPosition + Vector3.down * (progress * _distance);
    }
}