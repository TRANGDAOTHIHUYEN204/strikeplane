using System;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] LoseManager _loseGameManager;
    [SerializeField] WaveSpawnController _waveSpawnController;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Player _player;
    [SerializeField] private PowerUpPlayerSpawn _powerUpPlayerSpawn;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private PlayerMovement2D _playerMovement2D;
    public void ResetGame()
    {
        _waveSpawnController.ResetSpawn();
        _loseGameManager.ResetStatGame();
        _scoreManager.ResetScore();
        _player.ResetPlayer();
        _powerUpPlayerSpawn.ResetPowerUpPlayer();
        _playerShoot.ResetBulletPlayer();
        _playerMovement2D.ResetMovement();
    }

}
