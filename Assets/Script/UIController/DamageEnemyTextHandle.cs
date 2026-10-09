using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DamageEnemyTextHandle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float duration = 0.8f;

    private RectTransform rect;
    private Action<DamageEnemyTextHandle> releaseAction;

    private void Awake()
    {
        rect = (RectTransform)transform;
    }

    public void Init(Action<DamageEnemyTextHandle> release)
    {
        releaseAction = release;
    }

    public void Setup(int damageAmount, Vector2 localPos)
    {
        textMesh.text = damageAmount.ToString();
        rect.anchoredPosition = localPos;
        StartCoroutine(AnimateAndRelease());
    }

    private IEnumerator AnimateAndRelease()
    {
        float timer = 0f;
        Vector2 startPos = rect.anchoredPosition;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            rect.anchoredPosition = startPos + Vector2.up * (moveSpeed * (timer / duration));
            yield return null;
        }

        releaseAction?.Invoke(this);
    }
}