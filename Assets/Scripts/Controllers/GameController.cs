using System.Collections;
using System.Collections.Generic;
using Controllers;
using Enums;
using Model;
using Pool;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject EmptyItem;
    public GameObject prefab;
    public PlayerController PlayerController;
    public float MinSpawnDistance;
    public float MaxSpawnDistance;
    public UnityEngine.UI.Text WaveValue;
    public UnityEngine.UI.Text LevelValue;
    public UnityEngine.UI.Text EnemiesValue;
    public GameObject gameOver;
    public bool playerDied = false;
    public float TimePerWave;
    public AudioSource Ost;
    public AudioSource Fx;
    public UnityEngine.UI.Text time;
    public UnityEngine.UI.Text scoreUser;

	public int score = 0;


    //public GameObject pools;

    public float Speed;
    private List<Level> _levels;
    private EnemyPool _pool;
    private PowerUpPool _itemPool;
    private int _currentRound;
    private int _currentLevel;
    private int _remainingRoundItems;
    private List<GameObject> _waveEnemies;
    private float _timeCounter;
    private bool _appliedBossMusic;

    private void Start()
    {
        SoundsLoader.GetInstance().GetResource("Infected Mushroom - Spitfire [Monstercat Release]");
        _timeCounter = TimePerWave;
        _waveEnemies = new List<GameObject>();
        //Instantiate(pools);
        InitLevelWaves();
        /*
        for (int i = 0; i < 1; i++)
        {
            GameObject go = Instantiate(prefab);
            go.GetComponent<GameEntityController>().SetGameController(this);
            go.transform.position = new Vector3(i * 2,i,0);
        }
        */
        _pool = GameObject.Find("Pools").GetComponentInChildren<EnemyPool>();
        _itemPool = GameObject.Find("Pools").GetComponentInChildren<PowerUpPool>();
        _currentLevel = 0;
        _currentRound = 0;
        Time.timeScale = 0;
        LoadRound();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesValue.text = _waveEnemies.Count.ToString();
        _timeCounter -= Time.deltaTime;
        if (_timeCounter < 0)
        {
            _timeCounter = TimePerWave;
            UpdateWaveCount();
            LoadRound();
        }
        time.text = _timeCounter.ToString("00");
    }

    public void MovePlayer(Vector2 v)
    {
        PlayerController.MovePlayer(v);
    }

    public void ChangeWeapon(int i)
    {
        if (i > 0)
        {
            PlayerController.ChangeBullet(true);
        }
        else if (i < 0)
        {
            PlayerController.ChangeBullet(false);
        }
    }

    public void setOst(AudioClip audio)
    {
        Ost.clip = audio;
        Ost.Play();
    }

    public void setFx(AudioClip audio)
    {
        Fx.clip = audio;
        Fx.Play();
    }

    public void MapCreated(List<CellController> goCell, List<Cell> cellList, Graph<Cell> graph)
    {
    }

    public PlayerController GetPlayerController()
    {
        return PlayerController;
    }

    public void PauseGame(bool b)
    {
        if (b)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void InitLevelWaves()
    {
        _levels = new List<Level>();

        /******************
         ******Level 1*****
         ******************/
        Level l = new Level();
        _levels.Add(l);
        //-----Wave 1------
        Wave w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        //Wave 2
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 15; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        w.Enemies.Add(EnemyType.Alfil);

        /******************
         ******Level 2*****
         ******************/

        l = new Level();
        _levels.Add(l);

        //-----Wave 1------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 15; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 2; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        //-----Wave 2------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 5; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        //-----Wave 3------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 25; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 8; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        w.Enemies.Add(EnemyType.Caballo);


        /******************
         ******Level 3*****
         ******************/

        l = new Level();
        _levels.Add(l);


        //-----Wave 1------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 6; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        for (int i = 0; i < 2; i++)
        {
            w.Enemies.Add(EnemyType.Caballo);
        }

        //-----Wave 2------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        for (int i = 0; i < 5; i++)
        {
            w.Enemies.Add(EnemyType.Caballo);
        }

        //-----Wave 3------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 15; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Caballo);
        }

        w.Enemies.Add(EnemyType.Torre);

        /******************
        ******Level 4*****
        ******************/

        l = new Level();
        _levels.Add(l);


        //-----Wave 1------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Caballo);
        }

        for (int i = 0; i < 2; i++)
        {
            w.Enemies.Add(EnemyType.Torre);
        }

        //-----Wave 2------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        for (int i = 0; i < 15; i++)
        {
            w.Enemies.Add(EnemyType.Caballo);
        }

        for (int i = 0; i < 5; i++)
        {
            w.Enemies.Add(EnemyType.Torre);
        }

        //-----Wave 3------
        w = new Wave();
        l.Waves.Add(w);
        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Peon);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Alfil);
        }

        for (int i = 0; i < 20; i++)
        {
            w.Enemies.Add(EnemyType.Caballo);
        }

        for (int i = 0; i < 10; i++)
        {
            w.Enemies.Add(EnemyType.Torre);
        }

        //-----Wave 4------
        for (int i = 0; i < 1; i++)
        {
            w.Enemies.Add(EnemyType.Rey);
        }

        //-----Wave 4------
        for (int i = 0; i < 1; i++)
        {
            w.Enemies.Add(EnemyType.Rey);
            w.Enemies.Add(EnemyType.Reina);
        }
        
        //-----Wave 5------
        w = new Wave();
        l.Waves.Add(w);
        w.Enemies.Add(EnemyType.Kasparov);
    }

    public void LoadRound()
    {
        for (int i = 0; i < _levels[_currentLevel].Waves[_currentRound].Enemies.Count; i++)
        {
            GameObject gop = _pool.GetEnemy(_levels[_currentLevel].Waves[_currentRound].Enemies[i]);
            gop.GetComponent<GameEntityController>().SetGameController(this);
            Vector2 position = Random.insideUnitCircle * MaxSpawnDistance;
            while (position.magnitude < MinSpawnDistance)
            {
                position = Random.insideUnitCircle * MaxSpawnDistance;
            }

            _waveEnemies.Add(gop);
            gop.transform.position = new Vector3(position.x, position.y);
        }
        //currentRound indica la wave

        if (_currentLevel == 3 && !_appliedBossMusic)
        {
            _appliedBossMusic = true;
            setOst(SoundsLoader.GetInstance().GetResource("Infected Mushroom - Spitfire [Monstercat Release]"));
        }

        WaveValue.text = (_currentRound + 1).ToString();
        LevelValue.text = (_currentLevel + 1).ToString();
        _remainingRoundItems = _levels[_currentLevel].Waves[_currentRound].Enemies.Count;
    }

    public void UpdateWaveCount()
    {
        _currentRound++;
        if (_currentRound == _levels[_currentLevel].Waves.Count)
        {
            _currentLevel++;
            _currentRound = 0;
        }

        if (_levels.Count > _currentLevel)
        {
            LoadRound();
        }
        else
        {
            //Game ended
        }
    }

    public void NotifyPlayerDied()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        playerDied = true;
        
    }

    public void InstantiateItem(ItemType type, Vector3 position)
    {
        GameObject go = _itemPool.GetItem();
        SpriteRenderer spr;
        go.transform.position = position;
        go.GetComponent<ItemController>().Type = type;
        spr = go.GetComponent<SpriteRenderer>();
        switch (type)
        {
            case ItemType.DamageUp:
                spr.color = Color.red;
                spr.sprite = SpritesLoader.GetInstance().GetResource("icons/Projectile");
                break;
            case ItemType.Heal:
                spr.color = Color.green;
                spr.sprite = SpritesLoader.GetInstance().GetResource("icons/Vida");
                break;
            case ItemType.SpeedShootUp:
                spr.color = Color.blue;
                spr.sprite = SpritesLoader.GetInstance().GetResource("icons/SpeedBullet");
                break;
            case ItemType.Shield:
                spr.color = Color.cyan;
                spr.sprite = SpritesLoader.GetInstance().GetResource("icons/Shield");
                break;
            case ItemType.Speed:
                spr.color = Color.black;
                spr.sprite = SpritesLoader.GetInstance().GetResource("icons/Boot");
                break;
        }
    }

	public void addScore(EnemyType enemyType){
		Debug.Log (enemyType);

		switch (enemyType)
		{
		case EnemyType.Peon:
			score += 10;
			break;
		case EnemyType.Alfil:
			score += 20;
			break;
		case EnemyType.Caballo:
			score += 40;
			break;
		case EnemyType.Torre:
			score += 80;
			break;
		case EnemyType.Reina:
			score += 160;
			break;
		case EnemyType.Rey:
			score += 160;
			break;
		case EnemyType.Kasparov:
			score += 9999;
			break;
		}

		PlayerPrefs.SetInt ("Score", score);

        //Debug.Log (PlayerPrefs.GetInt ("Score", 0));
        scoreUser.text = PlayerPrefs.GetInt("Score", 0).ToString();


    }


}