using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float radius = 150f;

    public static Vector2 Direction { get; private set; }
    public static bool IsHeld { get; private set; }

    public void OnPointerDown(PointerEventData e)
    {
        IsHeld = true;
        OnDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle( background, e.position, e.pressEventCamera, out Vector2 local);
        local = Vector2.ClampMagnitude(local, radius);
        handle.anchoredPosition = local;
        Direction = local / radius;
    }

    public void OnPointerUp(PointerEventData e)
    {
        IsHeld = false;
        handle.anchoredPosition = Vector2.zero;
        Direction = Vector2.zero;
    }

    private void OnDisable()
    {
        IsHeld = false;
        Direction = Vector2.zero;
    }
}