using UnityEngine;

public class HeartFormationMove : MoveBase
{
    private readonly Vector3 _target;
    private readonly float _travelRatio;

    public HeartFormationMove(Vector2 target, float travelRatio = 0.4f)
    {
        _target = new Vector3(target.x, target.y, 0f);
        _travelRatio = Mathf.Clamp(travelRatio, 0.01f, 1f);
    }

    protected override void Move(float progress)
    {
        if (progress < _travelRatio)
        {
            float t = Mathf.SmoothStep(0f, 1f, progress / _travelRatio);
            _targetTransform.position = Vector3.Lerp(_startPosition, _target, t);
        }
        else
        {
            _targetTransform.position = _target; 
        }
    }
}