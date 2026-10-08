using UnityEngine;

public class WaveDownMove : MoveBase
{
    private readonly float _amplitude;
    private readonly float _distance;
    private readonly float _waves;

    public WaveDownMove(float amplitude = 2f, float distance = 8f, float waves = 2f)
    {
        _amplitude = amplitude;
        _distance = distance;
        _waves = waves;
    }

    protected override void Move(float progress)
    {
        float x = Mathf.Sin(progress * Mathf.PI * 2f * _waves) * _amplitude;
        float y = -progress * _distance;

        _targetTransform.position = _startPosition + new Vector3(x, y, 0f);
    }
}