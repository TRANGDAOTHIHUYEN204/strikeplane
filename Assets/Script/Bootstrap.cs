using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is missed in Bootstrap");
        }
    }

    public void Start() 
    {
        _player.Initialized(_characterData);
        Debug.Log($"MaxHp: {_characterData.MaxHp}");
    }
}
