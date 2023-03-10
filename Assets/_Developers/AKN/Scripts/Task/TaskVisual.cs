using Poop.Manager;
using Poop.Player;
using Poop.Player.Inventory;
using System;
using UnityEngine;

namespace Poop
{
    public class TaskVisual : MonoBehaviour
    {
        private Task task;
        private Outline outline;

        [SerializeField] private float progress = 0;
        public event EventHandler OnTaskCompleted;

        private void Start()
        {
            task = transform.parent.GetComponent<Task>();
            outline = GetComponent<Outline>();

            PlayerController.Instance.InventoryController.OnItemInHandChanged += InventoryController_OnItemInHandChanged;
        }

        private void Update()
        {
            if (progress > task.GetCompleteTime()) return;

            if (InputManager.Instance.IsProgressing)
            {
                progress += Time.deltaTime;
                Debug.Log(Mathf.Ceil(progress) + "/" + task.GetCompleteTime());

                if (progress > task.GetCompleteTime())
                {
                    OnTaskCompleted?.Invoke(this, EventArgs.Empty);
                    Debug.Log("Task Completed");
                }
            }
            else
            {
                progress = 0;
            }
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
