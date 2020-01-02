using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Bullet : MonoBehaviour
{
    public TimeScale TimeScale;
    public float MovementSpeed;

    public float damage;
    public int type;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    void Move()
    {
        float timeScale = 1;

        if (this.TimeScale != null)
        {
            switch (LayerMask.LayerToName(this.gameObject.layer))
            {
                case GameObjectsLayers.Player:
                    timeScale = this.TimeScale.PlayerScale;
                    break;

                case GameObjectsLayers.Enemy:
                    timeScale = this.TimeScale.GlobalScale;
                    break;
            }
        }

        this.transform.position += this.transform.up * this.MovementSpeed * Time.deltaTime * timeScale;
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
