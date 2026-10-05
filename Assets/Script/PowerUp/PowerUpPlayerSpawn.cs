using UnityEngine;

public class PowerUpPlayerSpawn : MonoBehaviour
{
    [SerializeField] private PowerUpPlayer _powerupPlayer;
    [SerializeField] private BulletDataBase[] _bulletDatabase;
    [SerializeField] private float _spawnInterval = 3f;
    private float _currentTime;
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
        BulletDataBase randomData = GetRandomBulletData();
        Vector2 randomPosition = GetRandomPosition();

        PowerUpPlayer powerUp = Instantiate(_powerupPlayer, randomPosition, Quaternion.identity);

        powerUp.Init(randomData);
    }

    private BulletDataBase GetRandomBulletData()
    {
        int index = Random.Range(0, _bulletDatabase.Length);
        return _bulletDatabase[index];
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(ScreenBoudaries.MinX, ScreenBoudaries.MaxX);
        float y = Random.Range(ScreenBoudaries.MinY, ScreenBoudaries.MaxY);

        return new Vector2(x, y);
    }
}
