using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter Obstacle " + other.name + " " + other.tag);
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.GameLost();
            }
        }
    }
}