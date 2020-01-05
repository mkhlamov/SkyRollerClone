using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter Finish " + other.name + " " + other.tag);
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.StopPlayer();
            }
        }
    }
}