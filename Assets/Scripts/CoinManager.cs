using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int totalCoins; // Gesamtzahl der Münzen im Level
    public Animator targetAnimator; // Animator, der die Animation abspielen soll
    public string animationTriggerName = "PlayAnimation"; // Trigger für die Animation
    public event Action OnCoinCollected;

    public int collectedCoins { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Setze die Gesamtzahl der Münzen
        collectedCoins = 0;
        // Die Gesamtzahl der Münzen kann z.B. durch die Anzahl der Coin-Objekte im Level gesetzt werden.
        // totalCoins = FindObjectsOfType<Coin>().Length; // Optionale automatische Zählung
    }

    public void AddCoin()
    {
        collectedCoins++;
        OnCoinCollected?.Invoke();
        CheckAllCoinsCollected();
    }

    private void CheckAllCoinsCollected()
    {
        // Überprüfe, ob alle Münzen gesammelt wurden
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
