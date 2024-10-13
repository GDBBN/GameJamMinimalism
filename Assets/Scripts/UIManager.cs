using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text coinText; // Das Textfeld für die Anzeige der Münzen
    public CoinManager coinManager; // Referenz auf den CoinManager der Szene

    void Start()
    {
        // Wenn kein CoinManager zugewiesen ist, wird versucht, ihn in der Szene zu finden
        if (coinManager == null)
        {
            coinManager = FindObjectOfType<CoinManager>();
        }

        if (coinManager != null)
        {
            // Initiale UI-Aktualisierung
            UpdateCoinUI();

            // Den CoinManager abonnieren, um Änderungen zu überwachen
            coinManager.OnCoinCollected += UpdateCoinUI;
        }
        else
        {
            Debug.LogError("CoinManager not found in the scene.");
        }
    }

    void OnDestroy()
    {
        // Abonnement vom CoinManager entfernen, falls vorhanden
        if (coinManager != null)
        {
            coinManager.OnCoinCollected -= UpdateCoinUI;
        }
    }

    // Diese Methode wird bei jeder Münzenaktualisierung aufgerufen
    public void UpdateCoinUI()
    {
        int coinCount = coinManager.collectedCoins;
        coinText.text = "Coins: " + coinCount + " / " + coinManager.totalCoins;
    }
}