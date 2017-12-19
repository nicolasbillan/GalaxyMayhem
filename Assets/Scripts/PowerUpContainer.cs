using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainer : MonoBehaviour {

    public GameObject[] bulletPrefabs;
    public int activePrefab;
    private SpriteRenderer bulletSprite;

	// Use this for initialization
	void Start () {
        this.LoadActivePrefab();        
	}
	
	// Update is called once per frame
	void Update () {
        this.MoveWithLevel();
	}

    void LoadActivePrefab()
    {
        this.activePrefab = Random.Range(0, bulletPrefabs.Length);
        this.bulletSprite = this.transform.Find("bulletSprite").GetComponent<SpriteRenderer>();

        bulletSprite.sprite = this.bulletPrefabs[activePrefab].GetComponent<SpriteRenderer>().sprite;

        this.bulletPrefabs[activePrefab].GetComponent<Bullet>().type = activePrefab;        
    }

    private void MoveWithLevel()
    {
        var scrollSpeed = GameObject.Find("Level").GetComponent<Level>().scrollSpeed;
        this.transform.position += Vector3.down * scrollSpeed * Time.deltaTime * GameObject.Find("TimeScale").GetComponent<TimeScale>().globalScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == this.gameObject.layer && collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
