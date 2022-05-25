using UnityEngine;

namespace MyOtherHalf.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public CharacterTypes playerCharacterType;
        public Sprite characterSprite;
        public float baseDamage;
        public float movementSpeed;
        public float baseMaxHealth;
        public Color backgroundLightColor;
    }

    public enum CharacterTypes
    {
        PlayerEvilHalf,
        PlayerGoodHalf,
        PlayerBalanceOne,
        PlayerEvilOne,
        PlayerGoodOne,
        Enemy,
        Boos
    }
    
}
