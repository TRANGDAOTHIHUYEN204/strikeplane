using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject loseGameCanvas;
    [SerializeField] private GameObject winGameCanvas;
    [SerializeField] private ResetManager resetManager;


    public void RePlayLoseGame() => Replay(loseGameCanvas);
    public void RePlayWinGame()  => Replay(winGameCanvas);

    private void Replay(GameObject canvas)
    {
        canvas.SetActive(false);
        resetManager.ResetGame();
    }
}
