using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyOtherHalf.Tools;
using TMPro;
using System;

namespace MyOtherHalf.Core.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public static Action<float> OnUIStepsUpdate;
        public static Action<bool,bool> OnUIMergePanelUpdate;

        [SerializeField] private GameObject mergePanel;
        [SerializeField] private GameObject puzzlePanel;
        [SerializeField] private GameObject battlePanel;
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private TextMeshProUGUI stepsText;
        [SerializeField] private GameObject menuPanel;

        private void Awake() 
        {
            DisablePanels();
        }

        private void OnEnable() 
        {
            OnUIMergePanelUpdate += MergePanel;
            OnUIStepsUpdate += UIStepUpdate;
            GameManager.OnGameStateChange += ChangeModes;
        }

        private void OnDisable() 
        {
            OnUIMergePanelUpdate -= MergePanel;
            OnUIStepsUpdate -= UIStepUpdate;
            GameManager.OnGameStateChange -= ChangeModes;
        }

        private void DisablePanels()
        {
            mergePanel.SetActive(false);
            puzzlePanel.SetActive(false);
            battlePanel.SetActive(false);
            inputPanel.SetActive(false);
            //menuPanel.SetActive(false);
        }

        private void UIStepUpdate(float movements)
        {
            stepsText.text = $"Movements: {movements}";
        }

        private void MergePanel(bool isActive, bool isToActivateInput = false)
        {
            mergePanel.SetActive(isActive);
            inputPanel.SetActive(isToActivateInput);
        }

        private void ChangeModes(GameState state)
        {
            DisablePanels();
            Debug.Log($"Change UI to {state}");

            switch (state)
            {
                case GameState.StartMenu:
                break;

                case GameState.Pause:
                    //menuPanel.SetActive(true);
                break;

                case GameState.PuzzleMode:
                    puzzlePanel.SetActive(true);
                break;

                case GameState.BattleMode:
                    inputPanel.SetActive(true);
                break;
            }
        }
    }
}

