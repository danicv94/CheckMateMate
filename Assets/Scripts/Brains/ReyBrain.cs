using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReyBrain: AbstractBrain{
	public bool primera_vida = true;
	public bool invulnerable = false;
	public ReyBrain(GameEntityController Game_entity_controller, InvokeEnemiesController Invoke_enemies_controller){
		GEC = Game_entity_controller;
		IEC = Invoke_enemies_controller;
	}

	public override Vector3 GetMovement(PlayerController player, GameEntityController enemy)
	{
		return Vector3DistanceObjectToPlayer(player.transform.position, enemy.transform.position);
	}

	public override void voy_a_morir(){
		if ((float)GEC.getCurrentHealth ()-1 <= GEC.getGameController().GetPlayerController().GunController.BaseDamage && primera_vida) {
			invulnerable = true;
			GEC.gameObject.transform.position = new Vector2 (0, 0);
			GEC.SetHealth(GEC.health_init / 2);
			GEC.speed_movement = 0;
			GEC.have_gun = true;
            primera_vida = false;
		}
        if(!primera_vida) {
            invulnerable = false;
        }
	}

	private EnemyType _WhichEnemyInvoke(int typo_de_invocacion, int random){
		switch (typo_de_invocacion) {
		case 2:
			IEC.InvokeRatio = 0.75f;
			if (random >= 6) {
				return EnemyType.Alfil;
			} else {
				return EnemyType.Peon;
			}
			break;
		case 3:
			IEC.InvokeRatio = 0.5f;
			if (random < 5) {
				return EnemyType.Peon;
			} else if (random < 8) {
				return EnemyType.Alfil;
			} else {
				return EnemyType.Caballo;
			}
			break;
		case 4:
			IEC.InvokeRatio = 0.3f;
			if (random == 0) {
				return EnemyType.Torre;
			} else if (random <= 2) {
				return EnemyType.Caballo;
			} else if (random <= 5) {
				return EnemyType.Alfil;
			} else {
				return EnemyType.Peon;
			}
			break;
		default:
			return EnemyType.Peon;
			break;
		}
	}

	public override EnemyType InvokeEnemies ()
	{
		int currentHealth = GEC.getCurrentHealth ();
		int random = Random.Range (0, 10);
		int typeInvokeEnemy = 0;
		if (primera_vida) {
			if (currentHealth >= (GEC.health_init * 0.75)) {
				typeInvokeEnemy = 1;
			} else if (currentHealth >= (GEC.health_init * 0.50)) {
				typeInvokeEnemy = 2;
			} else if (currentHealth >= (GEC.health_init * 0.25)) {
				typeInvokeEnemy = 3;
			} else if (currentHealth >= GEC.health_init * 0.1) {
				typeInvokeEnemy = 4;
			}
			return _WhichEnemyInvoke (typeInvokeEnemy, random);
		}else {
			return _WhichEnemyInvoke (4, random);
		}
	}

	public override bool canDecrementHeal()
	{
		return !invulnerable;
	}

	public static Vector2 Rotate(Vector2 v, float degrees)
	{
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}

	public override Vector2[] GetShoot(PlayerController player, GameEntityController shot)
	{
		int num_disparos = 20;
		Vector2[] array_distancia = new Vector2[num_disparos];
		Vector2 lookAt = new Vector2(shot.transform.position.x, shot.transform.position.y).normalized;

		for (int i = 0; i < num_disparos; i++) {
			array_distancia [i] = Rotate (lookAt, (i - num_disparos / 2) * 22.5f);
		}
		array_distancia = new Vector2[8];
		array_distancia [0] = new Vector2 (Mathf.Sin(TimeCounter),Mathf.Cos(TimeCounter));
		array_distancia [1] = new Vector2 (Mathf.Sin(TimeCounter+Mathf.PI/2),Mathf.Cos(TimeCounter+Mathf.PI/2));
		array_distancia [2] = new Vector2 (Mathf.Sin(TimeCounter+Mathf.PI),Mathf.Cos(TimeCounter+Mathf.PI));
		array_distancia [3] = new Vector2 (Mathf.Sin(TimeCounter+Mathf.PI*3/2),Mathf.Cos(TimeCounter+Mathf.PI*3/2));

		array_distancia [4] = new Vector2 (Mathf.Sin(-TimeCounter),Mathf.Cos(-TimeCounter));
		array_distancia [5] = new Vector2 (Mathf.Sin(-TimeCounter+Mathf.PI/2),Mathf.Cos(-TimeCounter+Mathf.PI/2));
		array_distancia [6] = new Vector2 (Mathf.Sin(-TimeCounter+Mathf.PI),Mathf.Cos(-TimeCounter+Mathf.PI));
		array_distancia [7] = new Vector2 (Mathf.Sin(-TimeCounter+Mathf.PI*3/2),Mathf.Cos(-TimeCounter+Mathf.PI*3/2));
		return array_distancia;
	}
}