using UnityEngine;

namespace InventoryFramework
{
    public class PickupItem : MonoBehaviour
    {
        public Item item;
        public int amount;

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject);

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<ItemPickupHandler>().PickupItem(item, amount);
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<ItemPickupHandler>().PickupItem(item, amount);
                Destroy(this.gameObject);
            }
        }
    }
}

