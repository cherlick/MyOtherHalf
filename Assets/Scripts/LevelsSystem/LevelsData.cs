using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyOtherHalf.LevelsSystem
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelsSystem/LevelData")]
    public class LevelsData : ScriptableObject
    {
        public ScenesNames sceneName;
        public ScenesNames nextSceneName;
        public LevelsTypes levelType;

        [Header("Puzzle type Data")]
        public int maxNumberOfSteps;

        [Header("Battle type Data")]
        public EnemiesToSpawn[] enemyes;
        public int numberOfSpanwPoints;
    }

    [Serializable]
    public class EnemiesToSpawn{
    public GameObject enemyPrefab;
    public int amountToSpawn;
}
}

