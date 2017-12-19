using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class Level_1 : MonoBehaviour {

    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    
    public Transform raindropHoldDistance;
    public Transform batmovileShootDistance;
    public Transform bossStopDistance;
    public Transform bossOrigin;

    public float spawnRate;
    public float spawnCount;
    public int spawnBossCount;
    public bool bossSpawned;

    private Main main;

    // Use this for initialization
    void Start()
    {
        this.main = this.GetComponent<Main>();
        Invoke("DisplayConditionText", 0.001f);
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
        this.main.ui.DisplayConditionText(this.spawnBossCount);
    }

    private void CheckSpawnBoss()
    {
        if (main.killCount >= spawnBossCount && !bossSpawned)
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
        enemyShip.GetComponent<Enemy_Raindrop>().holdDistance = this.raindropHoldDistance;
    }

    private void SpawnBatmovile()
    {
        var enemyShip = GameObject.Instantiate(this.enemyPrefabs[(int)EnemyPrefabsEnum.Batmovile]);
        enemyShip.transform.position = new Vector3(Random.Range(-this.main.horizontalSize, this.main.horizontalSize), Random.Range(this.main.verticalSize, this.main.verticalSize + 5), 0);
        enemyShip.GetComponent<Enemy_Batmovile>().shootDistance = this.batmovileShootDistance;
    }

    private void SpawnBoss()
    {
        this.bossSpawned = true;

        this.main.ui.DisplayBossHealth();

        var boss = GameObject.Instantiate(this.bossPrefab);
        boss.transform.position = new Vector3(this.bossOrigin.position.x, this.bossOrigin.transform.position.y, 0);

        boss.GetComponent<Enemy_DeathDonut>().stopDistance = this.bossStopDistance;
    }
}
