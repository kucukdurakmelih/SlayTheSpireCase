using System;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    [SerializeField] private DeckScriptableObject _deckScriptableObject;

    private void SetUpDeck()
    {
        foreach (var cardPrefab in _deckScriptableObject.cardsInTheDeck)
        {
            var card = Instantiate(cardPrefab).GetComponent<Card>();
            EventManager.PutCardToDeck?.Invoke(card);
        }

        EventManager.ShuffleDeck?.Invoke();
    }

    private void OnEnable()
    {
        EventManager.SetUpTheDeck = SetUpDeck;
    }
}