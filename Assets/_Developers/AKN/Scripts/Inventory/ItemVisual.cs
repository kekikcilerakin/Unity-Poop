using Poop.Player;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop
{
    public class ItemVisual : MonoBehaviour
    {
        private Item item;
        private Outline outline;

        private void Start()
        {
            item = transform.parent.GetComponent<Item>();
            outline = GetComponent<Outline>();
            PlayerController.Instance.OnHighlightedItemChanged += PlayerController_OnSelectedItemChanged;
        }

        private void PlayerController_OnSelectedItemChanged(object sender, PlayerController.OnHighlightedItemChangedEventArgs e)
        {
            if (e.highlightedItem == item)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            outline.enabled = true;
        }

        private void Hide()
        {
            outline.enabled = false;
        }
    }
}