using System.Collections;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float duration = 0.8f;

    public void Setup(int damageAmount, Vector2 position)
    {
        textMesh.text = damageAmount.ToString();
        transform.position = position;

        StartCoroutine(AnimateAndDestroy());
    }

    private IEnumerator AnimateAndDestroy()
    {
        float timer = 0f;
        Vector3 startPos = transform.position;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = startPos + Vector3.up * (moveSpeed * (timer / duration));
            yield return null;
        }
        Destroy(gameObject);
    }
}