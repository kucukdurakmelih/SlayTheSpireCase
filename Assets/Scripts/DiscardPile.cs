using System;
using System.Collections.Generic;
using UnityEngine;


public class DiscardPile : MonoBehaviour
{
    private List<Card> discardedCards = new List<Card>();

    private void DiscardCard(Card card)
    {
        discardedCards.Add(card);
    }

    private void PutAllDiscardedCardsToDeck()
    {
        foreach (var discardedCard in discardedCards)
        {
            EventManager.PutCardToDeck(discardedCard);
        }
    }


    private void OnEnable()
    {
        EventManager.PutCardToDiscardPile = DiscardCard;
        EventManager.PutAllDiscardedCardsToDeck = PutAllDiscardedCardsToDeck;
    }
}