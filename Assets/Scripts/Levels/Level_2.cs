using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class Level_2 : MonoBehaviour {

    public GameObject valkyriePrefab;
    public GameObject beybladePrefab;
    public GameObject sharpClamPrefab;

    public Transform valkyrieSpawnPosition;
    public Transform valkyrieArrivalPosition;
    public Transform valkyrieLeftPosition;
    public Transform valkyrieRightPosition;
    public Transform valkyrieTacklePosition;

    public int valkyrieSpawn;
    public bool valkyrieSpawned;

    public Transform beybladeLeftSpawnPosition;
    public Transform beybladeRightSpawnPosition;
    public Vector3 beybladeSpawnDirection = Vector3.right;

    public float beybladeSpawnRate;
    public float beybladeSpawnCount;

    public Transform sharpClamSpawnPosition;
    public Transform sharpClamShootPosition;

    public float sharpClamSpawnRate;
    public float sharpClamSpawnCount;

    private Main main;
    //private Level_Main main;

    // Use this for initialization
    void Start ()
    {
        this.main = this.GetComponent<Main>();
        Invoke("DisplayConditionText", 0.001f);
    }

    private void DisplayConditionText()
    {
        this.main.ui.DisplayConditionText(this.valkyrieSpawn);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!main.ui.pauseMenu.enabled)
        {
            if(!this.valkyrieSpawned)
            {
                this.CheckValkyrieSpawn();
                this.CheckBeybladeSpawnRate();
                this.CheckSharpClamSpawnRate();
            }            
        }
    }

    private void CheckValkyrieSpawn()
    {
        if(this.main.killCount >= this.valkyrieSpawn)
        {
            this.SpawnValkyrie();
        }
    }

    private void SpawnValkyrie()
    {
        this.main.ui.DisplayBossHealth();

        var valkyrieObject = GameObject.Instantiate(this.valkyriePrefab);

        var spawnPosition = this.valkyrieSpawnPosition.position;
        spawnPosition.z = 0;

        valkyrieObject.transform.position = spawnPosition;        
        
        var valkyrie = valkyrieObject.GetComponent<Enemy_Valkyrie>();       

        valkyrie.arrivalPosition = this.valkyrieArrivalPosition;
        valkyrie.rightPosition = this.valkyrieRightPosition;
        valkyrie.leftPosition = this.valkyrieLeftPosition;
        valkyrie.tacklePosition = this.valkyrieTacklePosition;

        this.valkyrieSpawned = true;
    }

    private void CheckBeybladeSpawnRate()
    {
        if (beybladeSpawnCount >= beybladeSpawnRate)
        {
            this.SpawnBeyblade();
        }
        else
        {
            beybladeSpawnCount += Time.deltaTime;
        }
    }

    private void SpawnBeyblade()
    {
        float spawnX = 0f;        

        if(this.beybladeSpawnDirection == Vector3.right)
        {
            spawnX = this.beybladeLeftSpawnPosition.position.x;
        }
        else
        {
            spawnX = this.beybladeRightSpawnPosition.position.x;
        }

        var beybladeObject = GameObject.Instantiate(this.beybladePrefab);

        beybladeObject.transform.position = new Vector3(spawnX, Random.Range(this.main.verticalSize, -this.main.verticalSize), 0);

        var beyblade = beybladeObject.GetComponent<Enemy_Beyblade>();

        beyblade.movementDirection = this.beybladeSpawnDirection;

        if (this.beybladeSpawnDirection == Vector3.right)
        {
            this.beybladeSpawnDirection = Vector3.left;
        }
        else
        {
            this.beybladeSpawnDirection = Vector3.right;
        }

        this.beybladeSpawnCount = 0;
    }

    private void CheckSharpClamSpawnRate()
    {
        if (sharpClamSpawnCount >= sharpClamSpawnRate)
        {
            this.SpawnSharpClam();
        }
        else
        {
            sharpClamSpawnCount += Time.deltaTime;
        }
    }

    private void SpawnSharpClam()
    {
        var sharpClamObject = GameObject.Instantiate(this.sharpClamPrefab);

        sharpClamObject.transform.position = new Vector3(Random.Range(this.main.horizontalSize, -this.main.horizontalSize), this.sharpClamSpawnPosition.position.y, 0);

        var sharpClam = sharpClamObject.GetComponent<Enemy_SharpClam>();

        sharpClam.shootDistance = this.sharpClamShootPosition;

        this.sharpClamSpawnCount = 0;
    }
}
