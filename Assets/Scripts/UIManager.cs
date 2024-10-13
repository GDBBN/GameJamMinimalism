using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text coinText; // Das Textfeld für die Anzeige der Münzen

    void Start()
    {
        // Initiale UI-Aktualisierung
        UpdateCoinUI();

        // Den CoinManager abonnieren, um Änderungen zu überwachen (falls nicht Singleton)
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinCollected += UpdateCoinUI;
        }
    }

    void OnDestroy()
    {
        // Abonnement vom CoinManager entfernen
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinCollected -= UpdateCoinUI;
        }
    }

    // Diese Methode wird bei jeder Münzenaktualisierung aufgerufen
    public void UpdateCoinUI()
    {
        int coinCount = CoinManager.Instance.collectedCoins;
        coinText.text = "Coins: " + coinCount + " / " + CoinManager.Instance.totalCoins;
    }
}
