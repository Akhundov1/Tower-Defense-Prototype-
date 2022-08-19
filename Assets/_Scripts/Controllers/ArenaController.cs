using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    List<TowerController> AllTowers= new List<TowerController>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AllTowers.Add(transform.GetChild(i).GetComponentInChildren<TowerController>());
        }
    }
    public void TurnOffOtherTowers(TowerController activeTower)
    {
        for (int i = 0; i < AllTowers.Count; i++)
        {
            if (AllTowers[i] != activeTower)
            {
                AllTowers[i].DeactivateUIPanel();
                AllTowers[i].SellButton.SetActive(false);
            }
        }
    }
    public void TurnOfTowersSpheres(TowerController activeTower)
    {
        for (int i = 0; i < AllTowers.Count; i++)
        {
            if (AllTowers[i] != activeTower)
            {
                AllTowers[i].TurnRadiusSphere(false);
                AllTowers[i].SellButton.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
