using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseGameCanvas;
    [SerializeField] private ScoreManager _scoreManager;
    public void RePlay()
    {
        _loseGameCanvas.SetActive(false);
        _scoreManager.ResetScore();
    }
}
