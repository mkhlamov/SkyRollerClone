using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("[Finish] OnTriggerEnter " + other.tag + " " + other.name);
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.SetGameWon();
            }
        }
    }
}