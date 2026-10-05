using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float score;
    private int countKill;
    private int totalKill;
    
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI killTotal;

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
        ApplyScore();
        ApplyKill();

    }
    public void AddScore(float amount)
    {
        score += amount;
        ApplyScore();
        Debug.Log(score);
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
        score = 0f;
        countKill = 0;
        totalKill = 0;
        ApplyScore();
        ApplyKill();
    }
    private void ApplyScore()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("F0");
        }
    }
    private void ApplyKill()
    {
        if ( killText != null)
        {
            killText.text = countKill.ToString();
        }
        
    }
    private void ApplyTotalKill()
    {
        if (killTotal != null)
        {
            killTotal.text = totalKill.ToString();
        }
    }
    
}
