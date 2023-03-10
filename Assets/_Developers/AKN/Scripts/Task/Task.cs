using Poop.Manager;
using Poop.Player;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private ItemSO requiredItem;
        [SerializeField] private float completeTime;

        public ItemSO GetRequiredItem()
        {
            return requiredItem;
        }

        public float GetCompleteTime()
        {
            return completeTime;
        }

    }
}
