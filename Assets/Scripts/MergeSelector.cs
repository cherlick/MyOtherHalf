using UnityEngine;
using MyOtherHalf.Characters;

namespace MyOtherHalf.Core
{
    public class MergeSelector : MonoBehaviour
    {
        [SerializeField] private CharacterData characterData;

        public void SelectCharacter()
        {
            GameManager.OnMergeSelection?.Invoke(characterData);
        }
    }
}

