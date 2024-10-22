using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text coinText;
    public CoinManager coinManager;

    private void Start()
    {
        //find coinmanger if none is referenced (falls ichs vergesse)
        if (coinManager == null)
        {
            coinManager = FindFirstObjectByType<CoinManager>();
        }

        if (coinManager != null)
        {
            UpdateCoinUI();
            
            coinManager.OnCoinCollected += UpdateCoinUI;
        }
        else
        {
            Debug.LogError("CoinManager not found in the scene.");
        }
    }

    private void OnDestroy()
    {
        if (coinManager != null)
        {
            coinManager.OnCoinCollected -= UpdateCoinUI;
        }
    }
    
    public void UpdateCoinUI()
    {
        var coinCount = coinManager.collectedCoins;
        coinText.text = "Coins: " + coinCount + " / " + coinManager.totalCoins;
    }
}