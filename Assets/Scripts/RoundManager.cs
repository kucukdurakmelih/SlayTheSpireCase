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
    }

    private void RoundStarted()
    {
        // draw cards
        
    }


    private void RoundEnded()
    {
        // discard all cards
    }
    
    
}