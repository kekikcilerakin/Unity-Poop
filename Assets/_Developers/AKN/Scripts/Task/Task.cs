using Poop.Player;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private ItemSO requiredItem;
        [SerializeField] private float completeTime;
        [SerializeField] private bool isActiveTask;

        public void Interact()
        {
            isActiveTask = true;
        }

        public ItemSO GetRequiredItem()
        {
            return requiredItem;
        }

        public float GetCompleteTime()
        {
            return completeTime;
        }

        public bool GetIsActiveTask()
        {
            return isActiveTask;
        }

        public void SetIsActiveTask(bool isActive)
        {
            isActiveTask = isActive;
        }
    }
}
