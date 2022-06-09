using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public class Card : MonoBehaviour
{
    [SerializeField] private CardScriptableObject _scriptableObject;
    internal string cardName;
    internal int cardCost;
    internal Sprite cardSprite;
    internal CardType cardType;
    private  bool isExhaustable;

    private void InitCard()
    {
        cardName = _scriptableObject.cardName;
        cardCost = _scriptableObject.cardCost;
        cardSprite = _scriptableObject.cardSprite;
        cardType = _scriptableObject.CardType;
        isExhaustable = _scriptableObject.isExhaustable;

        var rend = GetComponent<SpriteRenderer>();
        rend.sprite = cardSprite;
    }

    public virtual void CardPutToTheDeck(Transform deckTransform)
    {
        transform.position = deckTransform.position;
        gameObject.SetActive(false);
        InitCard();
        
    }

    public virtual void CardDrawn()
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(.2f, .2f, .2f);
        var targetScale = new Vector3(1, 1, 1);
        transform.DOScale(targetScale, .5f);

    }

    public virtual void CardUsed()
    {
        
    }

    public virtual void CardDestroyed()
    {
        if(isExhaustable)
            PutCardToExhaustPile();
        else
            PutCardToDiscardPile();
    }

    public virtual void PutCardToExhaustPile()
    {
        
    }

    public virtual void PutCardToDiscardPile()
    {
        EventManager.PutCardToDiscardPile?.Invoke(this);
    }
    
    
}