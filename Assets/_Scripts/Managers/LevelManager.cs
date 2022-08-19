using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<WaveInfo> AllWaveSettings;
    public List<TowerInfo> AllTowerSettings;
    public List<EnemyInfo> AllEnemySettings;
    public GameObject EnemyControllerPrefab;
    public Transform StartPoint;
    public Transform EndPoint;
    public int CurrentWaveIndex;
    public GameObject SpawnedEnemies;
    bool GameStarted;


    IEnumerator StartWave(WaveInfo currentWave)
    {
        yield return new WaitForSeconds(currentWave.WaveDuration);
        currentWave.IsWaveDone = true;
        if (CurrentWaveIndex + 1 < AllWaveSettings.Count)
        {
            CurrentWaveIndex += 1;
            GameInstanceManager.instance.UserInterfaceManager.RefreshUI();
        }

    }
    IEnumerator SpawnWithTime(EnemyQuantity enemyQuantityInfo)
    {

        if (enemyQuantityInfo.Spawned < enemyQuantityInfo.Quantity)
        {
            enemyQuantityInfo.Spawned += 1;
            GameObject newEnemy = Instantiate(EnemyControllerPrefab, StartPoint.position, StartPoint.rotation);
            newEnemy.transform.SetParent(SpawnedEnemies.transform);
            EnemyController controller = newEnemy.GetComponent<EnemyController>();
            controller.basicInfo.Type = enemyQuantityInfo.Type;
            controller.SetSettings();
        }


        yield return new WaitForSeconds(enemyQuantityInfo.SpawnTime);
        enemyQuantityInfo.IsSpawning = false;
    }
    public void SpawnEnemy(EnemyQuantity enemyQuantityInfo)
    {
        if (enemyQuantityInfo.IsSpawning==false)
        {
            enemyQuantityInfo.IsSpawning = true;
            StartCoroutine(SpawnWithTime(enemyQuantityInfo));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            if (AllWaveSettings != null)
            {
                if (CurrentWaveIndex < AllWaveSettings.Count)
                {
                    WaveInfo currentWave = AllWaveSettings[CurrentWaveIndex];
                    if (currentWave.IsWaveLaunched == false)
                    {
                        currentWave.IsWaveLaunched = true;
                        StartCoroutine(StartWave(currentWave));
                    }
                    else
                    {
                        foreach (var Enemies in currentWave.Enemies)
                        {
                            SpawnEnemy(Enemies);
                        }

                    }
                }
            }
        }
        

    }
    public TowerInfo GetTowerInfo(E_AttackType towerType)
    {
        if (AllTowerSettings != null)
        {
            foreach (var item in AllTowerSettings)
            {
                if (item.TowerType == towerType)
                {
                    return item;
                }
            }
        }
        return null;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        GameStarted = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
