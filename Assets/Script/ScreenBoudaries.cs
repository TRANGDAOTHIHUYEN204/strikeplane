using UnityEngine;

public class ScreenBoudaries : MonoBehaviour
{
    public static float MinX { get; private set; }
    public static float MaxX { get; private set; }
    public static float MinY { get; private set; }
    public static float MaxY { get; private set; }
    [SerializeField] private float padding = 1f;

    private void Awake()
    {
        Camera camera = Camera.main;
        float camHeight = camera.orthographicSize;
        float camWidth = camHeight * camera.aspect;
        Vector2 camPos = camera.transform.position;

        MinX = camPos.x - camWidth + padding;
        MaxX = camPos.x + camWidth - padding;
        MinY = camPos.y - camHeight + padding;
        MaxY = camPos.y + camHeight - padding;
    }
}
