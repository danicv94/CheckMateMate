using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int Damage;
    public int Modifier;
    public float Speed;
    public BulletType BulletType;
    private bool _hitted;
    public Rigidbody2D RigidbodyBullet;
    private float _timeCounter = 0;
    public EntityType Caster;
    private BulletPool _pool;
    private ParticlePool _particlePool;
    private float _bulletLifeTime = 3;
    private Vector2 _direction;
    private int _damage;

    // Use this for initialization
    void Start()
    {
        _hitted = false;
        _pool = GameObject.Find("Pools").GetComponentInChildren<BulletPool>();
        _particlePool = GameObject.Find("Pools").GetComponentInChildren<ParticlePool>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeCounter += Time.deltaTime;
        RigidbodyBullet.velocity = getTrajectory() * Speed;
        if (_timeCounter > _bulletLifeTime)
        {
            _pool.FreeBullet(BulletType, gameObject);
        }
    }

    // Function that returns the trajectory
    Vector2 getTrajectory()
    {
        return _direction;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void ResetValues()
    {
        RigidbodyBullet.velocity = Vector2.zero;
        _timeCounter = 0;
        _hitted = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (Caster)
        {
            case EntityType.Player:
                if (col.gameObject.CompareTag("Enemy") && !_hitted)
                {
                    _hitted = true;
                    _particlePool.GetParticle().transform.position = col.gameObject.transform.position;
                    GameEntityController gec = col.gameObject.GetComponent<GameEntityController>();
                    gec.DecrementHealth(_damage);
                    _pool.FreeBullet(BulletType, gameObject);
                }

                break;
            case EntityType.Enemy:
                if (col.gameObject.CompareTag("Player") && !_hitted)
                {
                    _hitted = true;
                    _particlePool.GetParticle().transform.position = col.gameObject.transform.position;
                    PlayerController pc = col.gameObject.GetComponent<PlayerController>();
                    pc.DecrementHealth(_damage);
                    _pool.FreeBullet(BulletType, gameObject);
                }

                break;
        }
    }

    public void setCaster(EntityType caster)
    {
        Caster = caster;
    }

    public void SetBaseDamage(float baseDamage)
    {
        _damage = (int) baseDamage * Damage;
    }
}