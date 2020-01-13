using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class Gem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.AddGem();
                Destroy(gameObject);
            }
        }
    }
}