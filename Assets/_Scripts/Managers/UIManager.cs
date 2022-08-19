using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI CoinsQuantity;
    public TextMeshProUGUI WaveInfo;
    public TextMeshProUGUI HealthRemaining;
    public GameObject GameEndPanel;
    public TextMeshProUGUI GameEndText;

    // Start is called before the first frame update
    void Start()
    {
        CoinsQuantity.text = GameInstanceManager.instance.playerManager.CurrentCoins.ToString();
        string CurrentWave = ($"{GameInstanceManager.instance.levelManager.CurrentWaveIndex+1}/{GameInstanceManager.instance.levelManager.AllWaveSettings.Count}");
        WaveInfo.text = CurrentWave;
        HealthRemaining.text = GameInstanceManager.instance.playerManager.CurrentHealth.ToString();
    }


    public void RefreshUI()
    {
        GameInstanceManager.instance.RefreshUIEvent();
        CoinsQuantity.text = GameInstanceManager.instance.playerManager.CurrentCoins.ToString();
        CoinsQuantity.text = GameInstanceManager.instance.playerManager.CurrentCoins.ToString();
        string CurrentWave = ($"{GameInstanceManager.instance.levelManager.CurrentWaveIndex+1}/{GameInstanceManager.instance.levelManager.AllWaveSettings.Count}");
        WaveInfo.text = CurrentWave;
        HealthRemaining.text = GameInstanceManager.instance.playerManager.CurrentHealth.ToString();
    }
    public void TurnOnEndGameUI(bool IsLose)
    {
        GameEndPanel.SetActive(true);
        if (IsLose)
        {
            GameEndText.color = Color.red;
            GameEndText.text = "YOU DIED!";
        }
        else
        {
            GameEndText.color = Color.green;
            GameEndText.text = "YOU SURVIVED!";
        }
    }
}
