using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Poop
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
