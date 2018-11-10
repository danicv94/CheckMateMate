using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Utils;

public class GunController : MonoBehaviour
{
    public BulletType SelectedBullet;
    public float ShootRatio;
    public float BaseDamage;
    public EntityType CasterType;
    private float _cooldownCounter;
    private BulletPool _pool;

    public GameController gameController;

    // Use this for initialization
    void Start()
    {
        _cooldownCounter = 0;
        _pool = GameObject.Find("Pools").GetComponentInChildren<BulletPool>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        _cooldownCounter += Time.deltaTime;
    }

    // Function that instantiate a bullet and shoot it
    public void Shoot(Vector2 v)
    {
        if (ShootRatio < _cooldownCounter)
        {
            GameObject Bullet = _pool.GetBullet(SelectedBullet);
            gameController.setFx(Resources.Load<AudioClip>("shotSound"));
            Bullet.transform.position = transform.position;
            BulletController bc = Bullet.GetComponent<BulletController>();
            bc.setCaster(CasterType);
            bc.SetDirection(v.normalized);
            bc.SetBaseDamage(BaseDamage);
            bc.transform.up = v;

            _cooldownCounter = 0f;
        }
    }

    public void Shoot(Vector2[] m)
    {
        if (ShootRatio < _cooldownCounter)
        {
            for (int i = 0; i < m.Length; i++)
            {
                GameObject Bullet = _pool.GetBullet(SelectedBullet);
                Bullet.transform.position = transform.position;
                BulletController bc = Bullet.GetComponent<BulletController>();
                bc.setCaster(CasterType);
                bc.SetDirection(m[i].normalized);
                bc.SetBaseDamage(BaseDamage);
                bc.transform.up = m[i];
            }

            _cooldownCounter = 0f;
        }
    }

    public void ShootPlayer(Vector2 direction)
    {
        if (ShootRatio < _cooldownCounter)
        {
            _cooldownCounter = 0f;
            GameObject bullet;
            BulletController bc;
            float angle;
            switch (SelectedBullet)
            {
                case BulletType.Peon:
                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(direction.normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = direction;
                    break;

                case BulletType.Alfil:
                    angle = 20;
                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(MathUtils.Rotate(direction, angle).normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = MathUtils.Rotate(direction, angle);

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(MathUtils.Rotate(direction, 0).normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = MathUtils.Rotate(direction, 0);

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(MathUtils.Rotate(direction, -angle).normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = MathUtils.Rotate(direction, -angle);
                    break;

                case BulletType.Caballo:
                    Vector2 offsett = MathUtils.Rotate(direction, 90).normalized * 0.5f;
                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position + new Vector3(offsett.x, offsett.y, 0);
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(direction.normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = direction;

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(direction.normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = direction;

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position - new Vector3(offsett.x, offsett.y, 0);
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(direction.normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = direction;
                    break;

                case BulletType.Torre:
                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(direction.normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = direction;

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(-direction.normalized);
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = -direction;

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(MathUtils.Rotate(direction.normalized, 90));
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = MathUtils.Rotate(direction.normalized, 90);

                    bullet = _pool.GetBullet(SelectedBullet);
                    gameController.setFx(Resources.Load<AudioClip>("shotSound"));
                    bullet.transform.position = transform.position;
                    bc = bullet.GetComponent<BulletController>();
                    bc.setCaster(CasterType);
                    bc.SetDirection(MathUtils.Rotate(direction.normalized, -90));
                    bc.SetBaseDamage(BaseDamage);
                    bc.transform.up = MathUtils.Rotate(direction.normalized, -90);
                    break;
            }
        }
    }
}