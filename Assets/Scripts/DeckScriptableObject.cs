using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "deck", menuName = "deck", order = 0)]
public class DeckScriptableObject : ScriptableObject
{
    public List<GameObject> cardsInTheDeck;
}