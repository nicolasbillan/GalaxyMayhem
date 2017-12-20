using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Constants;

public class TimeScale : MonoBehaviour {
    
    public float playerScale = 1;
    public float globalScale = 1;

    public Image slowMotionGaugeSprite;
    public float slowMotionMaxTime;
    public float slowMotionRechargeTime;
    public float slowMotionGauge;    
    public float slowMotionPower;
    public bool slowMotionActive;

    // Use this for initialization
    void Start () {
        this.name = GameObjectNames.TimeScale;
        this.LoadSlowMotionGauge();
    }
	
	// Update is called once per frame
	void Update () {
        if(this.playerScale > 0)
        {
            this.slowMotionGaugeSprite.fillAmount = this.slowMotionGauge / this.slowMotionMaxTime;

            this.CheckSlowMotion();
        }
	}

    void LoadSlowMotionGauge()
    {
        this.slowMotionGaugeSprite.transform.SetParent(GameObject.Find(GameObjectNames.Ui).transform);
        this.slowMotionGaugeSprite.rectTransform.localScale = new Vector3(1, 1, 1);
        this.slowMotionGaugeSprite.enabled = false;
        this.slowMotionGauge = this.slowMotionMaxTime;
        this.slowMotionActive = false;
    }

    void CheckSlowMotion()
    {
        if (Input.GetAxis("SlowMotion") == 1 && (slowMotionActive && slowMotionGauge > 0 || !slowMotionActive && this.slowMotionGauge >= 0.5f))
        {
            if(!slowMotionActive && this.slowMotionGauge >= 0.5f)
            {
                this.slowMotionGauge -= 0.5f;
            }

            this.slowMotionActive = true;                      
        }
        else
        {                        
            this.slowMotionActive = false;
        }

        if(slowMotionActive)
        {
            this.slowMotionGaugeSprite.enabled = true;
            this.slowMotionGauge -= Time.deltaTime;
            this.globalScale = this.slowMotionPower;
        }
        else
        {
            if (slowMotionGauge < this.slowMotionMaxTime)
            {
                this.slowMotionGauge += Time.deltaTime;
            }
            else
            {
                this.slowMotionGaugeSprite.enabled = false;
            }

            this.globalScale = this.playerScale;
        }
    }

    public void SetPauseStatus(bool paused)
    {
        this.playerScale = paused ? 0 : 1;
        this.globalScale = paused ? 0 : 1;
    }    
}
