using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class PowerUpContainer : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public int activePrefab;
    public float movementSpeed;

    private SpriteRenderer bulletSprite;
    private TimeScale timeScale;

    // Use this for initialization
    void Start()
    {
        this.LoadActivePrefab();
        this.LoadTimeScale();
        this.LoadMovementSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    void LoadTimeScale()
    {
        this.timeScale = GameObject.Find("TimeScale").GetComponent<TimeScale>();
    }

    void LoadMovementSpeed()
    {
        this.movementSpeed = GameObject.Find(GameObjectNames.Level).GetComponent<Level>().scrollSpeed;
    }

    void LoadActivePrefab()
    {
        this.activePrefab = Random.Range(0, bulletPrefabs.Length);
        this.bulletSprite = this.transform.Find("bulletSprite").GetComponent<SpriteRenderer>();

        bulletSprite.sprite = this.bulletPrefabs[activePrefab].GetComponent<SpriteRenderer>().sprite;

        this.bulletPrefabs[activePrefab].GetComponent<Bullet>().Type = activePrefab;
    }

    private void Move()
    {
        this.transform.position += Vector3.down * this.movementSpeed * Time.deltaTime * this.timeScale.GlobalScale;
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
