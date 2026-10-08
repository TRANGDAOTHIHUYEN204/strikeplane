using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField]  Player _player;

    private void Awake()
    {
        if (_player == null)
        {
            Debug.LogError("Player is missed in Bootstrap");
        }
    }

    public void Start() 
    {
        _player.Initialized(_characterData);
        Debug.Log($"Player Hp: {_player.CurrentHp}");
    }
}
