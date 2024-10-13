using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int totalCoins; // Gesamtzahl der M�nzen im Level
    public Animator targetAnimator; // Animator, der die Animation abspielen soll
    public string animationTriggerName = "PlayAnimation"; // Trigger f�r die Animation
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
        // Setze die Gesamtzahl der M�nzen
        collectedCoins = 0;
        // Die Gesamtzahl der M�nzen kann z.B. durch die Anzahl der Coin-Objekte im Level gesetzt werden.
        // totalCoins = FindObjectsOfType<Coin>().Length; // Optionale automatische Z�hlung
    }

    public void AddCoin()
    {
        collectedCoins++;
        OnCoinCollected?.Invoke();
        CheckAllCoinsCollected();
    }

    private void CheckAllCoinsCollected()
    {
        // �berpr�fe, ob alle M�nzen gesammelt wurden
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
