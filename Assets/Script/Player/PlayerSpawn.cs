using UnityEngine;
using System.Collections.Generic;
public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Vector2 pointSpawn;

    private Stack<Player> _playerPool = new Stack<Player>();

    public Player SpawnPlayer()
    {
        Player currentPlayer;
        if (_playerPool.Count > 0)
        {
            currentPlayer = _playerPool.Pop();
            currentPlayer.transform.SetLocalPositionAndRotation(pointSpawn, Quaternion.identity);

        }
        else
        {
            currentPlayer = Instantiate(_player, pointSpawn, Quaternion.identity);
        }
        return currentPlayer;
    }
    
}
