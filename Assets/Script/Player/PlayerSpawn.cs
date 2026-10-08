using UnityEngine;
using System.Collections.Generic;
public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Player player;
    private Vector2 _pointSpawn;

    private Stack<Player> _playerPool = new Stack<Player>();

    public Player SpawnPlayer()
    {
        Player currentPlayer;
        if (_playerPool.Count > 0)
        {
            currentPlayer = _playerPool.Pop();
            currentPlayer.transform.SetLocalPositionAndRotation(_pointSpawn, Quaternion.identity);

        }
        else
        {
            currentPlayer = Instantiate(player, _pointSpawn, Quaternion.identity);
        }
        return currentPlayer;
    }
    
}
