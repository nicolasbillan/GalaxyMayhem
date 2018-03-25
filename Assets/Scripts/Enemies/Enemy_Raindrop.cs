using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Raindrop : MonoBehaviour
{
    public Transform holdDistance;
    
    public float holdTime;
    public float holdCount;
    public float normalSpeed;
    public float chargeSpeed;

    private Enemy stats;

    // Use this for initialization
    void Start()
    {
        stats = GetComponent<Enemy>();
        holdCount = 0;
    }    

    // Update is called once per frame
    void Update()
    {
        if (this.CheckHoldDistance())
        {
            if (holdCount >= holdTime)
            {
                this.Move(chargeSpeed);
            }
            else
            {
                holdCount += Time.deltaTime;
            }            
        }
        else
        {
            this.Move(normalSpeed);
        }
    }

    void LoadStats()
    {
        this.stats = this.GetComponent<Enemy>();
    }

    private bool CheckHoldDistance()
    {
        return (this.transform.position.y - holdDistance.position.y <= 0);
    }

    private void Move(float speed)
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime * this.stats.timeScale;
    }
}
