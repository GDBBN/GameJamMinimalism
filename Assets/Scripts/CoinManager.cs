using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public int totalCoins;
    public Animator targetAnimator;
    public string animationTriggerName = "PlayAnimation";
    public event Action OnCoinCollected;

    public int collectedCoins;

    private void Start()
    {
        collectedCoins = 0;
    }

    public void AddCoin()
    {
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