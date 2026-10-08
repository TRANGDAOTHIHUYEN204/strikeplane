using UnityEngine;

public class ScreenBoudaries : MonoBehaviour
{

    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private Vector2 padding = new Vector2(1.5f, 1.5f);
    public float MinX { get; private set; }
    public float MaxX { get; private set; }
    public float MinY { get; private set; }
    public float MaxY { get; private set; }

    private void Awake() => UpdateBoundaries();

    public void UpdateBoundaries()
    {
        if (backgroundSprite == null) return;

        Bounds b = backgroundSprite.bounds;
        MinX = b.min.x + padding.x;
        MaxX = b.max.x - padding.x;
        MinY = b.min.y + padding.y;
        MaxY = b.max.y - padding.y;
    }
    
    public Vector2 Clamp(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, MinX, MaxX);
        pos.y = Mathf.Clamp(pos.y, MinY, MaxY);
        return pos;
    }

    public bool IsInside(Vector2 pos)
    {
        return pos.x >= MinX && pos.x <= MaxX && pos.y >= MinY && pos.y <= MaxY;
    }
}