using Unity.Netcode;
using UnityEngine;

namespace Poop.Network
{
    public class NetworkUI : MonoBehaviour
    {
        public void Host()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void Client()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
