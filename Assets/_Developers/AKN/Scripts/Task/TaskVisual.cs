using Poop.Player;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop
{
    public class TaskVisual : MonoBehaviour
    {
        private Task task;
        private Outline outline;
        public InventoryController inventoryController;

        private void Start()
        {
            task = transform.parent.GetComponent<Task>();
            outline = GetComponent<Outline>();
            inventoryController = PlayerController.Instance.InventoryController;
            inventoryController.OnItemInHandChanged += InventoryController_OnItemInHandChanged;
        }

        private void InventoryController_OnItemInHandChanged(object sender, InventoryController.OnItemInHandChangedEventArgs e)
        {
            if (task.GetRequiredItem() == e.ItemInHand)
            {
                Debug.Log(gameObject, gameObject);
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }

        }
    }
}
