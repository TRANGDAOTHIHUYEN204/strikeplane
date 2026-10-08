using UnityEngine;

public abstract class MoveBase
{
    protected Transform _targetTransform;
    protected Vector3 _startPosition;
    protected float _moveDuration;
    protected float _elapsedTime;

    public bool IsFinished { get; private set; }

    public void Begin(Transform targetTransform, float moveDuration)
    {
        IsFinished = false;
        _elapsedTime = 0f;
        _targetTransform = targetTransform;
        _moveDuration = Mathf.Max(0.0001f, moveDuration);
        _startPosition = targetTransform.position;
        OnBegin();
    }

    public void Tick(float deltaTime)
    {
        if (IsFinished) return;

        _elapsedTime += deltaTime;
        float progress = Mathf.Clamp01(_elapsedTime / _moveDuration);
        Move(progress);

        if (progress >= 1f)
            IsFinished = true;
    }

    protected abstract void Move(float progress);
    protected virtual void OnBegin() { }
}