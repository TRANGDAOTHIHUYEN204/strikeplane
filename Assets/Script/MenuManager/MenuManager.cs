using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseGameCanvas;
    private ResetManager _resetManager;
    private void Awake()
    {
        _resetManager = GetComponent<ResetManager>();
    }
    public void RePlay()
    {
        _loseGameCanvas.SetActive(false);
        _resetManager.ResetGame();
    }
}
