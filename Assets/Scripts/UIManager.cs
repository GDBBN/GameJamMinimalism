using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text coinText;
    public CoinManager coinManager;

    void Start()
    {
        //find coinmanger if none is referenced (falls ichs vergesse)
        if (coinManager == null)
        {
            coinManager = FindObjectOfType<CoinManager>();
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

    void OnDestroy()
    {
        if (coinManager != null)
        {
            coinManager.OnCoinCollected -= UpdateCoinUI;
        }
    }
    
    public void UpdateCoinUI()
    {
        int coinCount = coinManager.collectedCoins;
        coinText.text = "Coins: " + coinCount + " / " + coinManager.totalCoins;
    }
}