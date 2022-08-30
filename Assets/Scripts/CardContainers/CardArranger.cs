using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class CardArranger : MonoBehaviour
{
    private List<ArrangeableCard> _arrangeableCards = new List<ArrangeableCard>();
    [SerializeField] private float distanceBetweenCards;
    [SerializeField] private float pushDistance;
    [SerializeField] private Transform[] points;
    
    public void RemoveCardFromArranger(Card card)
    {
        ArrangeableCard arrangeableCard = null;

        foreach (var arrangeable in _arrangeableCards)
        {
            if (arrangeable.card == card)
                arrangeableCard = arrangeable;
        }

        if (arrangeableCard != null)
            _arrangeableCards.Remove(arrangeableCard);
        UpdateTargetPositions();
    }

    public void ArrangeCard(Card card)
    {
        var newArrangeableCard = new ArrangeableCard
        {
            targetPos = Vector3.zero,
            card = card,
        };

        _arrangeableCards.Add(newArrangeableCard);
        UpdateTargetPositions();
    }

    private void UpdateTargetPositions()
    {
        var cardCount = _arrangeableCards.Count;
        var centerIndex = 0f;
        // The case of card count is even
        if (cardCount % 2 == 0)
            centerIndex = (float)cardCount / 2 - 0.5f;
        else
            centerIndex = Mathf.FloorToInt((float)cardCount / 2);

        for (int i = 0; i < cardCount; i++)
        {
            var arrangeableCard = _arrangeableCards[i];
            //arrangeableCard.card.UpdateOrderInLayer(i);
            var indexDifference = i - centerIndex;

            var distance = indexDifference * distanceBetweenCards;
            var time = 0.5f + distance;
            arrangeableCard.bezierTime = time;
            var targetPos = CalculateBezierCurve(time);
            arrangeableCard.targetPos = targetPos;
        }

        CheckHighlightedCard();
    }

    private void CheckHighlightedCard()
    {
        for (var i = 0; i < _arrangeableCards.Count; i++)
        {
            var arrangeableCard = _arrangeableCards[i];

            if (!arrangeableCard.isHighlighted) continue;
            if (i != 0)
            {
                var prevCard = _arrangeableCards[i - 1];
                var newTime = prevCard.bezierTime - pushDistance;
                prevCard.bezierTime = newTime;
                var newTargetPos = CalculateBezierCurve(newTime);
                prevCard.targetPos = newTargetPos;
            }

            if (i != _arrangeableCards.Count - 1)
            {
                var prevCard = _arrangeableCards[i + 1];
                var newTime = prevCard.bezierTime + pushDistance;
                prevCard.bezierTime = newTime;
                var newTargetPos = CalculateBezierCurve(newTime);
                prevCard.targetPos = newTargetPos;
            }
        }
    }

    private void ACardHighlighted(Card card)
    {
        foreach (var arrangeableCard in _arrangeableCards)
        {
            if (arrangeableCard.card == card)
                arrangeableCard.isHighlighted = true;
        }
        UpdateTargetPositions();
    }

    private void ACardDeHighlighted(Card card)
    {
        foreach (var arrangeableCard in _arrangeableCards)
        {
            if (arrangeableCard.card == card)
                arrangeableCard.isHighlighted = false;
        }
        UpdateTargetPositions();
    }

    private void Update()
    {
        PositionCards();
    }

    private void PositionCards()
    {
        foreach (var arrangeableCard in _arrangeableCards)
        {
            var trans = arrangeableCard.card.transform;
            var cardPos = trans.position;
            var pos = Vector3.Lerp(cardPos, arrangeableCard.targetPos, Time.deltaTime * 5);
            trans.position = pos;
        }
    }


    private void OnDrawGizmos()
    {
        SimulateBezierCurve();
    }

    private void SimulateBezierCurve()
    {
        for (float i = 0; i < 50; i++)
        {
            var time = (float)1 / 20 * i;
            var pos = CalculateBezierCurve(time);
            Gizmos.DrawSphere(pos, 5f);
        }
    }

    private Vector3 CalculateBezierCurve(float time)
    {
        Vector3 startPos = points[0].position;
        Vector3 midPos = points[1].position;
        Vector3 endPos = points[2].position;

        var b1 = Vector3.Lerp(startPos, midPos, time);
        var b2 = Vector3.Lerp(midPos, endPos, time);

        var curvePos = Vector3.Lerp(b1, b2, time);
        return curvePos;
    }


    private void OnEnable()
    {
        EventManager.CardHighlighted = ACardHighlighted;
        EventManager.CardDeHighlighted = ACardDeHighlighted;
    }
}