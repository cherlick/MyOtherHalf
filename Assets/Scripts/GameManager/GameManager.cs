using System;
using UnityEngine;
using MyOtherHalf.Characters;
using MyOtherHalf.Tools;
using MyOtherHalf.Core.UI;
using MyOtherHalf.Core.CameraSystem;
using MyOtherHalf.Core.LevelsSystem;

namespace MyOtherHalf.Core
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public static Action<CharacterData> OnMergeSelection;
        public static Action<GameState> OnGameStateChange;
        public static Action OnEnemyKilled;
        public static Action<float> OnStepsUpdated;
        public static Action OnLevelComplete;
        public static Action<GameObject> OnPortalStart;

        #endregion

        #region Private variables
        [SerializeField] private GameObject fullHeart;
        [SerializeField] private GameObject portal;

        private GameState currentGameState;
        private GameState previousGameState;
        private LevelManager levelManager;
        private float puzzleModeStepsCount;
        private float battleModeEnemiesCount;

        #endregion

        #region Properties
        
        public GameState GetCurrentGameState => currentGameState;
        public GameState GetPreviousGameState => previousGameState;

        #endregion

        #region Unity Methods
        private void Awake() 
        {
            currentGameState = GameState.StartMenu;
            ChangeState(GameState.StartMenu);
            levelManager = LevelManager.Instance;
            levelManager.ChangeScene(ScenesNames.StartScene);
        }

        private void OnEnable() 
        {
            HalfPartController.OnHalfCollision += Merge;
            HalfPartController.OnStepDone += StepUpdate;
            LevelManager.OnSceneChange += LevelChangeUpdate;
            OnMergeSelection += MergeSelection;
            OnLevelComplete += LevelComplete;
            OnPortalStart += SetCorrectPortal;
        }

        private void OnDisable() 
        {
            HalfPartController.OnHalfCollision -= Merge;
            HalfPartController.OnStepDone -= StepUpdate;
            LevelManager.OnSceneChange -= LevelChangeUpdate;
            OnMergeSelection -= MergeSelection;
            OnLevelComplete -= LevelComplete;
            OnPortalStart -= SetCorrectPortal;
        }

        #endregion

        #region Pivate Methods

        private void StepUpdate()
        {
            puzzleModeStepsCount += 0.5f;
            OnStepsUpdated?.Invoke(puzzleModeStepsCount);
            CheckForLosingCondition();
        }

        private void CheckForLosingCondition()
        {
            if (puzzleModeStepsCount >= 0)
            {
                LevelManager.OnRestartRequest?.Invoke();
            }
        }

        private void Merge(Vector2 position)
        {
            fullHeart.transform.position = position;
            UIManager.OnUIMergePanelUpdate?.Invoke(true, false);
        }

        private void MergeSelection(CharacterData newData)
        {
            fullHeart.GetComponent<PlayerController>().SetData = newData;
            UIManager.OnUIMergePanelUpdate?.Invoke(false,true);
            ChangeState(GameState.BattleMode);
            portal!.SetActive(true);
        }

        private void LevelChangeUpdate()
        {
            Debug.Log($"LevelChangeUpdate {levelManager.GetCurrentLevelType}");
            switch (levelManager.GetCurrentLevelType)
            {
                case LevelsTypes.Puzzles:
                    puzzleModeStepsCount = levelManager.GetCurrentLevelSteps;
                    ChangeState(GameState.PuzzleMode);
                    fullHeart.gameObject.SetActive(false);
                break;
                case LevelsTypes.Battle:
                    //battleModeEnemiesCount = levelManager.GetEnemiesCount;
                    ChangeState(GameState.BattleMode);
                break;
            }
            if (portal != null)
            {
                portal!.SetActive(false);
            }
            
        }

        private void SetCorrectPortal(GameObject newPortal) => portal = newPortal;

        private void ChangeState(GameState newState)
        {
            Debug.Log($"State Change to {newState}");
            previousGameState = currentGameState;
            currentGameState = newState;
            OnGameStateChange.Invoke(currentGameState);
        }

        private void LevelComplete()
        {
            // #TODO Saving scores and check if there is more levels
            levelManager.ChangeLevel();
        }

        #endregion

    }
}

