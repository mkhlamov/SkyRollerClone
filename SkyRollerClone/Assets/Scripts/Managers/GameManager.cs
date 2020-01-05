using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Player;

namespace SkyRollerClone {
    public class GameManager : Singleton<GameManager>
    {
        private PlayerMovement _playerMovement;

        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        public void StopPlayer()
        {
            _playerMovement.SetSpeed(0f);
        }

        public void GameLost()
        {
            StopPlayer();
        }
    }
}