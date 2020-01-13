using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class Gem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.AddGem();
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, audio.clip.length);
            }
        }
    }
}