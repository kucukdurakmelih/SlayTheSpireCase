using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    private List<Card> cards;
    [SerializeField] private float cardDrawInterval;
    private float currentCardDrawTime;
    private int cardCountToDraw;


    private void ShuffleDeck()
    {
        var shuffledList = new List<Card>();

        for (int i = 0; i < cards.Count; i++)
        {
            var index = Random.Range(0, cards.Count);
            shuffledList.Add(cards[index]);
            shuffledList.RemoveAt(index);
        }

        cards = shuffledList;
    }
    private void PutCardToDeck(Card card)
    {
        card.CardPutToTheDeck(transform);
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
        if(cards.Count == 0) return;
        cardCountToDraw--;
        currentCardDrawTime = cardDrawInterval;
        EventManager.PutCardToHand(cards[0]);
        cards.RemoveAt(0);
    }

    private void OnEnable()
    {
        EventManager.ShuffleDeck = ShuffleDeck;
        EventManager.PutCardToDeck = PutCardToDeck;
        EventManager.DrawCards = RequestCardDraw;
    }
}