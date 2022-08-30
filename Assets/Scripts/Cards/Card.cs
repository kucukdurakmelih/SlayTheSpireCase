using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] private CardScriptableObject _scriptableObject;
    internal string cardName;
    internal int cardCost;
    internal Sprite cardSprite;
    internal CardType cardType;

    internal Image _image;


    //public void UpdateOrderInLayer(int order) => _spriteRenderer.sortingOrder = order;

    public void InitCard()
    {
        cardName = _scriptableObject.cardName;
        cardCost = _scriptableObject.cardCost;
        cardSprite = _scriptableObject.cardSprite;
        cardType = _scriptableObject.CardType;

        _image = GetComponent<Image>();
        _image.sprite = cardSprite;
    }

    public virtual void CardPutToTheDeck()
    {
        var deckPosition = EventManager.PutCardToDeck(this);
        transform.position = deckPosition;
        gameObject.SetActive(false);
    }

    public virtual void CardDrawn()
    {
        EventManager.PutCardToHand?.Invoke(this);
        gameObject.SetActive(true);
        transform.localScale = new Vector3(.2f, .2f, .2f);
        var targetScale = new Vector3(1, 1, 1);
        transform.DOScale(targetScale, .5f);
    }

    public virtual void CardUsed()
    {
        EventManager.RemoveFromHand?.Invoke(this);
        CardDestroyed();
    }

    public virtual void CardDestroyed()
    {
        PutCardToDiscardPile();
    }
    

    internal void PutCardToDiscardPile()
    {
        var discardPilePos = EventManager.PutCardToDiscardPile(this);
        transform.DOMove(discardPilePos, 0.5f);
        var targetScale = new Vector3(.2f, .2f, .2f);
        transform.DOScale(targetScale, .5f).OnComplete(() => { gameObject.SetActive(false); });
    }

    public void HighlightCard()
    {
        var targetScale = new Vector3(1.2f, 1.2f, 1.2f);
        transform.localScale = targetScale;
    }

    public void UnHighlightCard()
    {
        var targetScale = new Vector3(1, 1, 1);
        transform.localScale = targetScale;
    }
}