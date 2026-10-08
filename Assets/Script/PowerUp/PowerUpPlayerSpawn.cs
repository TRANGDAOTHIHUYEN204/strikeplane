using UnityEngine;
using System.Collections.Generic;
public class PowerUpPlayerSpawn : MonoBehaviour
{
    [SerializeField] private PowerUpPlayer _powerupPlayer;
    [SerializeField] private BulletDataBase[] _bulletDatabase;
    [SerializeField] private float _spawnInterval = 3f;
    private float _currentTime;
    private Stack<PowerUpPlayer> _powerUpPool = new Stack<PowerUpPlayer>();
    private List<PowerUpPlayer> _powerUpActive = new List<PowerUpPlayer>();
    [SerializeField] private ScreenBoudaries _screenBoudaries;
    private void Update()
    {
        if (LoseManager.isGameOver) return;
        _currentTime += Time.deltaTime;

        if (_currentTime >= _spawnInterval)
        {
            SpawnPowerUp();
            _currentTime = 0f;
        }
    }

    private void SpawnPowerUp()
    {
        PowerUpPlayer currentPowerUpPlayer;
        BulletDataBase randomData = GetRandomBulletData();
        Vector2 randomPosition = GetRandomPosition();

        if (_powerUpPool.Count > 0)
        {
            currentPowerUpPlayer = _powerUpPool.Pop();
            currentPowerUpPlayer.transform.position = randomPosition;
            currentPowerUpPlayer.gameObject.SetActive(true);
        }
        else
        {
            currentPowerUpPlayer = Instantiate(_powerupPlayer, randomPosition, Quaternion.identity);
        }

        currentPowerUpPlayer.Init(randomData, this, _screenBoudaries);
        _powerUpActive.Add(currentPowerUpPlayer);
        Debug.Log($"random : {randomData.name}");
    }
    public void ResetPowerUpPlayer()
    {
        while (_powerUpActive.Count > 0)
        {
            PowerUpPlayer powerUpActive = _powerUpActive[_powerUpActive.Count - 1];
            Release(powerUpActive);
        }
    }
    private BulletDataBase GetRandomBulletData()
    {
        int index = Random.Range(0, _bulletDatabase.Length);
        return _bulletDatabase[index];
    }
    public void Release(PowerUpPlayer powerUpPlayer)
    {
        powerUpPlayer.gameObject.SetActive(false);
        _powerUpActive.Remove(powerUpPlayer);
        _powerUpPool.Push(powerUpPlayer);
    }
    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(_screenBoudaries.MinX, _screenBoudaries.MaxX);
        float y = _screenBoudaries.MaxY + 1f;

        return new Vector2(x, y);
    }
}
