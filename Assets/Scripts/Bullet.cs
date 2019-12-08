using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Bullet : MonoBehaviour
{

    public float movementSpeed;
    public float damage;
    public float fireRate;
    public int type;

    private TimeScale timeScale;

    // Use this for initialization
    void Start()
    {
        this.LoadTimeScale();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    void LoadTimeScale()
    {
        this.timeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();
    }
    
    void Move()
    {
        float timeScale = 1;
        var layerName = LayerMask.LayerToName(this.gameObject.layer);        

        switch (layerName)
        {
            case "Player":
                timeScale = this.timeScale.playerScale;
                break;

            case "Enemy":
                timeScale = this.timeScale.globalScale;
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
        if (this.gameObject.layer != collision.gameObject.layer)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
