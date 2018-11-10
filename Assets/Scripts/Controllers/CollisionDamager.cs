using UnityEngine;

namespace Controllers
{
    public class CollisionDamager : MonoBehaviour
    {
        public int CollitionDamage;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().DecrementHealth(CollitionDamage);
            }
        }
    }
}