using System;
using Unity.VisualScripting;
using UnityEngine;


public class EventManager
{
    public static Action ShuffleDeck;
    public static Action<Card> PutCardToDeck;
    public static Action<int> DrawCards;

    public static Action<Card> PutCardToHand;
    public static Action DiscardAllCards;

    public static Action SetUpTheDeck;

    public static Action<Card> PutCardToDiscardPile;
    public static Action PutAllDiscardedCardsToDeck;
}