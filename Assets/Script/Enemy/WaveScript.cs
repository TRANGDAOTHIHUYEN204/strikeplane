using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EnemyData _enemyData;

    private class Step
    {
        public float Time;              
        public int Count;
        public float Interval;
        public float MoveDuration;
        public Func<Vector2> GetPosition;
        public Func<MoveBase> CreateMove;  
    }

    private readonly List<Step> _steps = new List<Step>();

    private void Start()
    {
        BuildScript();
        StartCoroutine(RunScript());
    }

    private void BuildScript()
    {
        _steps.Add(new Step
        {
            Time = 1.2f, Count = 20, Interval = 0.15f, MoveDuration = 4f,
            GetPosition = () => _spawnEnemy.TopCenter(-5f),
            CreateMove = () => new HeartMove(0.25f)
        });
        
        _steps.Add(new Step
        {
            Time = 5.2f, Count = 10, Interval = 0.3f, MoveDuration = 5f,
            GetPosition = () => _spawnEnemy.TopCenter(),
            CreateMove = () => new StraightDownMove(12f)
        });
        
        _steps.Add(new Step
        {
            Time = 9.2f, Count = 12, Interval = 0.25f, MoveDuration = 6f,
            GetPosition = () => _spawnEnemy.TopCenter(),
            CreateMove = () => new WaveDownMove(2f, 12f, 2f)
        });
    }

    private IEnumerator RunScript()
    {
        float start = Time.time;

        foreach (var step in _steps)
        {
            float wait = step.Time - (Time.time - start);
            if (wait > 0f) yield return new WaitForSeconds(wait);

            StartCoroutine(RunStep(step)); 
        }
    }

    private IEnumerator RunStep(Step step)
    {
        var wait = new WaitForSeconds(step.Interval);

        for (int i = 0; i < step.Count; i++)
        {
            Enemy enemy = _spawnEnemy.SpawnEnemyWave(_enemyPrefab, _enemyData, step.GetPosition());

            if (enemy != null)
                enemy.SetMove(step.CreateMove(), step.MoveDuration);

            yield return wait;
        }
    }

    public void StopScript() => StopAllCoroutines();
}