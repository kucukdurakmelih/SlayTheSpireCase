using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private CardArranger _cardArranger;
    public List<Card> cardsAtHand { get; private set; }

    private void Start()
    {
        cardsAtHand = new List<Card>();
        _cardArranger.hand = this;
    }

    private void PutCardToHand(Card card)
    {
        cardsAtHand.Add(card);
        card.CardDrawn();
    }

    private void OnEnable()
    {
        EventManager.PutCardToHand = PutCardToHand;
    }

}
