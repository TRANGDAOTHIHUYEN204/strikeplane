using System;
using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    public static Action<BulletDataBase, float> OnPowerUpCollected;

    private BulletDataBase powerUpBulletData;
    private SpriteRenderer spriteBullet;
    private PowerUpPlayerSpawn _powerUpPlayerSpawn;
    private PowerUpMovement _powerUpMovement;
    private void Awake()
    {
        spriteBullet = GetComponent<SpriteRenderer>();
        _powerUpMovement = GetComponent<PowerUpMovement>();
    }
    public void Init(BulletDataBase data, PowerUpPlayerSpawn spawn, ScreenBoudaries boudaries)
    {
        powerUpBulletData = data;
        _powerUpPlayerSpawn = spawn;
        spriteBullet.sprite = data.PowerUpIcon;
        _powerUpMovement.Init(this, 2f, boudaries);
    }

    private void OnTriggerEnter2D(Collider2D colliderTarget)
    {
        if (colliderTarget.TryGetComponent(out Player player))
        {
            Debug.Log("Interact with Player");
            OnPowerUpCollected?.Invoke(powerUpBulletData, 5f);
            Despawn();
        }
    }
    public void Despawn()
    {
        _powerUpPlayerSpawn.Release(this);
    }

    
}