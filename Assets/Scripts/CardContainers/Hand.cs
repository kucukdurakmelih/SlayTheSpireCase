using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private CardArranger _cardArranger;
    public List<Card> cardsAtHand { get; private set; }
    [SerializeField] private float discardInterval;
    private WaitForSeconds _waitForSeconds;
    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(discardInterval);
        cardsAtHand = new List<Card>();
    }

    private void PutCardToHand(Card card)
    {
        cardsAtHand.Add(card);
        _cardArranger.ArrangeCard(card);
        card.transform.parent = transform;
    }

    private void RemoveFromHand(Card card)
    {
        cardsAtHand.Remove(card);
        _cardArranger.RemoveCardFromArranger(card);
    }

    private void DiscardAllCards()
    {
        StartCoroutine(DiscardAll());
    }

    private IEnumerator DiscardAll()
    {
        EventManager.DiscardAllCards = null;
        
        var sideList = new List<Card>(cardsAtHand);
        foreach (var card in sideList)
        {
            card.PutCardToDiscardPile();
            RemoveFromHand(card);
            yield return _waitForSeconds;
        }

        EventManager.DiscardAllCards = DiscardAllCards;
    }

    private void OnEnable()
    {
        EventManager.PutCardToHand = PutCardToHand;
        EventManager.RemoveFromHand = RemoveFromHand;
        EventManager.DiscardAllCards = DiscardAllCards;
    }

}
