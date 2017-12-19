using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Batmovile : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform placeHolder;
    public Transform shootDistance;

    public float movementSpeed;
    public float rotationSpeed;

    public float fireRate;
    public float fireCount;
    
	// Use this for initialization
	void Start () {
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

    void LoadPlaceHolder()
    {
        this.placeHolder = this.transform.FindChild("PH");
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
        this.transform.position += Vector3.down * this.movementSpeed * Time.deltaTime * this.gameObject.GetComponent<Enemy>().timeScale;
    }

    private void RotateToPlayer()
    {
        var player = GameObject.Find("PlayerShip");

        if(player != null)
        {
            var direction = (player.transform.position - this.transform.position).normalized;

            this.transform.up = Vector3.Lerp(this.transform.up, direction, rotationSpeed * this.gameObject.GetComponent<Enemy>().timeScale);
        }
    }

    bool CheckFireRate()
    {
        if (fireCount >= fireRate)
        {
            return true;
        }

        fireCount += Time.deltaTime * this.gameObject.GetComponent<Enemy>().timeScale;

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
