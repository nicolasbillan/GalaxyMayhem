using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Enemy_Batmovile : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform placeHolder;
    public Transform shootDistance;

    public float movementSpeed;
    public float rotationSpeed;
    public float fireRate;
    public float fireCount;

    private Enemy stats;
    private GameObject player;
    
	// Use this for initialization
	void Start () {
        this.LoadStats();
        this.LoadPlaceHolder();
	}
	
	// Update is called once per frame
	void Update () {
        this.Move();

        if(this.CheckShootDistance())
        {
            this.RotateToPlayer();

            if (CheckFireRate())
            {
                this.Shoot();
            }
        }        
    }

    void LoadStats()
    {
        this.stats = this.gameObject.GetComponent<Enemy>();
    }

    void LoadPlayer()
    {
        this.player = GameObject.Find(GameObjectNames.PlayerShip);
    }

    void LoadPlaceHolder()
    {
        this.placeHolder = this.transform.FindChild("PH");
    }

    private bool CheckShootDistance()
    {
        return (this.transform.position.y - shootDistance.position.y <= 0);        
    }

    private void Move()
    {
        this.transform.position += Vector3.down * this.movementSpeed * Time.deltaTime * this.stats.timeScale;
    }

    private void RotateToPlayer()
    {
        if(this.player != null)
        {
            var direction = (this.player.transform.position - this.transform.position).normalized;

            this.transform.up = Vector3.Lerp(this.transform.up, direction, rotationSpeed * this.stats.timeScale);
        }
    }

    bool CheckFireRate()
    {
        if (fireCount >= fireRate)
        {
            return true;
        }

        fireCount += Time.deltaTime * this.stats.timeScale;

        return false;
    }

    void Shoot()
    {
        var bullet = GameObject.Instantiate(this.bulletPrefab);

        bullet.transform.position = this.placeHolder.position;
        bullet.transform.up = this.placeHolder.up;
        bullet.gameObject.layer = this.gameObject.layer;

        fireCount = 0;
    }
}
