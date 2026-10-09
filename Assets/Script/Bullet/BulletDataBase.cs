using System;
using UnityEngine;

public enum BulletType
{
    NormalBullet,
    PiercingBullet,
    Explosive,
    HomingBullet,
}

[CreateAssetMenu(fileName = "BulletData", menuName = "BulletBase", order = 3)]
public class BulletDataBase : ScriptableObject
{
    [SerializeField, Min(1)] private int damage;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private float lifeTime;
    [SerializeField, Min(0.1f)] private float speedMove;
    [SerializeField] private Sprite powerUpIcon;
    [SerializeField] private bool addLine;
    public int Damage
    {
        get {  return damage; }
        private set { damage = Mathf.Max(1, value); }
    }
    public BulletType TypeBullet => bulletType;
    public LayerMask LayerTarget =>  layerTarget;
    public float LifeTime => lifeTime;
    public float SpeedMove => speedMove;
    public Sprite PowerUpIcon => powerUpIcon;
    public bool AddLine => addLine;
}
