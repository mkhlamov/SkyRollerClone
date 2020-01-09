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
        public event Action<int> OnLevelUpdated;

        [SerializeField]
        private Transform _startBlock;
        [SerializeField]
        private Transform _endBlock;

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

        public float GetProgress()
        {
            return (_playerMovement.gameObject.transform.position.z - _startBlock.position.z) / (_endBlock.position.z - _startBlock.position.z);
        }

        public GameState GetGameState()
        {
            return _currentGameState;
        }

        public int GetCurrentLevel()
        {
            return _currentLevel;
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