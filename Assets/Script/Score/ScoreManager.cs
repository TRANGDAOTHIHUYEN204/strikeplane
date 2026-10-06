using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float currentScore;
    private float highScore;
    private int countKill;
    private int totalKill;
    private const string SCORE_KEY = "highScore";

    public static ScoreManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI killTotal;
    [SerializeField] private TextMeshProUGUI textHighScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetFloat(SCORE_KEY, 0f);
        currentScore = 0f;
        ApplyScore();
        ApplyKill();
        ApplyHighScore();

    }
    public void AddScore(float amount)
    {
        currentScore += amount;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat(SCORE_KEY, highScore);
            PlayerPrefs.Save();
            ApplyHighScore();
        }

        ApplyScore();
    }
    public void AddKill()
    {
        countKill++;

        ApplyKill();
    }
    public void SetTotalKill(int enemySpawn)
    {
        totalKill = enemySpawn;
        ApplyTotalKill();
    }

    public void ResetScore()
    {
        currentScore = 0f;
        countKill = 0;
        totalKill = 0;
        ApplyScore();
        ApplyKill();
    }
    private void ApplyScore()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString("F0");
        }
    }
    private void ApplyKill()
    {
        if ( killText != null)
        {
            killText.text = countKill.ToString();
        }
        
    }
    private void ApplyHighScore()
    {
        if (textHighScore != null)
        {
            textHighScore.text = highScore.ToString("F0");
        }
    }
    private void ApplyTotalKill()
    {
        if (killTotal != null)
        {
            killTotal.text = totalKill.ToString();
        }
    }
    private void ClearHighScore()
    {
        highScore = 0f;
        PlayerPrefs.DeleteKey(SCORE_KEY);
        PlayerPrefs.Save();
        ApplyHighScore();
    }

}
