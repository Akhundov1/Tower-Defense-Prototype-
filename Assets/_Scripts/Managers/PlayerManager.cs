using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int CurrentCoins;
    public float CurrentHealth;

    public void ReduceHealth(float reducedAmount)
    {
        CurrentHealth -= reducedAmount;
        if (CurrentHealth <= 0)
        {
            GameInstanceManager.instance.UserInterfaceManager.TurnOnEndGameUI(true);
            Time.timeScale = 0.0f;
        }
    }

}
