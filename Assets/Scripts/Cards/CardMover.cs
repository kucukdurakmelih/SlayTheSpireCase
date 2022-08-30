using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class CardMover : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IDragHandler, IPointerUpHandler
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
        var mousePos = Input.mousePosition;
        transform.position = Vector3.Lerp(transform.position, mousePos, .7f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.CardHighlighted?.Invoke(_card);
        _card.HighlightCard();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.CardDeHighlighted?.Invoke(_card);
        _card.UnHighlightCard();
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveCard();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(transform.position.y > activateYAxisThreshold)
            _card.CardUsed();
    }
}