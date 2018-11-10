using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GunController GunController;
    private int Health = 10;
    public int Max_health;
    public UnityEngine.UI.Slider HealthSlider;
    public float InmunityDuration;
    private float _currentInmunity;
    private int _shieldCount;
    public float Speed;

    private List<BulletType> _bulletTypes = new List<BulletType>(new[]
        {BulletType.Peon, BulletType.Alfil, BulletType.Caballo, BulletType.Torre});

    private void Start()
    {
        GunController.SelectedBullet = _bulletTypes[0];
        _shieldCount = 0;
        HealthSlider.value = Health;
    }

    private void Update()
    {
        _currentInmunity -= Time.deltaTime;
        HealthSlider.value = Health;
    }

    public void MovePlayer(Vector2 v)
    {
        GetComponent<Rigidbody2D>().velocity = v * Speed;
    }

    public void Shoot(Vector2 direction)
    {
        GunController.ShootPlayer(direction);
    }

    public void ApplyUpgrade(ItemType t)
    {
        switch (t)
        {
            case ItemType.DamageUp:
                GunController.BaseDamage *= 1.2f;
                break;
            case ItemType.SpeedShootUp:
                GunController.ShootRatio *= 0.88f;
                break;
            case ItemType.Heal:
                Max_health += 1;
                if (Health + 2 > Max_health)
                {
                    Health = Max_health;
                } else {
                    Health += 2;
                }
                break;
            case ItemType.Shield:
                _shieldCount++;
                break;
            case ItemType.Speed:
                Speed += 0.4f;
                break;
        }
    }

    public void DecrementHealth(int Quantity)
    {
        if (_currentInmunity <= 0)
        {
            if (_shieldCount == 0)
            {
                Health -= Quantity;
                CheckPlayerDied();
            }
            else
            {
                _shieldCount--;
            }

            _currentInmunity = InmunityDuration;
        }
    }

    private void CheckPlayerDied()
    {
        if (Health <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameController>().NotifyPlayerDied();
        }
    }

    public void ChangeBullet(bool b)
    {
        int index = _bulletTypes.IndexOf(GunController.SelectedBullet);
        Debug.Log(index);
        if (b)
        {
            GunController.SelectedBullet =
                _bulletTypes[(index + 1) % _bulletTypes.Count];
        }
        else
        {
            GunController.SelectedBullet =
                _bulletTypes[(index - 1 + _bulletTypes.Count) % _bulletTypes.Count];
        }
    }
}