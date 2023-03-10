using Poop.Player;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop
{
    public class TaskVisual : MonoBehaviour
    {
        private Task task;
        private Outline outline;

        [SerializeField] private float progress = 0;

        private void Start()
        {
            task = transform.parent.GetComponent<Task>();
            outline = GetComponent<Outline>();

            PlayerController.Instance.InventoryController.OnItemInHandChanged += InventoryController_OnItemInHandChanged;
            task.OnActivePlayerChanged += Task_OnActivePlayerChanged;
        }

        private void Task_OnActivePlayerChanged(object sender, Task.OnActivePlayerChangedEventArgs e)
        {
            Debug.Log($"ActivePlayerChanged to {e.ActivePlayer}");
            if (e.ActivePlayer == null)
            {
                progress = 0;
                Debug.Log($"Progress set to {progress}");
            }
        }

        private void HandleProgress()
        {
            if (progress > task.GetCompleteTime()) return;

            if (task.GetActivePlayer())
            {
                progress += Time.deltaTime;
                Debug.Log((progress / task.GetCompleteTime()).ToString("P0"));

                if (progress > task.GetCompleteTime())
                {
                    Debug.Log("Task Completed");
                }
            }
        }

        private void Update()
        {
            HandleProgress();
        }

        private void InventoryController_OnItemInHandChanged(object sender, InventoryController.OnItemInHandChangedEventArgs e)
        {
            if (task.GetRequiredItem() == e.ItemInHand)
            {
                ShowOutline();
            }
            else
            {
                HideOutline();
            }

        }

        private void ShowOutline()
        {
            outline.enabled = true;
        }

        private void HideOutline()
        {
            outline.enabled = false;
        }
    }
}
