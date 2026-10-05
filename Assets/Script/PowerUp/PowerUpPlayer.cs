using System;
using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    public static Action<BulletDataBase, float> OnPowerUpCollected;

    [SerializeField] private BulletDataBase powerUpBulletData;
    [SerializeField] private SpriteRenderer spriteBullet;

    public void Init(BulletDataBase data)
    {
        powerUpBulletData = data;
        spriteBullet = data.PowerUpIcon;
    }

    private void OnTriggerEnter2D(Collider2D colliderTarget)
    {
        if (colliderTarget.CompareTag("Player"))
        {
            OnPowerUpCollected?.Invoke(powerUpBulletData, 1f);

            //Destroy(gameObject);
        }
    }
    public void Despawn()
    {

    }
}