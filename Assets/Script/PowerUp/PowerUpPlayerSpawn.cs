using UnityEngine;
using System.Collections.Generic;
public class PowerUpPlayerSpawn : MonoBehaviour
{
    [SerializeField] private PowerUpPlayer _powerupPlayer;
    [SerializeField] private BulletDataBase[] _bulletDatabase;
    [SerializeField] private float _spawnInterval = 3f;
    private float _currentTime;
    private Stack<PowerUpPlayer> _powerUpPool = new Stack<PowerUpPlayer>();
    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _spawnInterval)
        {
            SpawnPowerUp();
            _currentTime = 0f;
        }
    }

    private void SpawnPowerUp()
    {
        PowerUpPlayer powerUpPlayer;
        BulletDataBase randomData = GetRandomBulletData();
        Vector2 randomPosition = GetRandomPosition();

        if (_powerUpPool.Count > 0)
        {
            powerUpPlayer = _powerUpPool.Pop();
            powerUpPlayer.transform.position = randomPosition;
            powerUpPlayer.gameObject.SetActive(true);
        }
        else
        {
            powerUpPlayer = Instantiate(_powerupPlayer, randomPosition, Quaternion.identity);
        }

        powerUpPlayer.Init(randomData);
    }

    private BulletDataBase GetRandomBulletData()
    {
        int index = Random.Range(0, _bulletDatabase.Length);
        return _bulletDatabase[index];
    }
    public void Release(PowerUpPlayer powerUpPlayer)
    {
        powerUpPlayer.gameObject.SetActive(false);
        _powerUpPool.Push(powerUpPlayer);
    }
    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(ScreenBoudaries.MinX, ScreenBoudaries.MaxX);
        float y = Random.Range(ScreenBoudaries.MinY, ScreenBoudaries.MaxY);

        return new Vector2(x, y);
    }
}
