using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class Level_1 : MonoBehaviour
{   
    public GameObject deathDonutPrefab;
    public GameObject[] enemyPrefabs;

    public Transform deathDonutStopPosition;
    public Transform deathDonutSpawnPosition;
    public int deathDonutSpawn;
    public bool deathDonutSpawned;

    public Transform raindropHoldPosition;
    public Transform batmovileShootPosisiton;
    public float spawnRate;
    public float spawnCount;

    private Main main;

    // Use this for initialization
    void Start()
    {
        this.main = this.GetComponent<Main>();
        Invoke("DisplayConditionText", Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!main.ui.pauseMenu.enabled)
        {
            this.CheckSpawnBoss();
            this.CheckEnemySpawnRate();
        }
    }

    private void DisplayConditionText()
    {
        this.main.ui.DisplayConditionText(this.deathDonutSpawn);
    }

    private void CheckSpawnBoss()
    {
        if (main.killCount >= deathDonutSpawn && !deathDonutSpawned)
        {
            this.SpawnBoss();
        }
    }

    private void CheckEnemySpawnRate()
    {
        if (spawnCount >= spawnRate)
        {
            this.SpawnEnemy((EnemyPrefabsEnum)Random.Range(0, 2));
            this.spawnCount = 0;
        }
        else
        {
            spawnCount += Time.deltaTime;
        }
    }

    private void SpawnEnemy(EnemyPrefabsEnum prefab)
    {
        switch (prefab)
        {
            case EnemyPrefabsEnum.Raindrop:
                this.SpawnRaindrop();
                break;

            case EnemyPrefabsEnum.Batmovile:
                this.SpawnBatmovile();
                break;
        }
    }

    private void SpawnRaindrop()
    {
        var enemyShip = GameObject.Instantiate(this.enemyPrefabs[(int)EnemyPrefabsEnum.Raindrop]);
        enemyShip.transform.position = new Vector3(Random.Range(-this.main.horizontalSize, this.main.horizontalSize), Random.Range(this.main.verticalSize, this.main.verticalSize + 5), 0);
        enemyShip.GetComponent<Enemy_Raindrop>().holdDistance = this.raindropHoldPosition;
    }

    private void SpawnBatmovile()
    {
        var enemyShip = GameObject.Instantiate(this.enemyPrefabs[(int)EnemyPrefabsEnum.Batmovile]);
        enemyShip.transform.position = new Vector3(Random.Range(-this.main.horizontalSize, this.main.horizontalSize), Random.Range(this.main.verticalSize, this.main.verticalSize + 5), 0);
        enemyShip.GetComponent<Enemy_Batmovile>().shootDistance = this.batmovileShootPosisiton;
    }

    private void SpawnBoss()
    {
        this.deathDonutSpawned = true;

        this.main.ui.DisplayBossHealth();

        var boss = GameObject.Instantiate(this.deathDonutPrefab);
        boss.transform.position = new Vector3(this.deathDonutSpawnPosition.position.x, this.deathDonutSpawnPosition.transform.position.y, 0);

        boss.GetComponent<Enemy_DeathDonut>().stopDistance = this.deathDonutStopPosition;
    }
}
