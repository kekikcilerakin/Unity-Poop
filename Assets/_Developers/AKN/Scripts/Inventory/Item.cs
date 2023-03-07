using UnityEngine;

namespace Poop.Player.Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;

        public ItemSO GetItem()
        {
            return itemSO;
        }

        public void Interact()
        {
            Debug.Log("Hot damn" + gameObject.name);
        }
    }
}
