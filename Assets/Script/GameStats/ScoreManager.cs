using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float _currentScore;
    private float _highScore;
    private int _countKill;
    private int _totalKill;
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
        _highScore = PlayerPrefs.GetFloat(SCORE_KEY, 0f);
        _currentScore = 0f;
        ApplyScore();
        ApplyKill();
        ApplyHighScore();

    }
    public void AddScore(float amount)
    {
        _currentScore += amount;

        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetFloat(SCORE_KEY, _highScore);
            PlayerPrefs.Save();
            ApplyHighScore();
        }

        ApplyScore();
    }
    public void AddKill()
    {
        _countKill++;

        ApplyKill();
    }
    public void SetTotalKill(int enemySpawn)
    {
        _totalKill = enemySpawn;
        ApplyTotalKill();
    }

    public void ResetScore()
    {
        _currentScore = 0f;
        _countKill = 0;
        _totalKill = 0;
        ApplyScore();
        ApplyKill();
    }
    private void ApplyScore()
    {
        if (scoreText != null)
        {
            scoreText.text = _currentScore.ToString("F0");
        }
    }
    private void ApplyKill()
    {
        if ( killText != null)
        {
            killText.text = _countKill.ToString();
        }
        
    }
    private void ApplyHighScore()
    {
        if (textHighScore != null)
        {
            textHighScore.text = _highScore.ToString("F0");
        }
    }
    private void ApplyTotalKill()
    {
        if (killTotal != null)
        {
            killTotal.text = _totalKill.ToString();
        }
    }
    private void ClearHighScore()
    {
        _highScore = 0f;
        PlayerPrefs.DeleteKey(SCORE_KEY);
        PlayerPrefs.Save();
        ApplyHighScore();
    }

}
