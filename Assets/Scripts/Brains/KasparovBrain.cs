using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasparovBrain: AbstractBrain{

	public float ShootRatio;
	private float _cooldownCounter;
	public int contador = 2;

	public KasparovBrain(){
		TimeCounter = 30;
	}
    public override Vector3 GetMovement(PlayerController player, GameEntityController enemy)
    {
    	return Vector3.zero;
	}

	public override EnemyType InvokeEnemies()
	{
		if (TimeCounter >= 30) {
			for (int i = 0; i < contador; i++) {
				Debug.Log ("123");
				EnemyPool pool = GameObject.Find ("Pools").GetComponentInChildren<EnemyPool> ();
				GameObject rey = pool.GetEnemy (EnemyType.Rey);
				GameObject reina = pool.GetEnemy (EnemyType.Reina);
				Vector2 position = Random.insideUnitCircle * 15;
				while (position.magnitude < 10) {
					position = Random.insideUnitCircle * 15;
				}
				rey.transform.position = new Vector3 (position.x, position.y);
				rey.GetComponent<GameEntityController> ().SetGameController (GameObject.Find ("GameController").GetComponent<GameController> ());
				position = Random.insideUnitCircle * 15;
				while (position.magnitude < 10) {
					position = Random.insideUnitCircle * 15;
				}
				reina.transform.position = new Vector3 (position.x, position.y);
				reina.GetComponent<GameEntityController> ().SetGameController (GameObject.Find ("GameController").GetComponent<GameController> ());


			}
			TimeCounter = 0f;
			contador += 2;
		}
		return EnemyType.Peon;
		}
}