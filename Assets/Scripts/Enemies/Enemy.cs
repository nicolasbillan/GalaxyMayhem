using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthPoints;
    public float maxHealth;

    public float timeScale;

    public UI ui;

    public bool boss;

    // Use this for initialization
    void Start()
    {
        healthPoints = maxHealth;
        timeScale = 1;
        
        if(this.boss)
        {
            this.StartUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.GetTimeScale();
    }

    void StartUI()
    {
        this.ui = GameObject.Find("UI").GetComponent<UI>();
    }

    void GetTimeScale()
    {
        this.timeScale = GameObject.Find("TimeScale").GetComponent<TimeScale>().globalScale;
    }

    void UpdateBossHealth()
    {
        this.ui.UpdateBossHealth(this.healthPoints, this.maxHealth);
    }

    public void BulletHit(GameObject bullet)
    {
        //bala enemiga
        if (bullet.layer != this.gameObject.layer)
        {
            this.healthPoints -= bullet.GetComponent<Bullet>().damage;

            if(this.boss)
            {
                this.UpdateBossHealth();
            }
            
            if (healthPoints <= 0)
            {
                GameObject.Find("Main Camera").GetComponent<Main>().killCount++;
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = Color.red;
                Invoke("GetBackToVerdaderoColor", 0.1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                this.BulletHit(collision.gameObject);
                break;
        }
    }

    public void GetBackToVerdaderoColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}