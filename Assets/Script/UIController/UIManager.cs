using UnityEngine;
using UnityEngine.Pool;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DamageEnemyTextHandle damageTextPrefab;
    [SerializeField] private Transform damageTextContainer; // object con của Canvas

    private ObjectPool<DamageEnemyTextHandle> damageTextPool;
    private Camera cam; 

    private void Awake()
    {
        cam = Camera.main;
        damageTextPool = new ObjectPool<DamageEnemyTextHandle>(
            createFunc: () =>
            {
                var obj = Instantiate(damageTextPrefab, damageTextContainer);
                obj.Init(ReleaseDamageText);
                return obj;
            },
            actionOnGet: obj => obj.gameObject.SetActive(true),
            actionOnRelease: obj => obj.gameObject.SetActive(false),
            actionOnDestroy: obj => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    public void ShowDamageText(int damage, Vector3 worldPos)
    {
        Vector2 screenPos = cam.WorldToScreenPoint(worldPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)damageTextContainer, screenPos, cam, out Vector2 localPos);

        var text = damageTextPool.Get();
        text.Setup(damage, localPos);
    }

    private void ReleaseDamageText(DamageEnemyTextHandle text)
    {
        damageTextPool.Release(text);
    }
}