using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float movementSpeed;

    public float damage;

    //public BulletTypeEnum type;

    public float fireRate;
    
    public int type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.Move();
	}

    void Move()
    {
        float timeScale = 1;
        var layerName = LayerMask.LayerToName(this.gameObject.layer);

        switch(layerName)
        {
            case "Player":
                timeScale = GameObject.Find("TimeScale").GetComponent<TimeScale>().playerScale;
                break;

            case "Enemy":
                timeScale = GameObject.Find("TimeScale").GetComponent<TimeScale>().globalScale;                
                break;
        }

        this.transform.position += this.transform.up * movementSpeed * Time.deltaTime * timeScale;
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.layer != collision.gameObject.layer)
        {
            GameObject.Destroy(this.gameObject);
        }       
    }

    //public enum BulletTypeEnum
    //{

    //}
}
