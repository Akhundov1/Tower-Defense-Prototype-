using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public EnemyInfo basicInfo;
    bool IsDead;
    private NavMeshAgent navigationAI;
    private Animator enemyAnimator;
    public Transform point;
    public bool IsLaunched;
    public Slider currentSlider;
    public SkinnedMeshRenderer currentMesh;
    public Color OriginalColor;
    // Start is called before the first frame update
    void Start()
    {

        navigationAI = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponentInChildren<Animator>();


    }
    public void SetSettings()
    {
        List<EnemyInfo> AllEnemies = GameInstanceManager.instance.levelManager.AllEnemySettings;
        if (AllEnemies != null)
        {
            foreach (var enemy in AllEnemies)
            {
                if (enemy.Type == basicInfo.Type)
                {
                    basicInfo.EnemyPrefab = enemy.EnemyPrefab;
                    basicInfo.Health = enemy.MaxHealth;
                    basicInfo.MaxHealth = enemy.MaxHealth;
                    basicInfo.Damage = enemy.Damage;
                    basicInfo.MoveSpeed = enemy.MoveSpeed;
                    basicInfo.Reward = enemy.Reward;
                    GameObject enemyMesh = Instantiate(basicInfo.EnemyPrefab);
                    enemyMesh.transform.SetParent(this.transform);
                    enemyMesh.transform.localPosition = new Vector3(0.0f, enemyMesh.transform.localPosition.y, 0.0f);
                    Animator enemyAnimator = GetComponent<Animator>();
                    IsLaunched = true;
                    point = GameInstanceManager.instance.levelManager.EndPoint;
                    currentMesh = enemyMesh.GetComponentInChildren<SkinnedMeshRenderer>();
                    OriginalColor = currentMesh.material.color;
                    navigationAI = GetComponent<NavMeshAgent>();
                    if (navigationAI != null)
                    {
                        navigationAI.speed = basicInfo.MoveSpeed;
                    }
                    enemyAnimator = GetComponentInChildren<Animator>();
                    if (enemyAnimator!=null){
                        enemyAnimator.SetFloat("MoveSpeed", basicInfo.MoveSpeed);
                    }

                }
            }
        }
    }
    IEnumerator acceptModifier()
    {
        currentMesh.material.color = Color.blue;
        navigationAI.speed = navigationAI.speed / 2f;
        yield return new WaitForSeconds(2f);
        navigationAI.speed = basicInfo.MoveSpeed;
        currentMesh.material.color = OriginalColor;
    }
    IEnumerator changeColor()
    {
        currentMesh.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        currentMesh.material.color = OriginalColor;

    }
    // Update is called once per frame
    void Update()
    {
        if (IsLaunched==true&&IsDead==false)
        {
            navigationAI.destination = point.position;
        }

    }
    public void AcceptDamage(float Damage,E_AttackType attackType)
    {
        if (IsDead==false)
        {
            basicInfo.Health -= Damage;
            float newValue = ((basicInfo.Health / basicInfo.MaxHealth) * 100f);
            currentSlider.value = (newValue/100f);

            if (attackType == E_AttackType.Ice)
            {
                StartCoroutine(acceptModifier());
            }
            else
            {
                StartCoroutine(changeColor());
            }
            if (basicInfo.Health <= 0)
            {
                IsDead = true;
                GameInstanceManager.instance.playerManager.CurrentCoins += basicInfo.Reward;
                GameInstanceManager.instance.UserInterfaceManager.RefreshUI();
                if (GameInstanceManager.instance.levelManager.CurrentWaveIndex + 1 == GameInstanceManager.instance.levelManager.AllWaveSettings.Count)
                {
                    if (transform.parent.childCount == 1)
                    {
                        GameInstanceManager.instance.UserInterfaceManager.TurnOnEndGameUI(false);
                    }

                }
                Destroy(this.gameObject);
            }
        }
    }
}