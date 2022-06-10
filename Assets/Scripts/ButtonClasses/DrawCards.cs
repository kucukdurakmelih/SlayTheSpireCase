using UnityEngine;


public class DrawCards : MonoBehaviour
{
    [SerializeField] private int drawCardCount;
    public void DrawCard() => EventManager.DrawCards?.Invoke(drawCardCount);
}