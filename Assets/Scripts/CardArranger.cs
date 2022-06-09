using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class CardArranger : MonoBehaviour
{
    [SerializeField] private float distanceBetweenCards;
    [SerializeField] private List<Card> cards;
    [SerializeField] private Transform[] points;
    public Hand hand;


    private void PositionCards()
    {
        
    }

    private void OnDrawGizmos()
    {
        for (int i = 1; i <= 20; i++)
        {
            var time = (float)i / 20;

            var startPos = points[0].position;
            var midPos = points[1].position;
            var endPos = points[2].position;
            var pos = CalculateBezierCurve(startPos,midPos,endPos, time);
            
            Gizmos.DrawSphere(pos, .2f);
        }
        
    }

    private Vector2 CalculateBezierCurve(Vector2 startPos, Vector2 midPos, Vector2 endPos, float time)
    {
        var b1 = Vector2.Lerp(startPos, midPos, time);
        var b2 = Vector2.Lerp(midPos, endPos, time);

        var curvePos = Vector2.Lerp(b1, b2, time);
        return curvePos;
    }
}