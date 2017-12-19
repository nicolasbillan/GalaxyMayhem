using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Bullet_Valkyrie : MonoBehaviour {

    public float movementSpeed;
    public float damage;
    public float chargeTime;
    public float chargeScale;
    public bool charged;
    public Vector3 moveDirection;
    
    // Use this for initialization
    void Start()
    {
        this.charged = false;
        this.chargeScale = Time.deltaTime * 2;
        Invoke("EndCharge", this.chargeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.charged)
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
        var timeScale = GameObject.Find("TimeScale").GetComponent<TimeScale>().globalScale;

        this.transform.position += this.moveDirection * movementSpeed * Time.deltaTime * timeScale;
    }

    void Charge()
    {
        this.transform.localScale += new Vector3(this.chargeScale, this.chargeScale, this.chargeScale);
    }

    void EndCharge()
    {
        this.charged = true;

        var player = GameObject.Find(ParametersKeys.PlayerShip);

        if(player != null)
        {
           this.moveDirection = (player.transform.position - this.transform.position).normalized;
        }
        else
        {
            this.moveDirection = Vector3.down;
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
