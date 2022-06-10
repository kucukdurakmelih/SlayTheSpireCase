using System;
using UnityEngine;


public class CardMover : MonoBehaviour
{
    private Card _card;
    private float activateYAxisThreshold = -1;

    private Camera mainCamera;
    
    private void Start()
    {
        _card = GetComponent<Card>();
        mainCamera = Camera.main;
    }

    private void OnMouseEnter()
    {
        EventManager.CardHighlighted?.Invoke(_card);
        _card.HighlightCard();
    }

    private void OnMouseExit()
    {
        EventManager.CardDeHighlighted?.Invoke(_card);
        _card.UnHighlightCard();
    }

    private void OnMouseDrag()
    {
        MoveCard();
    }

    private void OnMouseUp()
    {
        if(transform.position.y > activateYAxisThreshold)
            _card.CardUsed();
    }

    private void MoveCard()
    {
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePos, .7f);
    }
}