using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SharpClam : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform[] shootPlaceholders;
    public Transform shootDistance;

    public float movementSpeed;
    public float rotationSpeed;

    public float fireRate;
    public float fireCount;

    private Enemy stats;

    // Use this for initialization
    void Start()
    {
        this.LoadStats();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();

        if (this.CheckShootDistance())
        {
            this.RotateToPlayer();

            if (CheckFireRate())
            {
                this.Shoot();
            }
        }
    }

    private void LoadStats()
    {
        this.stats = this.gameObject.GetComponent<Enemy>();
    }

    private bool CheckShootDistance()
    {
        var realDistance = this.transform.position.y - shootDistance.position.y;

        if (realDistance <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move()
    {
        this.transform.position += Vector3.down * this.movementSpeed * Time.deltaTime * this.stats.timeScale;
    }

    private void RotateToPlayer()
    {
        var player = GameObject.Find("PlayerShip");

        if (player != null)
        {
            var direction = (player.transform.position - this.transform.position).normalized;

            this.transform.up = Vector3.Lerp(this.transform.up, -direction, rotationSpeed * this.stats.timeScale);
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
        for (int i = 0; i < this.shootPlaceholders.Length; i++)
        {
            var bullet = GameObject.Instantiate(this.bulletPrefab);
            
            bullet.transform.position = this.shootPlaceholders[i].position;
            bullet.transform.up = this.shootPlaceholders[i].up;
            bullet.gameObject.layer = this.gameObject.layer;
        }      

        fireCount = 0;
    }
}
