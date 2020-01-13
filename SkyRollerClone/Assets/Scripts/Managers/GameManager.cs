using System;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Player;
using SkyRollerClone.Input;

namespace SkyRollerClone {
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnGameWon;
        public event Action OnGameLost;
        public event Action OnNotStarted;
        public event Action OnGameRunning;
        public event Action<int> OnLevelUpdated;
        public event Action<List<Vector3>> OnLevelBuilt;
        public event Action<int> OnGemScoreChanged;

        [SerializeField]
        private List<Vector3> _waypoints;
        [SerializeField]
        private List<LevelInfo> _levelInfos;

        private PlayerMovement _playerMovement;
        private PlayerController _playerController;
        private LevelBuilder _levelBuilder;

        [SerializeField]
        private int _currentLevel = 0;
        private GameState _currentGameState = GameState.NOTSTARTED;
        private int _gemScore = 0;

        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerController = FindObjectOfType<PlayerController>();
            _levelBuilder = FindObjectOfType<LevelBuilder>();

            LoadData();
            RebuildCurrentLevel();
        }

        private void OnEnable()
        {
            SwipeDetector.OnSwipe += SwipeHandler;
        }

        private void OnDisable()
        {
            SwipeDetector.OnSwipe -= SwipeHandler;
            PlayerPrefsManager.Save();
        }

        #region Public Methods
        public void SetGameWon()
        {
            _currentGameState = GameState.WON;
            PlayerPrefsManager.SetLevel((_currentLevel + 1) % _levelInfos.Count);
            OnGameWon?.Invoke();
        }

        public void SetGameLost()
        {
            _currentGameState = GameState.LOST;
            OnGameLost?.Invoke();
        }

        public float GetProgress()
        {
            return (_playerMovement.GetPassedDist()) / (_levelBuilder.GetLevelLength());
        }

        public GameState GetGameState()
        {
            return _currentGameState;
        }

        public void StartNewLevel()
        {
            if (_currentLevel == _levelInfos.Count - 1)
            {
                _currentLevel = 0;
            }
            else
            {
                _currentLevel += 1;
            }
            PlayerPrefsManager.SetLevel(_currentLevel);
            OnLevelUpdated?.Invoke(_currentLevel);

            RebuildCurrentLevel();
        }

        public void RebuildCurrentLevel()
        {
            BuildLevel();
            SetGameNotStarted();
        }

        public void Respawn()
        {
            SetGameNotStarted();
        }

        public int GetCurrentLevel()
        {
            return _currentLevel;
        }

        public void AddGem()
        {
            _gemScore += 1;
            PlayerPrefsManager.SetGems(_gemScore);
            OnGemScoreChanged?.Invoke(_gemScore);
        }
        #endregion

        #region Private Methods
        private void SetGameNotStarted()
        {
            _currentGameState = GameState.NOTSTARTED;
            _playerMovement.StopPlayer();
            _playerController.GoToStart();
            OnNotStarted?.Invoke();
        }

        private void GameRunning()
        {
            _currentGameState = GameState.RUNNING;
            _playerMovement.StartMoving();
            OnGameRunning?.Invoke();
        }

        private void SwipeHandler(SwipeData d)
        {
            if (_currentGameState == GameState.NOTSTARTED)
            {
                GameRunning();
            }
        }

        private bool BuildLevel()
        {
            _waypoints = _levelBuilder.BuildLevel(_levelInfos[_currentLevel]);
            if (_waypoints == null)
            {
                Debug.LogError("[GameManager] Couldn't biuld level " + (_currentLevel));
            }
            OnLevelBuilt?.Invoke(_waypoints);
            return _waypoints != null;
        }

        private void LoadData()
        {
            _currentLevel = PlayerPrefsManager.GetLevel();
            _gemScore = PlayerPrefsManager.GetGems();
            OnLevelUpdated?.Invoke(_currentLevel);
            OnGemScoreChanged?.Invoke(_gemScore);
        }
        #endregion
    }

    public enum GameState
    {
        NOTSTARTED,
        RUNNING,
        LOST,
        WON
    }
}