using UnityEngine;

public class ScreenBoudaries : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 padding = new Vector2(1.5f, 1.5f);

    [SerializeField] private bool updateEveryFrame = false;

    public float MinX { get; private set; }
    public float MaxX { get; private set; }
    public float MinY { get; private set; }
    public float MaxY { get; private set; }

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
        UpdateBoundaries();
    }

    private void LateUpdate()
    {
        if (updateEveryFrame) UpdateBoundaries();
    }

    public void UpdateBoundaries()
    {
        if (cam == null) return;

        float distance = Mathf.Abs(cam.transform.position.z);

        Vector3 bottomLeft  = cam.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 topRight    = cam.ViewportToWorldPoint(new Vector3(1f, 1f, distance));
        MinX = bottomLeft.x + padding.x;
        MaxX = topRight.x   - padding.x;
        MinY = bottomLeft.y + padding.y;
        MaxY = topRight.y   - padding.y;
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (cam == null) return;
        UpdateBoundaries();

        Vector3 bl = new Vector3(MinX, MinY, 0);
        Vector3 br = new Vector3(MaxX, MinY, 0);
        Vector3 tr = new Vector3(MaxX, MaxY, 0);
        Vector3 tl = new Vector3(MinX, MaxY, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(bl, br);
        Gizmos.DrawLine(br, tr);
        Gizmos.DrawLine(tr, tl);
        Gizmos.DrawLine(tl, bl);
    }
#endif
}