using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractEnemy : MonoBehaviour
{
    private Player _player;
    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D colliderTarget)
    {

        if (colliderTarget.TryGetComponent(out Enemy enemy))
        {
            _player.TakeDamage(enemy.DamageInteract);
            Debug.Log($"Current Player HP: {_player.CurrentHp}");
        }
    }
}
