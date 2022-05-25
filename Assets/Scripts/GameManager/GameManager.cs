using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyOtherHalf.Characters;
using MyOtherHalf.LevelsSystem;
using MyOtherHalf.Tools;
using System;

namespace MyOtherHalf.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public static Action<CharacterData> OnMergeSelection;
        [SerializeField] private GameObject fullHeart;
        [SerializeField] private GameObject mergePanel;
        [SerializeField] private GameObject puzzlePanel;
        [SerializeField] private GameObject battlePanel;
        [SerializeField] private GameObject inputPanel;

        private GameState currentGameState;
        private GameState previousGameState;
        private float puzzleModeStepsCount;
        

        public GameState GetCurrentGameState => currentGameState;
        public GameState GetPreviousGameState => previousGameState;

        private void Awake() 
        {
            currentGameState = GameState.StartMenu;
            ChangeState(GameState.StartMenu);
            LevelManager.Instance.ChangeScene(ScenesNames.StartScene);
        }

        private void OnEnable() 
        {
            HalfPartController.OnHalfCollision += Merge;
            HalfPartController.OnStepDone += StepUpdate;
            OnMergeSelection += MergeSelection;
        }

        

        public void ChangeState(GameState newState)
        {
            previousGameState = currentGameState;
            currentGameState = newState;
        }

        private void StepUpdate()
        {
            
        }

        private void Merge(Vector2 position)
        {
            fullHeart.transform.position = position;
            mergePanel.SetActive(true);
        }

        private void MergeSelection(CharacterData newData)
        {
            mergePanel.SetActive(false);
            fullHeart.GetComponent<PlayerController>().SetData = newData;
            inputPanel.SetActive(true);
        }


    }
}

