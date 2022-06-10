using UnityEngine;


    public class DiscardAllCards : MonoBehaviour
    {
        public void DiscardAll() => EventManager.DiscardAllCards?.Invoke();
    }
