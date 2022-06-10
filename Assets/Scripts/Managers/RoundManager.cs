using System;
using UnityEngine;


public class RoundManager : MonoBehaviour
{
    private void Start()
    {
        MatchStarted();
    }

    private void MatchStarted()
    {
        EventManager.SetUpTheDeck();
        RoundStarted();
    }

    private void RoundStarted()
    {
        // draw cards
        EventManager.DrawCards?.Invoke(5);

    }


    private void RoundEnded()
    {
        // discard all cards
    }
    
    
}