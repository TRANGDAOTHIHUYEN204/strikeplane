using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] LoseManager _loseGameManager;
    [SerializeField] WaveSpawnController _waveSpawnController;
    [SerializeField] private ScoreManager _scoreManager;
    public void ResetGame()
    {
        _waveSpawnController.ResetSpawn();
        _loseGameManager.ResetStatGame();
        _scoreManager.ResetScore();
    }

}
