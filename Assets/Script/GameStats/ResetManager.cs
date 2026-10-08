using System;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] LoseManager loseGameManager;
    [SerializeField] WaveSpawnController waveSpawnController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Player player;
    [SerializeField] private PowerUpPlayerSpawn powerUpPlayerSpawn;
    [SerializeField] private PlayerMovement2D playerMovement2D;
    [SerializeField] private BulletFactory bulletFactory;
    
    public void ResetGame()
    {
        waveSpawnController.ResetSpawn();
        loseGameManager.ResetStatGame();
        scoreManager.ResetScore();
        player.ResetPlayer();
        powerUpPlayerSpawn.ResetPowerUpPlayer();
        bulletFactory.ResetBullet();
        playerMovement2D.ResetMovement();
    }

}
