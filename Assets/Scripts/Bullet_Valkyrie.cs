using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Bullet_Valkyrie : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 movementDirection;

    public float chargeTime;
    public float chargeTimeCount;
    public float chargeScale;
    public bool charged;

    private TimeScale timeScale;

    // Use this for initialization
    void Start()
    {
        this.charged = false;
        this.chargeScale = Time.deltaTime * 2;
        this.timeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.charged)
        {
            this.Move();
        }
        else
        {
            this.Charge();
        }
    }

    void Move()
    {
        this.transform.position += this.movementDirection * movementSpeed * Time.deltaTime * timeScale.GlobalScale;
    }

    void Charge()
    {
        if (this.chargeTimeCount >= this.chargeTime)
        {
            this.EndCharge();
        }
        else
        {
            this.transform.localScale += new Vector3(this.chargeScale * this.timeScale.GlobalScale, this.chargeScale * this.timeScale.GlobalScale, this.chargeScale * this.timeScale.GlobalScale);

            this.chargeTimeCount += Time.deltaTime * this.timeScale.GlobalScale;
        }
    }

    void EndCharge()
    {
        this.charged = true;

        var player = GameObject.Find(GameObjectNames.PlayerShip);

        if (player != null)
        {
            this.movementDirection = (player.transform.position - this.transform.position).normalized;
        }
        else
        {
            this.movementDirection = Vector3.down;
        }
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
