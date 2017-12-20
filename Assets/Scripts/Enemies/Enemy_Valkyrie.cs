using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class Enemy_Valkyrie : MonoBehaviour {

    public float movementSpeed = 5f;
    public float fastHealthThreshold;
    public float fastSpeedMultiplier;    
    private ValkyrieBehaviourStateEnum behaivourState;

    public Transform arrivalPosition;
    public Transform leftPosition;
    public Transform rightPosition;
    public Transform tacklePosition;

    public Vector3 moveDirection;

    [Range(0, 60)]
    public float gatlinTime;
    public float gatlinTimeCount;

    [Range(0, 60)]
    public float cannonTime;
    public float cannonTimeCount;

    public GameObject gatlinBulletPrefab;
    public Transform[] gatlinPlaceHolders;    
    public float gatlinFireRate;
    public float gatlinFireCount;
    public int gatlinBulletTilt;
    
    public GameObject cannonBulletPrefab;
    public Transform cannonPlaceHolder;
    public float cannonFireRate;
    public float cannonFireCount;
    public float cannonChargeTime;
    public float cannonChargeTimeCount;
    public bool cannonCharge;

    public float tackleTimeRate;
    public float tackleTimeCount;
    public bool tackleStateOn;
    public float tackleHealthThreshold;
    public float tackleSpeedMultiplier;

    private Enemy stats;
    
    // Use this for initialization
    void Start () {
        this.name = GameObjectNames.BossShip;
        this.behaivourState = ValkyrieBehaviourStateEnum.Arrival;
        this.stats = this.GetComponent<Enemy>();		
	}
	
	// Update is called once per frame
	void Update () {        
		switch(this.behaivourState)
        {
            case ValkyrieBehaviourStateEnum.Arrival:
                this.MoveToArrival();
                break;

            case ValkyrieBehaviourStateEnum.Gatlin:
                this.GatlinState();
                break;

            case ValkyrieBehaviourStateEnum.Cannon:
                this.CannonState();
                break;

            case ValkyrieBehaviourStateEnum.Tackle:
                this.TackleState();
                break;
        }
    }

    public void MoveToArrival()
    {
        this.transform.position += Vector3.down * this.stats.timeScale * this.movementSpeed * Time.deltaTime;

        if(this.transform.position.y <= this.arrivalPosition.transform.position.y)
        {
            this.moveDirection = Vector3.right;
            this.SwitchState();
        }
    }

    public void GatlinState()
    {
        this.Move();

        if (this.CheckGatlinFireRate())
        {
            this.ShootGatlin();
        }

        if (this.CheckGatlinTime())
        {
            this.SwitchState();
        }
    }

    public void CannonState()
    {
        if (!this.cannonCharge)
        {
            this.Move();
        }
        else
        {
            this.CheckCannonChargeTime();
        }

        if (this.CheckCannonFireRate())
        {
            this.ShootCannon();
        }

        if (this.CheckCannonTime())
        {
            this.SwitchState();
        }
    }

    public void TackleState()
    {
        if (!this.tackleStateOn)
        {
            this.Move();
        }
        else
        {
            this.Tackle();
        }

        if (this.CheckTackleTimeRate())
        {
            this.BeginTackle();
        }
    }

    public void Move()
    {
        float speedMultiplier = 1;

        if(this.behaivourState == ValkyrieBehaviourStateEnum.Tackle)
        {
            speedMultiplier = this.tackleSpeedMultiplier;
        }
        else if(this.stats.healthPoints <= this.fastHealthThreshold)
        {
            speedMultiplier = this.fastSpeedMultiplier;
        }

        this.transform.position += this.moveDirection * this.stats.timeScale * this.movementSpeed * speedMultiplier * Time.deltaTime;

        if (this.transform.position.x >= this.rightPosition.transform.position.x
            || this.transform.position.x <= this.leftPosition.transform.position.x)
        {
            this.moveDirection *= -1;
        }
    }

    public void Tackle()
    {
        this.transform.position += this.moveDirection * this.stats.timeScale * this.movementSpeed * tackleSpeedMultiplier * Time.deltaTime;

        if (this.transform.position.y <= this.tacklePosition.transform.position.y)
        {
            this.moveDirection *= -1;            
        }

        if(this.transform.position.y >= this.arrivalPosition.transform.position.y)
        {
            this.moveDirection = Vector3.right;
            this.tackleStateOn = false;             
        }
    }

    public bool CheckGatlinFireRate()
    {
        if (this.gatlinFireCount >= this.gatlinFireRate)
        {
            return true;
        }

        this.gatlinFireCount += Time.deltaTime * this.stats.timeScale;

        return false;
    }

    public void ShootGatlin()
    {
        for (int i = 0; i < this.gatlinPlaceHolders.GetLength(0); i++)
        {
            var bullet = GameObject.Instantiate(this.gatlinBulletPrefab);

            bullet.transform.position = this.gatlinPlaceHolders[i].position;
            bullet.gameObject.layer = this.gameObject.layer;
            bullet.transform.up = -this.gatlinPlaceHolders[i].up;

            if(i < 2)
            {
                bullet.transform.Rotate(0, 0, i == 0 ? gatlinBulletTilt : -gatlinBulletTilt);
            }            
        }           

        this.gatlinFireCount = 0;
    }

    public void MoveCannon()
    {
        if (!this.cannonCharge)
        {
            this.transform.position += this.moveDirection * this.stats.timeScale * this.movementSpeed * Time.deltaTime;
        }
    }

    public bool CheckCannonTime()
    {
        if (this.cannonTimeCount >= this.cannonTime)
        {
            this.cannonTimeCount = 0;
            return true;
        }

        this.cannonTimeCount += Time.deltaTime * this.stats.timeScale;

        return false;
    }

    public bool CheckCannonFireRate()
    {
        if(!this.cannonCharge)
        {
            if (this.cannonFireCount >= this.cannonFireRate)
            {
                return true;
            }

            this.cannonFireCount += Time.deltaTime * this.stats.timeScale;
        }        

        return false;
    }

    public void ShootCannon()
    {
        this.cannonCharge = true;
        this.cannonFireCount = 0;

        var bullet = GameObject.Instantiate(this.cannonBulletPrefab);

        bullet.transform.position = this.cannonPlaceHolder.position;
        bullet.gameObject.layer = this.gameObject.layer;
        bullet.GetComponent<Bullet_Valkyrie>().chargeTime = this.cannonChargeTime;
    }

    public void StartCannonState()
    {
        this.behaivourState = ValkyrieBehaviourStateEnum.Cannon;
        this.cannonTimeCount = 0;
        this.cannonFireCount = 0;
        this.cannonChargeTimeCount = 0;
        this.ShootCannon();
    }

    public bool CheckCannonChargeTime()
    {
        if (this.cannonChargeTimeCount >= this.cannonChargeTime)
        {
            this.cannonChargeTimeCount = 0;
            this.cannonCharge = false;
            return true;
        }

        this.cannonChargeTimeCount += Time.deltaTime * this.stats.timeScale;

        return false;
    }

    public bool CheckGatlinTime()
    {
        if (this.gatlinTimeCount >= this.gatlinTime)
        {
            this.gatlinTimeCount = 0;
            return true;            
        }

        this.gatlinTimeCount += Time.deltaTime * this.stats.timeScale;

        return false;
    }

    public void StartGatlinState()
    {
        this.behaivourState = ValkyrieBehaviourStateEnum.Gatlin;
        this.gatlinFireCount = 0;
        this.gatlinTimeCount = 0;
    }

    public void StartTackleState()
    {
        this.behaivourState = ValkyrieBehaviourStateEnum.Tackle;
        this.tackleTimeCount = 0;
    }

    public void BeginTackle()
    {
        this.tackleStateOn = true;
        this.tackleTimeCount = 0;
        this.moveDirection = Vector3.down;        
    }

    public bool CheckTackleTimeRate()
    {
        if (!this.tackleStateOn)
        {
            if (this.tackleTimeCount >= this.tackleTimeRate)
            {
                return true;
            }

            this.tackleTimeCount += Time.deltaTime * this.stats.timeScale;
        }

        return false;
    }

    public void SwitchState()
    {
        if(this.behaivourState != ValkyrieBehaviourStateEnum.Tackle && this.stats.healthPoints <= this.tackleHealthThreshold)
        {
            this.StartTackleState();            
        }
        
        switch(this.behaivourState)
        {
            case ValkyrieBehaviourStateEnum.Arrival:
                this.StartGatlinState();
                break;

            case ValkyrieBehaviourStateEnum.Gatlin:
                this.StartCannonState();
                break;

            case ValkyrieBehaviourStateEnum.Cannon:
                this.StartGatlinState();
                break;

            default:
                break;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                if(this.stats.healthPoints <= 0)
                {
                    
                }
                break;
        }
    }

    private enum ValkyrieBehaviourStateEnum
    {
        Arrival = 0,
        Gatlin,
        Cannon,
        Tackle
    }
}
