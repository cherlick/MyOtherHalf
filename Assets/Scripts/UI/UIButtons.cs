using UnityEngine;
using MyOtherHalf.Core.LevelsSystem;
using MyOtherHalf.Core;

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
