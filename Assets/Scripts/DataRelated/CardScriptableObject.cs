using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "card", menuName = "card", order = 0)]
    public class CardScriptableObject : ScriptableObject
    {
        public string cardName;
        public int cardCost;
        public Sprite cardSprite;
        public CardType CardType;
    }
}