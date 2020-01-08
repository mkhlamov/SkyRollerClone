using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Player;
using System;

namespace SkyRollerClone {
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnGameWon;
        public event Action OnGameLost;
        public event Action OnNotStarted;

        private PlayerMovement _playerMovement;
        private PlayerController _playerController;

        private int _currentLevel = 0;
        private GameState _currentGameState = GameState.NOTSTARTED;

        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerController = FindObjectOfType<PlayerController>();
            _currentGameState = GameState.NOTSTARTED;
            OnNotStarted?.Invoke();
        }

        public void GameWon()
        {
            _currentGameState = GameState.WON;
            OnGameWon?.Invoke();
        }

        public void GameLost()
        {
            _currentGameState = GameState.LOST;
            OnGameLost?.Invoke();
        }
    }

    public enum GameState
    {
        NOTSTARTED,
        RUNNING,
        LOST,
        WON
    }
}