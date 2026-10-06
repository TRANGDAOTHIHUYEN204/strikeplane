using System;
using Unity.VisualScripting;
using UnityEngine;

public enum BulletType
{
    NormalBullet,
    PiercingBullet,
    Explosive,
    HomingBullet
}
[CreateAssetMenu(fileName = "BulletData", menuName = "BulletBase", order = 3)]
public class BulletDataBase : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private BulletType _bulletType;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speedMove;
    [SerializeField] private Sprite _powerUpIcon;

    public int Damage
    {
        get {  return _damage; }
        private set { _damage = Mathf.Max(1, value); }
    }
    public BulletType TypeBullet
    {
        get { return _bulletType; }
        private set { _bulletType = value; }
    }
    public LayerMask LayerTarget
    {
        get { return layerTarget;}
        private set { layerTarget = value; }
    }
    public float LifeTime => _lifeTime;
    public float SpeedMove => _speedMove;
    public Sprite PowerUpIcon => _powerUpIcon;
}
