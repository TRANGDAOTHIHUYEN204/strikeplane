using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{
    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private ScreenBoudaries _screenBoudaries;
    [SerializeField] private LoseManager _loseManager;

    [Header("Wave 1 - Rơi thẳng (spawn ở mép trên, X ngẫu nhiên)")]
    [SerializeField] private float _straightStartTime = 1.2f;
    [SerializeField, Min(0)] private int _straightCount = 10;
    [SerializeField, Min(0.05f)] private float _straightInterval = 0.8f;   
    [SerializeField, Min(0.5f)] private float _straightFallDuration = 5f;
    [SerializeField, Range(0f, 1f)] private float _spawnMinT = 0.1f;      
    [SerializeField, Range(0f, 1f)] private float _spawnMaxT = 0.9f;       

    [Header("Wave 2 - Trái tim")]
    [SerializeField, Min(0f)] private float _delayBeforeHeart = 4f;        
    [SerializeField, Min(1)] private int _heartCount = 20;
    [SerializeField, Min(0.01f)] private float _heartScale = 0.12f;
    [SerializeField, Range(0f, 1f)] private float _heartCenterY = 0.65f;
    [SerializeField, Min(0.05f)] private float _heartInterval = 0.15f;

    [Header("Wave 3 - Lượn sóng")]
    [SerializeField, Min(0f)] private float _delayBeforeWave = 8f;        
    [SerializeField, Min(0)] private int _waveCount = 12;
    [SerializeField, Min(0.05f)] private float _waveInterval = 0.25f;

    private class Step
    {
        public float Time;
        public int Count;
        public float Interval;
        public float MoveDuration;
        public Func<Vector2> GetPosition;
        public Func<int, MoveBase> CreateMove;
    }

    private readonly List<Step> _steps = new List<Step>();
    private Coroutine _scriptCoroutine;
    private Coroutine _winCoroutine;
    private int _runningSteps;
    public int TotalEnemy { get; private set; }

    private void Start()
    {
        BuildScript();
        ScoreManager.Instance.SetTotalKill(TotalEnemy);
        StartScript();
    }

    private void BuildScript()
    {
        _steps.Clear();
        
        float straightTime = _straightStartTime;
        float heartTime = straightTime + _straightCount * _straightInterval + _delayBeforeHeart;
        float waveTime = heartTime + _heartCount * _heartInterval + _delayBeforeWave;
        
        _steps.Add(new Step
        {
            Time = straightTime, Count = _straightCount, Interval = _straightInterval,
            MoveDuration = _straightFallDuration,
            GetPosition = () => _spawnEnemy.TopCenter(0f, RandomT(), _enemyPrefab),
            CreateMove = i =>
            {
                float distance = (_screenBoudaries.ViewMaxY - _screenBoudaries.ViewMinY) + 2f;
                return new StraightDownMove(distance);
            }
        });


        _steps.Add(new Step
        {
            Time = heartTime, Count = _heartCount, Interval = _heartInterval, MoveDuration = 8f,
            GetPosition = () => _spawnEnemy.TopLeft(0.8f, _enemyPrefab),
            CreateMove = i =>
            {
                Vector2 center = new Vector2(
                    Mathf.Lerp(_screenBoudaries.MinX, _screenBoudaries.MaxX, 0.5f),
                    Mathf.Lerp(_screenBoudaries.MinY, _screenBoudaries.MaxY, _heartCenterY));
                return new HeartFormationMove(HeartPoint(i, _heartCount, center, _heartScale));
            }
        });
        
        _steps.Add(new Step
        {
            Time = waveTime, Count = _waveCount, Interval = _waveInterval, MoveDuration = 6f,
            GetPosition = () => _spawnEnemy.TopCenter(0f, 0.75f, _enemyPrefab),
            CreateMove = i => new WaveDownMove(2f, 12f, 2f)
        });

        TotalEnemy = 0;
        foreach (var s in _steps) TotalEnemy += s.Count;
    }
    
    private float RandomT()
    {
        return UnityEngine.Random.Range(_spawnMinT, _spawnMaxT);
    }

    private Vector2 HeartPoint(int index, int count, Vector2 center, float scale)
    {
        float a = (float)index / count * Mathf.PI * 2f;
        float s = Mathf.Sin(a);
        float x = 16f * s * s * s;
        float y = 13f * Mathf.Cos(a) - 5f * Mathf.Cos(2f * a)
                  - 2f * Mathf.Cos(3f * a) - Mathf.Cos(4f * a);
        return center + new Vector2(x, y) * scale;
    }

    private void StartScript()
    {
        _runningSteps = 0;
        _scriptCoroutine = StartCoroutine(RunScript());
    }

    private IEnumerator RunScript()
    {
        float start = Time.time;

        foreach (var step in _steps)
        {
            float wait = step.Time - (Time.time - start);
            if (wait > 0f) yield return new WaitForSeconds(wait);
            if (LoseManager.isGameOver) yield break;

            _runningSteps++;
            StartCoroutine(RunStep(step));
        }

        while (_runningSteps > 0)
        {
            if (LoseManager.isGameOver) yield break;
            yield return null;
        }

        _winCoroutine = StartCoroutine(WaitForWin());
    }

    private IEnumerator RunStep(Step step)
    {
        var wait = new WaitForSeconds(step.Interval);

        for (int i = 0; i < step.Count; i++)
        {
            while (!_spawnEnemy.CanSpawn)
            {
                if (LoseManager.isGameOver) { _runningSteps--; yield break; }
                yield return null;
            }

            if (LoseManager.isGameOver) { _runningSteps--; yield break; }

            Enemy enemy = _spawnEnemy.SpawnEnemyWave(_enemyPrefab, _enemyData, step.GetPosition());
            if (enemy != null)
                enemy.SetMove(step.CreateMove(i), step.MoveDuration);

            yield return wait;
        }

        _runningSteps--;
    }

    private IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1f);

        while (_spawnEnemy._enemyActive.Count > 0)
        {
            if (LoseManager.isGameOver) yield break;
            yield return new WaitForSeconds(0.5f);
        }

        if (!LoseManager.isGameOver)
            ShowWin();
    }

    private void ShowWin()
    {
        _loseManager.SetActiveWinPanel();
    }

    public void StopScript()
    {
        StopAllCoroutines();
        _scriptCoroutine = null;
        _winCoroutine = null;
        _runningSteps = 0;
    }

    public void ResetSpawn()
    {
        StopScript();
        _spawnEnemy.ResetEnemy();
        StartScript();
    }
}   