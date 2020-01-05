using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Player;

namespace SkyRollerClone {
    public class GameManager : Singleton<GameManager>
    {
        private PlayerMovement _playerMovement;
        private PlayerController _playerController;

        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerController = FindObjectOfType<PlayerController>();
        }

        public void StopPlayer()
        {
            _playerMovement.SetSpeed(0f);
        }

        public void GameLost()
        {
            _playerController.ToggleDead();
        }
    }
}