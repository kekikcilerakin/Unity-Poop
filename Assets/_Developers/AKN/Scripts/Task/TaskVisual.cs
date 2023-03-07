using Poop.Player;
using UnityEngine;

namespace Poop
{
    public class TaskVisual : MonoBehaviour
    {
        private Task task;
        private Outline outline;

        private void Start()
        {
            task = transform.parent.GetComponent<Task>();
            outline = GetComponent<Outline>();
           //PlayerController.Instance.OnHighlightedItemChanged += PlayerController_OnSelectedItemChanged;
        }
    }
}
