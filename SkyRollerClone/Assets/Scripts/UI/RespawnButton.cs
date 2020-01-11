using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class RespawnButton : MonoBehaviour
    {
        public void Respawn()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.Respawn();
            }
            else
            {
                Debug.LogError("GameManager not found");
            }
        }
    }
}