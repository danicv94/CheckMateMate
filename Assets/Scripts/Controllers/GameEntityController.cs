using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class GameEntityController : MonoBehaviour
{
    private EnemyPool _pool;
    public AbstractBrain brain;
    public List<ItemType> items;
    private GameController game_controller;
    public GunController gun_controller;
    public Rigidbody2D RigidBody;
    public EnemyType EnemyType;
    public InvokeEnemiesController invoke_enemies;
    public float speed_movement;
    public bool have_gun;
    public bool has_invoke_power;
    public int health_init;

    private int health;

    public GameController getGameController()
    {
        return game_controller;
    }

    // Use this for initialization
    void Start()
    {
        ResetHealth();
        _pool = GameObject.Find("Pools").GetComponentInChildren<EnemyPool>();
        switch (EnemyType)
        {
            case EnemyType.Peon:
                brain = new PeonBrain();
                break;
            case EnemyType.Alfil:
                brain = new AlfilBrain();
                break;
            case EnemyType.Caballo:
                brain = new CaballoBrain();
                break;
            case EnemyType.Torre:
                brain = new TorreBrain();
                break;
            case EnemyType.Reina:
                brain = new ReinaBrain(this, invoke_enemies);
                break;
            case EnemyType.Rey:
                brain = new ReyBrain(this, invoke_enemies);
                break;
            case EnemyType.Kasparov:
                brain = new KasparovBrain();
                break;
        }
    }


    public void ResetHealth()
    {
        health = health_init;
    }

    public void DecrementHealth(int Quantity)
    {
        if (brain.canDecrementHeal())
        {
            health -= Quantity;
            if (health <= 0)
            {
                _pool.FreeEnemy(0, gameObject);
                //game_controller.NotifyEnemyDied (gameObject);
                OnDie();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        brain.UpdateTime(Time.deltaTime);
        Move();
        Shot();
        InvokeEnemies();
        brain.voy_a_morir();
    }

    void Move()
    {
        RigidBody.velocity = brain.GetMovement(game_controller.GetPlayerController(), this) * speed_movement *
                             brain.GetSpeed();
    }

    public void SetGameController(GameController gameController)
    {
        game_controller = gameController;
    }

    void Shot()
    {
        if (have_gun)
        {
            gun_controller.Shoot(brain.GetShoot(game_controller.GetPlayerController(), this));
        }
    }

    void InvokeEnemies()
    {
        if (has_invoke_power)
        {
            invoke_enemies.InvokeEnemy(brain.InvokeEnemies());
        }
    }

    public void ResetValues()
    {
        ResetHealth();
    }

    public int getCurrentHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        if (health > health_init)
        {
            health = health_init;
        }

        this.health = health;
    }

    public void OnDie()
    {
        float r = Random.Range(0f, 100f);
        if (r <= 10f)
        {
            int i = Random.Range(0, 5);
            Vector3 position = transform.position;
            switch (i)
            {
                case 0:
                    game_controller.InstantiateItem(ItemType.Heal, position);
                    break;
                case 1:
                    game_controller.InstantiateItem(ItemType.DamageUp, position);
                    break;
                case 2:
                    game_controller.InstantiateItem(ItemType.SpeedShootUp, position);
                    break;
                case 3:
                    game_controller.InstantiateItem(ItemType.Shield, position);
                    break;
                case 4:
                    game_controller.InstantiateItem(ItemType.Speed, position);
                    break;
            }
        }
		game_controller.addScore (EnemyType);
    }
}