using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            GameInstanceManager.instance.playerManager.ReduceHealth(enemy.basicInfo.Damage);
            GameInstanceManager.instance.UserInterfaceManager.RefreshUI();
            if (GameInstanceManager.instance.levelManager.CurrentWaveIndex + 1 == GameInstanceManager.instance.levelManager.AllWaveSettings.Count)
            {
                if (enemy.transform.parent.childCount == 1)
                {
                    GameInstanceManager.instance.UserInterfaceManager.TurnOnEndGameUI(false);
                }

            }
            Destroy(other.gameObject);

        }
    }
}

