using UnityEngine;

namespace Poop.Player.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private Item itemInHand;

        public void SetItemInHand(Item item)
        {
            if (item == null)
            {
                itemInHand = null;
                return;
            }

            if (itemInHand == null)
            {
                itemInHand = item;
                itemInHand.ParentToHand();
                return;
            }

            //Swap Items
            Vector3 newItemPosition = item.transform.position;

            itemInHand.SwapPosition(newItemPosition);
            itemInHand = item;
            item.ParentToHand();
        }

        public void DropItem()
        {
            itemInHand.UnparentFromHand();
            SetItemInHand(null);
        }

        public Item GetItemInHand()
        {
            return itemInHand;
        }

        public Transform GetHandTransform()
        {
            return handTransform;
        }
    }
}
