using UnityEngine;

namespace Poop.Player.Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO item;
        private SphereCollider sphereCollider;

        private void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        public ItemSO GetItem()
        {
            return item;
        }

        public void Interact()
        {
            Debug.Log("Hot damn" + item.itemName);
            PlayerController.Instance.InventoryController.SetItemInHand(this);
        }

        public void ShowCollider()
        {
            sphereCollider.enabled = true;
        }

        public void HideCollider()
        {
            sphereCollider.enabled = false;
        }
    }
}
