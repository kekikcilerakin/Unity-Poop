using Poop.Player;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private ItemSO requiredItem;

        public ItemSO GetRequiredItem()
        {
            return requiredItem;
        }
    }
}
