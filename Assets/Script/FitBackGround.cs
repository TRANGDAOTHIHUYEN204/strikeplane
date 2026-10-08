using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[ExecuteAlways]
public class FitBackGround : MonoBehaviour
{
    private Image img;
    private RectTransform rt;
    private RectTransform canvasRt;
    private Vector2 lastSize;

    private void OnEnable()
    {
        Fit();
    }

    private void LateUpdate()
    {
        if (canvasRt == null || canvasRt.rect.size != lastSize)
            Fit();
    }

    public void Fit()
    {
        if (img == null) img = GetComponent<Image>();
        if (rt == null) rt = GetComponent<RectTransform>();
        if (img.sprite == null) return;

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null) return;
        canvasRt = canvas.rootCanvas.transform as RectTransform;

        Vector2 canvasSize = canvasRt.rect.size;
        lastSize = canvasSize;

        Vector2 spriteSize = img.sprite.rect.size;
        float scale = Mathf.Max(canvasSize.x / spriteSize.x, canvasSize.y / spriteSize.y);

        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = spriteSize * scale;
        rt.localScale = Vector3.one;
    }
}