using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class AlfilBrain: AbstractBrain{
	public int RadioExterno = 9;
	public int RadioInterno = 4;
	public override Vector3 GetMovement(PlayerController player, GameEntityController enemy)
	{
		Vector3 position_with_radio = player.transform.position;
		Vector3 distancia = player.transform.position - enemy.transform.position;
		if (distancia.magnitude < RadioInterno) {
			return -distancia.normalized;
		} else if (distancia.magnitude > RadioExterno) {
			return distancia.normalized;
		} else {
			return MathUtils.Rotate(distancia.normalized,90)*2f/3f;
		}
	}
}