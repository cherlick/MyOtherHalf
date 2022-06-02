using UnityEngine;
using MyOtherHalf.Core.LevelsSystem;

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
