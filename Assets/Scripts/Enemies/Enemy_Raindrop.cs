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

    // Use this for initialization
    void Start()
    {
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

    private bool CheckHoldDistance()
    {
        var realDistance = this.transform.position.y - holdDistance.position.y;

        if (realDistance <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move(float speed)
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime * this.gameObject.GetComponent<Enemy>().timeScale;
    }
}
