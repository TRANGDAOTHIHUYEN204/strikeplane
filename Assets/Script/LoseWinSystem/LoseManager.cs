using UnityEngine;

public class LoseManager : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject winPanel;
    public static bool isGameOver { get; private set; }

    private void Awake()
    {
        isGameOver = false;
    }

    public void SetActiveLosePanel()
    {
        LosePanel.SetActive(true);
        isGameOver = true;
    }

    public void SetActiveWinPanel()
    {
        winPanel.SetActive(true);
        isGameOver = true;
    }

    public void ResetStatGame()
    {
        isGameOver = false;
        if (winPanel != null) winPanel.SetActive(false);
    }

    private void OnDestroy() => isGameOver = false;
}