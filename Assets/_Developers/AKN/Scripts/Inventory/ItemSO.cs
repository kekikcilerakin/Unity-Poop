using UnityEngine;

namespace Poop.Player.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        public Transform prefab;
    }
}
