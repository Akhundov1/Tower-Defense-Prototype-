using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotInfo : MonoBehaviour
{

    [SerializeField] E_AttackType TowerClass;

    [SerializeField] Image LockImage;
    [SerializeField] Image TowerIcon;
    [SerializeField] TextMeshProUGUI TowerPriceText;
    [SerializeField] TowerController mainController;
    int currentPrice = 50;
    [SerializeField] bool ForSelling;
    // Start is called before the first frame update
    void Start()
    {

        if (ForSelling == false)
        {
            GameInstanceManager.instance.RefreshUIEvent += CheckStatus;
            List<TowerInfo> AllTowers = GameInstanceManager.instance.levelManager.AllTowerSettings;
            if (AllTowers != null)
            {
                foreach (var item in AllTowers)
                {
                    if (item.TowerType == TowerClass)
                    {
                        currentPrice = item.Price;
                    }
                }
            }
            TowerPriceText.text = currentPrice.ToString();
            CheckStatus();
        }
    }
    public void CheckSellInfo()
    {

        List<TowerInfo> AllTowers = GameInstanceManager.instance.levelManager.AllTowerSettings;
        TowerClass = mainController.currentConstructedTower;
        if (AllTowers != null)
        {
            foreach (var item in AllTowers)
            {
                if (item.TowerType == TowerClass)
                {
                    currentPrice = (item.Price / 2);
                }
            }
        }
        TowerPriceText.text = currentPrice.ToString();


    }
    public void CheckStatus()
    {
        int playerCoins = GameInstanceManager.instance.playerManager.CurrentCoins;
        if (playerCoins >= currentPrice)
        {
            LockImage.gameObject.SetActive(false);
            TowerIcon.color = new Color(TowerIcon.color.r, TowerIcon.color.g, TowerIcon.color.b, 1.0f);
            TowerPriceText.color = new Color(TowerPriceText.color.r, TowerPriceText.color.g, TowerPriceText.color.b, 1.0f);
        }
        else
        {
            TowerIcon.color = new Color(TowerIcon.color.r, TowerIcon.color.g, TowerIcon.color.b, 0.5f);

            TowerPriceText.color = new Color(TowerPriceText.color.r, TowerPriceText.color.g, TowerPriceText.color.b, 0.5f);

            LockImage.gameObject.SetActive(true);
        }
    }
    public void SlotClicked()
    {
        int playerCoins = GameInstanceManager.instance.playerManager.CurrentCoins;
        if (playerCoins >= currentPrice)
        {
            GameInstanceManager.instance.playerManager.CurrentCoins -= currentPrice;
            LockImage.gameObject.SetActive(false);
            mainController.AddTower(TowerClass);
        }
    }
    public void SellSlot()
    {
        GameInstanceManager.instance.playerManager.CurrentCoins += currentPrice;
        mainController.TurnRadiusSphere(false);
        gameObject.SetActive(false);
        mainController.SellTower();
    }
}
