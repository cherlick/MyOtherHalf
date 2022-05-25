using System;
using UnityEngine;
using MyOtherHalf.LevelsSystem;

namespace MyOtherHalf.UI
{
    public class UIButtons : MonoBehaviour
    {
        [SerializeField] private ScenesNames sceneNameToLoad;

        public void ButtonPress()
        {
            LevelManager.Instance.ChangeScene(sceneNameToLoad);
        }
    }
}
