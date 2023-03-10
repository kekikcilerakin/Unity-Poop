using Poop.Player;
using Poop.Player.Inventory;
using System;
using UnityEngine;

namespace Poop
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private ItemSO requiredItem;
        [SerializeField] private float completeTime;

        [SerializeField] private PlayerController activePlayer;
        public event EventHandler<OnActivePlayerChangedEventArgs> OnActivePlayerChanged;
        public class OnActivePlayerChangedEventArgs : EventArgs
        {
            public PlayerController ActivePlayer;
        }

        public ItemSO GetRequiredItem()
        {
            return requiredItem;
        }

        public float GetCompleteTime()
        {
            return completeTime;
        }

        public PlayerController GetActivePlayer()
        {
            return activePlayer;
        }

        public void SetPlayer(PlayerController player)
        {
            this.activePlayer = player;

            if (player == null)
            {
                this.activePlayer = null;
            }

            OnActivePlayerChanged?.Invoke(this, new OnActivePlayerChangedEventArgs
            {
                ActivePlayer = activePlayer
            });
        }


    }
}
