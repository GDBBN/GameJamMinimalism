using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public int totalCoins; // Gesamtzahl der Münzen im Level
    public Animator targetAnimator; // Animator für die Animation
    public string animationTriggerName = "PlayAnimation"; // Trigger für die Animation
    public event Action OnCoinCollected; // Event für das Münzsammeln

    public int collectedCoins; // Anzahl der gesammelten Münzen

    void Start()
    {
        // Anfangswert der gesammelten Münzen festlegen
        collectedCoins = 0;
    }

    public void AddCoin()
    {
        // Münze hinzufügen und prüfen, ob alle gesammelt sind
        collectedCoins++;
        OnCoinCollected?.Invoke();
        CheckAllCoinsCollected();
    }

    private void CheckAllCoinsCollected()
    {
        if (collectedCoins >= totalCoins)
        {
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(animationTriggerName);
        }
    }
}