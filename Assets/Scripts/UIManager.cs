using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text coinText; // Das Textfeld f�r die Anzeige der M�nzen

    void Start()
    {
        // Initiale UI-Aktualisierung
        UpdateCoinUI();

        // Den CoinManager abonnieren, um �nderungen zu �berwachen (falls nicht Singleton)
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

    // Diese Methode wird bei jeder M�nzenaktualisierung aufgerufen
    public void UpdateCoinUI()
    {
        int coinCount = CoinManager.Instance.collectedCoins;
        coinText.text = "Coins: " + coinCount + " / " + CoinManager.Instance.totalCoins;
    }
}
