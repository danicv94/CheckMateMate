using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeEnemiesController:MonoBehaviour
{
	public float MinSpawnDistance;
	public float MaxSpawnDistance;
	private EnemyPool _pool;
    public Canvas time;

	public float InvokeRatio = 1;
	private float _cooldownCounter = 0;

	public void Start(){
		_pool = GameObject.Find("Pools").GetComponentInChildren<EnemyPool>();
	}
	public void Update(){
		_cooldownCounter += Time.deltaTime;
	}
	public void InvokeEnemy (EnemyType type_enemy)
	{
		if (InvokeRatio < _cooldownCounter) {
			GameObject gop = _pool.GetEnemy (type_enemy);
			Vector2 position = Random.insideUnitCircle * MaxSpawnDistance;
			while (position.magnitude < MinSpawnDistance) {
				position = Random.insideUnitCircle * MaxSpawnDistance;
			}
			gop.transform.position = new Vector3 (position.x, position.y);
			gop.GetComponent<GameEntityController> ().SetGameController (GameObject.Find ("GameController").GetComponent<GameController> ());
			_cooldownCounter = 0f;
		}
	}

	public void InvokeEnemy (EnemyType[] type_enemy)
	{
		for (int i = 0; i < type_enemy.Length; i++) {
			GameObject gop = _pool.GetEnemy (type_enemy[i]);
			Vector2 position = Random.insideUnitCircle * 50;
			while (position.magnitude < MinSpawnDistance) {
				position = Random.insideUnitCircle * MaxSpawnDistance;
			}
			gop.transform.position = new Vector3 (position.x, position.y);
			gop.GetComponent<GameEntityController> ().SetGameController (GameObject.Find ("GameController").GetComponent<GameController> ());

		}
			
		}
	}

