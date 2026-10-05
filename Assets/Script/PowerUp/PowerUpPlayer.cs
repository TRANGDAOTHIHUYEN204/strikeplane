using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static Action<BulletDataBase, float> OnPowerUpCollected;

    [SerializeField] private BulletDataBase powerUpBulletData;

    private void OnTriggerEnter2D(Collider2D colliderTarget)
    {
        if (colliderTarget.CompareTag("Player"))
        {
            OnPowerUpCollected?.Invoke(powerUpBulletData, 2f);

            Destroy(gameObject);
        }
    }
}