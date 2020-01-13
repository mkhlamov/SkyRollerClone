using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyRollerClone.UI
{
    [RequireComponent(typeof(Slider))]
    public class LevelProgressUpdater : MonoBehaviour
    {
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
        }

        void Update()
        {
            GameState curGameState = GameManager.Instance.GetGameState();
            if (curGameState == GameState.RUNNING || curGameState == GameState.WON)
            {
                _slider.value = GameManager.Instance.GetProgress();
            } else {
                _slider.value = 0f;
            }
        }
    }
}