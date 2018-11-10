using Enums;
using Pool;
using UnityEngine;

public class ItemController : MonoBehaviour
{

	public ItemType Type;
	public PowerUpPool Pool;

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerController>().ApplyUpgrade(Type);
			Pool.FreeItem(gameObject);
		}
	}
}
