using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBrain {
    private Vector3 movement;
    private Vector3 shot;
	protected float TimeCounter = 0;
	public GameEntityController GEC;
	public InvokeEnemiesController IEC;
    public virtual Vector3 GetMovement(PlayerController player, GameEntityController enemy)
    {
        return Vector3DistanceObjectToPlayer(player.transform.position, enemy.transform.position);
    }

	public virtual Vector2[] GetShoot(PlayerController player, GameEntityController shot){
		Vector2 v = Vector2DistanceObjectToPlayer(player.transform.position, shot.transform.position);
		Vector2[] m = new Vector2[1];
		m [0] = v;
		return m;
	}

    protected Vector3 Vector3DistanceObjectToPlayer(Vector3 Destination, Vector3 Origin)
    {
        movement.x = Destination.x - Origin.x;
        movement.y = Destination.y - Origin.y;
		return movement.normalized;
    }

	protected Vector2 Vector2DistanceObjectToPlayer(Vector3 Destination, Vector3 Origin)
	{
		movement.x = Destination.x - Origin.x;
		movement.y = Destination.y - Origin.y;
		return new Vector2(movement.normalized.x, movement.normalized.y);
	}

	public virtual float GetSpeed(){
		return 1.0f;
	}

	public void UpdateTime(float Time){
		TimeCounter += Time;
	}

	public virtual EnemyType InvokeEnemies()
	{
		return EnemyType.Peon;
	}
	public virtual bool canDecrementHeal()
	{
		return true;
	}

	public virtual void voy_a_morir(){
	}
}