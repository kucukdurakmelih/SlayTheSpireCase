using System;
using System.Collections.Generic;
using UnityEngine;


public class DiscardPile : MonoBehaviour
{
    private List<Card> discardedCards = new List<Card>();

    private Vector3 DiscardCard(Card card)
    {
        discardedCards.Add(card);
        return transform.position;
    }

    private void PutAllDiscardedCardsToDeck()
    {
        if(discardedCards.Count == 0) return;
        foreach (var discardedCard in discardedCards)
        {
            discardedCard.CardPutToTheDeck();
        }
        discardedCards.Clear();
        
        EventManager.ShuffleDeck?.Invoke();
    }


    private void OnEnable()
    {
        EventManager.PutCardToDiscardPile = DiscardCard;
        EventManager.PutAllDiscardedCardsToDeck = PutAllDiscardedCardsToDeck;
    }
}