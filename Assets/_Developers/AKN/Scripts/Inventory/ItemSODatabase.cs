using System.Collections.Generic;
using UnityEngine;

namespace Poop.Player.Inventory
{
    //[CreateAssetMenu(menuName = "Inventory/Item Database")]
    public class ItemSODatabase : ScriptableObject
    {
        public List<ItemSO> itemSOList;
    }
}