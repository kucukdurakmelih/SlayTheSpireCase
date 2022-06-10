using System;
using Unity.VisualScripting;
using UnityEngine;


public class EventManager
{
    public static Action ShuffleDeck;
    public static Func<Card, Vector3> PutCardToDeck;
    public static Action<int> DrawCards;

    public static Action<Card> PutCardToHand;
    public static Action<Card> RemoveFromHand;
    public static Action DiscardAllCards;

    public static Action SetUpTheDeck;

    public static Func<Card,Vector3> PutCardToDiscardPile;
    public static Action PutAllDiscardedCardsToDeck;

    public static Action<Card> CardHighlighted;
    public static Action<Card> CardDeHighlighted;
}