using UnityEngine;

namespace Poop.Player.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private Item itemInHand;



        public void SetItemInHand(Item item)
        {
            if (itemInHand == null)
            {
                itemInHand = item;
                item.transform.SetParent(handTransform);
                item.transform.localPosition = Vector3.zero;
                item.HideCollider();
            }
            else //swap items
            {
                Vector3 newItemPosition = item.transform.position;

                itemInHand.transform.SetParent(null);
                itemInHand.transform.SetPositionAndRotation(newItemPosition, Quaternion.identity);
                itemInHand.ShowCollider();

                item.transform.SetParent(handTransform);
                item.transform.localPosition = Vector3.zero;

                itemInHand = item;
                itemInHand.HideCollider();
            }
        }

        public void DropItem()
        {
            itemInHand.transform.SetParent(null);
            itemInHand.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            itemInHand.ShowCollider();

            itemInHand = null;
        }

        public Item GetItemInHand()
        {
            return itemInHand;
        }
    }
}
