using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public float AttackRadius;
    public List<EnemyController> Enemies;
    float oldRadius;
    public GameObject RadiusSphere;
    [SerializeField] GameObject meshPlacement;
    public E_AttackType currentConstructedTower;
    public GameObject SellButton;
    [SerializeField] List<GameObject> UIPanel;
    bool IsConstructed;
    bool IsAttacking;
    TowerInfo currentTowerInfo;
    public GameObject TowerProjectilePrefab;


    GameObject TowerMesh;
    private void Awake()
    {
        RadiusSphere = GetComponentInChildren<ColliderChecker>().gameObject;
        GetComponentInChildren<ColliderChecker>().transform.localScale = new Vector3(AttackRadius, AttackRadius, AttackRadius);
        oldRadius = AttackRadius;
    }
    public void TurnRadiusSphere(bool Status)
    {
        if (RadiusSphere != null)
        {
            RadiusSphere.GetComponent<MeshRenderer>().enabled = Status;
        }
    }
    public void SpawnProjectile(EnemyController enemyToFollow)
    {
        float Damage = currentTowerInfo.Damage;
        GameObject bullet = Instantiate(TowerProjectilePrefab);
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = Vector3.zero;
        TowerProjectile curProjectile = bullet.GetComponent<TowerProjectile>();
        curProjectile.EnemyToFollow = enemyToFollow.gameObject;
        curProjectile.IsLaunched = true;
        curProjectile.damageType = currentConstructedTower;
        curProjectile.Damage = Damage;
        curProjectile.SetSettings();
        if (TowerMesh != null)
        {
            curProjectile.currentMesh.material.color = TowerMesh.GetComponent<MeshController>().GetMainColor();
        }
        else
        {
            curProjectile.currentMesh.material.color = Color.white;
        }



    }
    IEnumerator AttackEnemy()
    {

        yield return new WaitForSeconds(currentTowerInfo.AttackSpeed);
        if (IsConstructed)
        {
            if (Enemies != null)
            {
                if (Enemies.Count > 0)
                {
                    switch (currentConstructedTower)
                    {

                        case E_AttackType.Lightning:
                            foreach (var enemy in Enemies)
                            {
                                if (enemy != null)
                                {
                                    SpawnProjectile(enemy);
                                }

                            }
                            break;
                        default:
                            bool isFound = false;
                            foreach (var enemy in Enemies)
                            {
                                if (isFound == false)
                                {
                                    if (enemy != null)
                                    {
                                        SpawnProjectile(enemy);
                                        isFound = true;
                                    }
                                }
                            }
                            break;
                    }
                }
            }

        }
        IsAttacking = false;


    }
    // Update is called once per frame
    void Update()
    {
        //Debugging
        if (AttackRadius != oldRadius)
        {
            GetComponentInChildren<ColliderChecker>().transform.localScale = new Vector3(AttackRadius, AttackRadius, AttackRadius);
            oldRadius = AttackRadius;
        }
        if (IsConstructed)
        {
            if (IsAttacking == false)
            {
                IsAttacking = true;
                if (currentTowerInfo != null)
                {
                    StartCoroutine(AttackEnemy());
                }

            }

        }

    }
    public void AcceptTriggerInfo(Collider other, bool Status)
    {
        EnemyController newEnemy = null;
        if (other.TryGetComponent(out newEnemy))
        {
            if (Status)
            {
                Enemies.Add(newEnemy);
                Debug.Log($"Added: {newEnemy}");
            }
            else
            {
                Enemies.Remove(newEnemy);
                Debug.Log($"Removed: {newEnemy}");
            }
        }


    }
    public void TowerClicked()
    {
        if (TowerMesh != null)
        {
            TurnRadiusSphere(true);
            transform.GetComponentInParent<ArenaController>().TurnOfTowersSpheres(this);
            SellButton.SetActive(true);
            SellButton.GetComponent<SlotInfo>().CheckSellInfo();
            TowerMesh.GetComponent<MeshController>().ChangeColor(true, Color.green);
        }

    }
    public void AddTower(E_AttackType towerType)
    {
        GameObject newTowerMesh = null;
        currentConstructedTower = towerType;
        IsConstructed = true;
        currentTowerInfo = GameInstanceManager.instance.levelManager.GetTowerInfo(currentConstructedTower);
        if (currentTowerInfo != null)
        {
            newTowerMesh = Instantiate(currentTowerInfo.TowerPrefab);
            newTowerMesh.transform.SetParent(meshPlacement.transform);
            newTowerMesh.transform.localPosition = Vector3.zero;
            for (int i = 0; i < UIPanel.Count; i++)
            {
                UIPanel[i].SetActive(false);
            }
            GameInstanceManager.instance.UserInterfaceManager.RefreshUI();
            AttackRadius = currentTowerInfo.AttackRadius;
            MeshController newTowerMeshController = newTowerMesh.GetComponent<MeshController>();
            newTowerMeshController.InitColorDictionary();
            MouseBehaviour newTowerMouseBehaviour = newTowerMesh.GetComponent<MouseBehaviour>();
            newTowerMouseBehaviour.OnMouseDownEvent += TowerClicked;
            newTowerMouseBehaviour.OnMouseOverEvent+= newTowerMeshController.MouseOnMesh;
            TowerMesh = newTowerMesh;
        }



    }
    public void SellTower()
    {
        IsConstructed = false;
        GameInstanceManager.instance.UserInterfaceManager.RefreshUI();
        if (TowerMesh != null)
        {
            TowerMesh.GetComponent<MeshController>().ClearColors();
        }
        TowerMesh = null; 
        Destroy(meshPlacement.transform.GetChild(0).gameObject);

    }
    public void DeactivateUIPanel()
    {

        UIPanel[0].SetActive(false);

    }
    public void CheckOtherTowers()
    {
        GetComponentInParent<ArenaController>().TurnOffOtherTowers(this);
    }
}
