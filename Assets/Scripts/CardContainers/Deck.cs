using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    private List<Card> cards = new List<Card>();
    [SerializeField] private float cardDrawInterval;
    private float currentCardDrawTime;
    private int cardCountToDraw;


    private void ShuffleDeck()
    {
        var shuffledList = new List<Card>();

        var cardCount = cards.Count;
        for (var i = 0; i < cardCount; i++)
        {
            var index = Random.Range(0, cards.Count);
            shuffledList.Add(cards[index]);
            cards.RemoveAt(index);
        }

        cards = shuffledList;
    }
    private Vector3 PutCardToDeck(Card card)
    {
        cards.Add(card);
        return transform.position;
    }
    
    

    private void RequestCardDraw(int count)
    {
        cardCountToDraw += count;
    }

    private void Update()
    {
        CardDrawTimer();
    }

    private void CardDrawTimer()
    {
        if(cardCountToDraw == 0) return;

        if (currentCardDrawTime > 0)
        {
            currentCardDrawTime -= Time.deltaTime;
            return;
        }

        DrawCard();
    
    }

    private void DrawCard()
    {
        if (cards.Count == 0)
        {
            EventManager.PutAllDiscardedCardsToDeck?.Invoke();
        }
        cardCountToDraw--;
        currentCardDrawTime = cardDrawInterval;
        cards[0].CardDrawn();
        cards.RemoveAt(0);
    }

    private void OnEnable()
    {
        EventManager.ShuffleDeck = ShuffleDeck;
        EventManager.PutCardToDeck = PutCardToDeck;
        EventManager.DrawCards = RequestCardDraw;
    }
}