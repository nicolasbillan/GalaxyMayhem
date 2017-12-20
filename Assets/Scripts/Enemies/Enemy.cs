using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Enemy : MonoBehaviour
{
    public UI ui;

    public float healthPoints;
    public float maxHealth;
    public float timeScale;    
    public bool boss;

    // Use this for initialization
    void Start()
    {
        healthPoints = maxHealth;
        timeScale = 1;
        
        if(this.boss)
        {
            this.LoadUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.GetTimeScale();
    }

    void LoadUI()
    {
        this.ui = GameObject.Find(GameObjectNames.Ui).GetComponent<UI>();
    }

    void GetTimeScale()
    {
        this.timeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>().globalScale;
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
                GameObject.Find(GameObjectNames.MainCamera).GetComponent<Main>().killCount++;
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = Color.red;
                Invoke("GetBackToVerdaderoColor", 0.1f);
            }
        }
    }
    
    public void GetBackToVerdaderoColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
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
    
    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}